using System;
using System.Threading.Tasks;
using AutoMapper;
using MarketingBox.Affiliate.Service.Client.Interfaces;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.Registration.Service.Domain.Models.Affiliate;
using MarketingBox.Registration.Service.Grpc;
using MarketingBox.Registration.Service.Grpc.Requests.Crm;
using MarketingBox.Registration.Service.Grpc.Requests.Deposits;
using MarketingBox.Registration.Service.Grpc.Requests.Registration;
using MarketingBox.Reporting.Service.Domain.Models.TrackingLinks;
using MarketingBox.Reporting.Service.Grpc;
using MarketingBox.Reporting.Service.Grpc.Requests.TrackingLinks;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;

namespace MarketingBox.Postback.Service.Services
{
    public class BrandPostbackService : IBrandPostbackService
    {
        private readonly ILogger<BrandPostbackService> _logger;
        private readonly IAffiliateClient _affiliateClient;
        private readonly IServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage> _serviceBusPublisher;
        private readonly ITrackingLinkReportService _trackingLinkReportService;
        private readonly Registration.Service.Grpc.IRegistrationService _registrationService;
        private readonly ICrmService _crmService;
        private readonly IDepositService _depositService;
        private readonly IMapper _mapper;
        
        public BrandPostbackService(ILogger<BrandPostbackService> logger,
            IServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage> serviceBusPublisher,
            ITrackingLinkReportService trackingLinkReportService,
            Registration.Service.Grpc.IRegistrationService registrationService,
            IMapper mapper,
            ICrmService crmService,
            IDepositService depositService, IAffiliateClient affiliateClient)
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _trackingLinkReportService = trackingLinkReportService;
            _registrationService = registrationService;
            _mapper = mapper;
            _crmService = crmService;
            _depositService = depositService;
            _affiliateClient = affiliateClient;
        }

        public async Task<Response<bool>> ProcessRequestAsync(BrandPostbackRequest request)
        {
            try
            {
                request.ValidateEntity();

                var trackingLink =
                    await GetTrackingLinkByClickIdAsync(request.ClickId);
                var registrationId = trackingLink.RegistrationId;
                var affiliateId = trackingLink.AffiliateId;
                var brandId = trackingLink.BrandId;
                var tenantId = trackingLink.TenantId;
                var offerId = trackingLink.OfferId;
                Registration.Service.Domain.Models.Registrations.Registration registration = null;
                switch (request.EventType)
                {
                    case BrandEventType.Registeration:
                    {
                        if (!registrationId.HasValue)
                        {
                            await RegisterAsync(request, affiliateId, brandId, offerId);
                        }
                        else
                        {
                            //todo: discuss with business
                            _logger.LogWarning("Registration {@RegistrationId} already exists", registrationId);
                        }

                        break;
                    }
                    case BrandEventType.Deposit:
                    {
                        if (!registrationId.HasValue)
                        {
                            registration = await RegisterAsync(request, affiliateId, brandId, offerId);
                            registrationId = registration.Id;
                        }

                        await DepositAsync(request, registrationId.Value, tenantId);
                        break;
                    }
                    case BrandEventType.ChangedCrm:
                    {
                        if (!registrationId.HasValue)
                        {
                            registration = await RegisterAsync(request, affiliateId, brandId, offerId);
                            registrationId = registration.Id;
                        }

                        await ChangeCrmAsync(request, registrationId.Value, tenantId);
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return e.FailedResponse<bool>();
            }
        }

        private async Task<TrackingLink> GetTrackingLinkByClickIdAsync(long clickId)
        {
            var trackingLinkResponse = await _trackingLinkReportService.GetAsync(
                new TrackingLinkByClickIdRequest
                {
                    ClickId = clickId
                });
            var trackingLink = trackingLinkResponse.Process();
            return trackingLink;
        }

        private async Task ChangeCrmAsync(BrandPostbackRequest request, long registrationId, string tenantId)
        {
            _logger.LogInformation("Changing crm status: {@Context} ", request.CrmStatus);
            await _crmService.SetCrmStatusAsync(new UpdateCrmStatusRequest
            {
                Crm = request.CrmStatus,
                RegistrationId = registrationId,
                TenantId = tenantId
            });
        }

        private async Task DepositAsync(BrandPostbackRequest request, long registrationId, string tenantId)
        {
            _logger.LogInformation("Make deposit: {@Context} ", request);
            await _depositService.RegisterDepositAsync(new DepositCreateRequest
            {
                RegistrationId = registrationId,
                TenantId = tenantId
            });
        }

        private async Task<Registration.Service.Domain.Models.Registrations.Registration> RegisterAsync(
            BrandPostbackRequest request, long affiliateId, long brandId, long? offerId)
        {
            _logger.LogInformation("Register: {@Context} ", request);

            var affiliate = await _affiliateClient.GetAffiliateById(affiliateId, checkInService: true);

            var registrationRequest = _mapper.Map<RegistrationCreateS2SRequest>(request);
            registrationRequest.BrandId = brandId;
            registrationRequest.OfferId = offerId;
            
            registrationRequest.AuthInfo = new AffiliateAuthInfo
            {
                AffiliateId = affiliateId,
                ApiKey = affiliate.GeneralInfo?.ApiKey
            };
            var response = await _registrationService.CreateS2SAsync(registrationRequest);
            var registration = response.Process();

            await _serviceBusPublisher.PublishAsync(new TrackingLinkUpdateRegistrationIdMessage
            {
                ClickId = request.ClickId,
                RegistrationId = registration.Id
            });

            return registration;
        }
    }
}