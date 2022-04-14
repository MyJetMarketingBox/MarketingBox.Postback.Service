using System;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Postback.Service.Services
{
    public class BrandPostbackService : IBrandPostbackService
    {
        private readonly ILogger<BrandPostbackService> _logger;

        public BrandPostbackService(ILogger<BrandPostbackService> logger)
        {
            _logger = logger;
        }

        public async Task<Response<bool>> ProcessRequestAsync(BrandPostbackRequest request)
        {
            try
            {
                request.ValidateEntity();

                switch (request.EventType)
                {
                    case BrandEventType.Registered:
                        await RegisterateAsync(request);
                        break;
                    case BrandEventType.Deposited:
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
            _logger.LogInformation("Changing crm status: {@Status} ", request.CrmStatus);
            throw new NotImplementedException();
        }

        private async Task DepositAsync(BrandPostbackRequest request)
        {
            _logger.LogInformation("Make deposit: {@Status} ", request.CrmStatus);
            throw new NotImplementedException();
        }

        private async Task RegisterateAsync(BrandPostbackRequest request)
        {
            _logger.LogInformation("Changing crm status: {@Status} ", request.CrmStatus);
            throw new NotImplementedException();
        }
    }
}