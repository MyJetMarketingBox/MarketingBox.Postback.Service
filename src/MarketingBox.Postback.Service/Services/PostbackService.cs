using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain;
using System;
using System.Text.Json;
using FluentValidation;
using MarketingBox.Affiliate.Service.Grpc;
using MarketingBox.Postback.Service.Domain.Models.Requests;
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
        private readonly IAffiliateService _affiliateService;
        private readonly IAffiliateRepository _affiliateRepository;
        private readonly IValidator<CreateOrUpdateReferenceRequest> _createUpdateValidator;
        private readonly IValidator<ByAffiliateIdRequest> _affiliateIdValidator;

        public PostbackService(ILogger<PostbackService> logger,
            IReferenceRepository referenceRepository,
            IAffiliateReferenceLoggerRepository loggerRepository,
            IAffiliateService affiliateService,
            IAffiliateRepository affiliateRepository,
            IValidator<CreateOrUpdateReferenceRequest> createUpdateValidator,
            IValidator<ByAffiliateIdRequest> affiliateIdValidator)
        {
            _logger = logger;
            _referenceRepository = referenceRepository;
            _loggerRepository = loggerRepository;
            _affiliateService = affiliateService;
            _affiliateRepository = affiliateRepository;
            _createUpdateValidator = createUpdateValidator;
            _affiliateIdValidator = affiliateIdValidator;
        }

        public async Task<Response<bool>> DeleteAsync(ByAffiliateIdRequest request)
        {
            try
            {
                await _affiliateIdValidator.ValidateAndThrowAsync(request);
                
                _logger.LogInformation("Deleting reference entity for affiliate with id {AffiliateId}",
                    request.AffiliateId);

                var deletedId = await _referenceRepository.DeleteAsync(request.AffiliateId.Value);

                await _loggerRepository.CreateAsync(request.AffiliateId.Value, deletedId, OperationType.Delete);

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
                await _affiliateIdValidator.ValidateAndThrowAsync(request);
                
                _logger.LogInformation("Getting reference entity for affiliate with id {AffiliateId}",
                    request.AffiliateId);

                var res = await _referenceRepository.GetAsync(request.AffiliateId.Value);

                await _loggerRepository.CreateAsync(request.AffiliateId.Value, res.Id, OperationType.Get);

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

        public async Task<Response<Reference>> CreateAsync(CreateOrUpdateReferenceRequest request)
        {
            try
            {
                await _createUpdateValidator.ValidateAndThrowAsync(request);
                _logger.LogInformation("Getting information about affiliate with id {AffiliateId}",
                    request.AffiliateId);
                var affiliateResponse = await _affiliateService.GetAsync(new ()
                {
                    AffiliateId = request.AffiliateId.Value
                });
                var affiliate = affiliateResponse.Process();

                _logger.LogWarning("Saving information about affiliate with id {AffiliateId}",
                    request.AffiliateId);
                await _affiliateRepository.CreateAsync(
                    new Domain.Models.Affiliate
                    {
                        Id = affiliate.Id,
                        Name = affiliate.Username
                    });

                _logger.LogInformation("Saving reference: {SaveReferenceRequest}", JsonSerializer.Serialize(request));

                var res = await _referenceRepository.CreateAsync(request);

                await _loggerRepository.CreateAsync(request.AffiliateId.Value, res.Id, OperationType.Create);

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

        public async Task<Response<Reference>> UpdateAsync(CreateOrUpdateReferenceRequest request)
        {
            try
            {
                await _createUpdateValidator.ValidateAndThrowAsync(request);
                
                _logger.LogInformation("Updating reference: {UpdateReferenceRequest}", request);

                var res = await _referenceRepository.UpdateAsync(request);

                await _loggerRepository.CreateAsync(request.AffiliateId.Value, res.Id, OperationType.Update);

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