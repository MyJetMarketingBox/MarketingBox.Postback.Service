using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain;
using System;
using System.Text.Json;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using ResponseStatus = MarketingBox.Sdk.Common.Models.Grpc.ResponseStatus;

namespace MarketingBox.Postback.Service.Services
{
    public class PostbackService : IPostbackService
    {
        private readonly ILogger<PostbackService> _logger;
        private readonly IReferenceRepository _referenceRepository;
        private readonly IAffiliateReferenceLoggerRepository _loggerRepository;

        public PostbackService(ILogger<PostbackService> logger,
            IReferenceRepository referenceRepository,
            IAffiliateReferenceLoggerRepository loggerRepository)
        {
            _logger = logger;
            _referenceRepository = referenceRepository;
            _loggerRepository = loggerRepository;
        }

        public async Task<Response<bool>> DeleteAsync(ByAffiliateIdRequest request)
        {
            try
            {
                _logger.LogInformation("Deleting reference entity for affiliate with id {AffiliateId}",
                    request.AffiliateId);

                var deletedId = await _referenceRepository.DeleteAsync(request.AffiliateId);

                await _loggerRepository.CreateAsync(request.AffiliateId, deletedId, OperationType.Delete);

                return new Response<bool> {Status = ResponseStatus.Ok, Data = true};
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<bool>();
            }
        }

        public async Task<Response<Reference>> GetAsync(ByAffiliateIdRequest request)
        {
            try
            {
                _logger.LogInformation("Getting reference entity for affiliate with id {AffiliateId}",
                    request.AffiliateId);

                var res = await _referenceRepository.GetAsync(request.AffiliateId);

                await _loggerRepository.CreateAsync(request.AffiliateId, res.Id, OperationType.Get);

                return new Response<Reference>
                {
                    Status = ResponseStatus.Ok,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<Reference>();
            }
        }

        public async Task<Response<Reference>> CreateAsync(Reference request)
        {
            try
            {
                _logger.LogInformation("Saving reference: {SaveReferenceRequest}", JsonSerializer.Serialize(request));

                var res = await _referenceRepository.CreateAsync(request);

                await _loggerRepository.CreateAsync(request.AffiliateId, res.Id, OperationType.Create);

                return new Response<Reference>
                {
                    Status = ResponseStatus.Ok,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<Reference>();
            }
        }

        public async Task<Response<Reference>> UpdateAsync(Reference request)
        {
            try
            {
                _logger.LogInformation("Updating reference: {UpdateReferenceRequest}",
                    JsonSerializer.Serialize(request));

                var res = await _referenceRepository.UpdateAsync(request);

                await _loggerRepository.CreateAsync(request.AffiliateId, res.Id, OperationType.Update);

                return new Response<Reference>
                {
                    Status = ResponseStatus.Ok,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<Reference>();
            }
        }
    }
}