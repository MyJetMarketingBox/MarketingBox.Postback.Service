using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.Services
{
    public class PostbackService: IPostbackService
    {
        private readonly ILogger<PostbackService> _logger;

        public PostbackService(ILogger<PostbackService> logger)
        {
            _logger = logger;
        }

        public Task DeleteReferenceAsync(ReferenceByAffiliateRequest request)
        {
            _logger.LogInformation("Deleting reference entity for affiliate id {id}", request.AffiliateId);
            return Task.CompletedTask;
        }

        public Task<ReferenceResponse> GetReferenceAsync(ReferenceByAffiliateRequest request)
        {
            _logger.LogInformation("Getting reference entity for affiliate id {id}", request.AffiliateId);

            return Task.FromResult(new ReferenceResponse
            {
                RegistrationReference = "Hello " + request.AffiliateId
            });
        }

        public Task<ReferenceResponse> SaveReferenceAsync(FullReferenceRequest request)
        {
            _logger.LogInformation("Saving reference entity for affiliate id {id}", request);
            return Task.FromResult(new ReferenceResponse
            {
                RegistrationReference = "Hello " + request.AffiliateId
            });
        }

        public Task<ReferenceResponse> UpdateReferenceAsync(FullReferenceRequest request)
        {
            _logger.LogInformation("Updating reference entity for affiliate id {id}", request.AffiliateId);
            return Task.FromResult(new ReferenceResponse
            {
                RegistrationReference = "Hello " + request.AffiliateId
            });
        }
    }
}
