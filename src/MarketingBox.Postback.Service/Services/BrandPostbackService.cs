using System;
using System.Threading.Tasks;
using AutoMapper;
using MarketingBox.Affiliate.Service.MyNoSql.Affiliates;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.Registration.Service.Domain.Models.Affiliate;
using MarketingBox.Registration.Service.Grpc;
using MarketingBox.Registration.Service.Grpc.Requests.Crm;
using MarketingBox.Registration.Service.Grpc.Requests.Deposits;
using MarketingBox.Registration.Service.Grpc.Requests.Registration;
using MarketingBox.Reporting.Service.Grpc;
using MarketingBox.Reporting.Service.Grpc.Requests.TrackingLinks;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using MyNoSqlServer.Abstractions;

namespace MarketingBox.Postback.Service.Services
{
    public class BrandPostbackService : IBrandPostbackService
    {
        private readonly ILogger<BrandPostbackService> _logger;
        private readonly IMyNoSqlServerDataReader<AffiliateNoSql> _affiliateNoSqlServerDataReader;
        private readonly IServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage> _serviceBusPublisher;
        private readonly ITrackingLinkReportService _trackingLinkReportService;
        private readonly Registration.Service.Grpc.IRegistrationService _registrationService;
        private readonly ICrmService _crmService;
        private readonly IDepositService _depositService;
        private readonly IMapper _mapper;

        // Todo: implement proper logic for multi-tenancy and get rid of this constant.
        private const string TenantId = "default-tenant-id";

        public BrandPostbackService(ILogger<BrandPostbackService> logger,
            IServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage> serviceBusPublisher,
            ITrackingLinkReportService trackingLinkReportService,
            Registration.Service.Grpc.IRegistrationService registrationService,
            IMapper mapper,
            ICrmService crmService,
            IDepositService depositService,
            IMyNoSqlServerDataReader<AffiliateNoSql> affiliateNoSqlServerDataReader)
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _trackingLinkReportService = trackingLinkReportService;
            _registrationService = registrationService;
            _mapper = mapper;
            _crmService = crmService;
            _depositService = depositService;
            _affiliateNoSqlServerDataReader = affiliateNoSqlServerDataReader;
        }

        public async Task<Response<bool>> ProcessRequestAsync(BrandPostbackRequest request)
        {
            try
            {
                request.ValidateEntity();

                var (registrationId, affiliateId) = await GetRegistrationAndAffiliateIdsByClickIdAsync(request.ClickId);
                Registration.Service.Domain.Models.Registrations.Registration registration = null;
                switch (request.EventType)
                {
                    case BrandEventType.Registeration:
                    {
                        if (!registrationId.HasValue)
                        {
                            await RegisterAsync(request, affiliateId);
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
                           registration = await RegisterAsync(request, affiliateId);
                        }

                        await DepositAsync(request, registration.RegistrationId);
                        break;
                    }
                    case BrandEventType.ChangedCrm:
                    {
                        if (!registrationId.HasValue)
                        {
                           registration = await RegisterAsync(request, affiliateId);
                        }

                        await ChangeCrmAsync(request, registration.RegistrationId);
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

        private async Task<(long?, long)> GetRegistrationAndAffiliateIdsByClickIdAsync(long clickId)
        {
            var trackingLinkResponse = await _trackingLinkReportService.GetAsync(
                new TrackingLinkByClickIdRequest
                {
                    ClickId = clickId
                });
            var trackingLink = trackingLinkResponse.Process();
            return (trackingLink.RegistrationId, trackingLink.AffiliateId);
        }

        private async Task ChangeCrmAsync(BrandPostbackRequest request, long registrationId)
        {
            _logger.LogInformation("Changing crm status: {@Context} ", request.CrmStatus);
            await _crmService.SetCrmStatusAsync(new UpdateCrmStatusRequest
            {
                Crm = request.CrmStatus,
                RegistrationId = registrationId,
                TenantId = TenantId
            });
        }

        private async Task DepositAsync(BrandPostbackRequest request, long registrationId)
        {
            _logger.LogInformation("Make deposit: {@Context} ", request);
            await _depositService.RegisterDepositAsync(new DepositCreateRequest
            {
                RegistrationId = registrationId,
                TenantId = TenantId
            });
        }

        private async Task<Registration.Service.Domain.Models.Registrations.Registration> RegisterAsync(BrandPostbackRequest request, long affiliateId)
        {
            _logger.LogInformation("Register: {@Context} ", request);

            var affiliate = _affiliateNoSqlServerDataReader.Get(
                AffiliateNoSql.GeneratePartitionKey(TenantId),
                AffiliateNoSql.GenerateRowKey(affiliateId));
            if (affiliate is null)
            {
                throw new NotFoundException("Affiliate with id", affiliateId);
            }

            var registrationRequest = _mapper.Map<RegistrationCreateRequest>(request);
            registrationRequest.AuthInfo = new AffiliateAuthInfo
            {
                AffiliateId = affiliateId,
                ApiKey = affiliate.Affiliate?.GeneralInfo?.ApiKey
            };

            var response = await _registrationService.CreateAsync(registrationRequest);
            var registration = response.Process();

            await _serviceBusPublisher.PublishAsync(new TrackingLinkUpdateRegistrationIdMessage
            {
                ClickId = request.ClickId,
                RegistrationId = registration.RegistrationId
            });

            return registration;
        }
    }
}