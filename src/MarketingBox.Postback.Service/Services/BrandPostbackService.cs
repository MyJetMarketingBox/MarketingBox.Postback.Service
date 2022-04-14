using System;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;

namespace MarketingBox.Postback.Service.Services
{
    public class BrandPostbackService : IBrandPostbackService
    {
        private readonly ILogger<BrandPostbackService> _logger;
        private readonly IServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage> _serviceBusPublisher;

        public BrandPostbackService(ILogger<BrandPostbackService> logger,
            IServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage> serviceBusPublisher)
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task<Response<bool>> ProcessRequestAsync(BrandPostbackRequest request)
        {
            try
            {
                request.ValidateEntity();

                switch (request.EventType)
                {
                    case BrandEventType.Registeration:
                        await RegisterAsync(request);
                        break;
                    case BrandEventType.Deposit:
                        await DepositAsync(request);
                        break;
                    case BrandEventType.ChangedCrm:
                        await ChangeCrmAsync(request);
                        break;
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

        private async Task ChangeCrmAsync(BrandPostbackRequest request)
        {
            _logger.LogInformation("Changing crm status: {@Context} ", request.CrmStatus);
            // get registration from ReportService.
            var registrationId = 1;
            
            // if registrationId is null -> create registration
            
            // -> update tracking link service
            await _serviceBusPublisher.PublishAsync(new TrackingLinkUpdateRegistrationIdMessage
            {
                ClickId = request.ClickId,
                RegistrationId = registrationId
            });
        }

        private async Task DepositAsync(BrandPostbackRequest request)
        {
            _logger.LogInformation("Make deposit: {@Context} ", request);
            throw new NotImplementedException();
        }

        private async Task RegisterAsync(BrandPostbackRequest request)
        {
            _logger.LogInformation("Changing crm status: {@Context} ", request);
            throw new NotImplementedException();
        }
    }
}