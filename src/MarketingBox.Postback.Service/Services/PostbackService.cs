using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Grpc.Models;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain;
using AutoMapper;
using System;
using System.Text.Json;

namespace MarketingBox.Postback.Service.Services
{
    public class PostbackService : IPostbackService
    {
        private readonly ILogger<PostbackService> _logger;
        private readonly IReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;
        private readonly IAffiliateReferenceLoggerRepository _loggerRepository;

        public PostbackService(ILogger<PostbackService> logger,
            IReferenceRepository referenceRepository,
            IMapper mapper,
            IAffiliateReferenceLoggerRepository loggerRepository)
        {
            _logger = logger;
            _referenceRepository = referenceRepository;
            _mapper = mapper;
            _loggerRepository = loggerRepository;
        }
        private static Response<T> FailedResponse<T>(string errorMessage)
        {
           return new Response<T> { Success = false, ErrorMessage = errorMessage };
        }

        public async Task<Response<bool>> DeleteReferenceAsync(ByAffiliateIdRequest request)
        {
            try
            {
                _logger.LogInformation("Deleting reference entity for affiliate with id {AffiliateId}", request.AffiliateId);

                await _referenceRepository.DeleteReferenceAsync(request.AffiliateId);

                await _loggerRepository.CreateAsync(request.AffiliateId, OperationType.Delete);

                return new Response<bool> { Success = true, Data = true };
            }
            catch(Exception ex)
            {
                return FailedResponse<bool>(ex.Message);
            }
        }

        public async Task<Response<ReferenceResponse>> GetReferenceAsync(ByAffiliateIdRequest request)
        {
            try
            {
                _logger.LogInformation("Getting reference entity for affiliate with id {AffiliateId}", request.AffiliateId);

                var res = await _referenceRepository.GetReferenceAsync(request.AffiliateId);

                await _loggerRepository.CreateAsync(request.AffiliateId, OperationType.Get);

                return new Response<ReferenceResponse>
                {
                    Success = true,
                    Data = _mapper.Map<ReferenceResponse>(res)
                };
            }
            catch(Exception ex)
            {
                return FailedResponse<ReferenceResponse>(ex.Message);
            }
        }

        public async Task<Response<ReferenceResponse>> CreateReferenceAsync(FullReferenceRequest request)
        {
            try
            {
                _logger.LogInformation("Saving reference: {SaveReferenceRequest}", JsonSerializer.Serialize(request));

                var res = await _referenceRepository.CreateReferenceAsync(_mapper.Map<Reference>(request));

                await _loggerRepository.CreateAsync(request.AffiliateId, OperationType.Create);

                return new Response<ReferenceResponse>
                {
                    Success = true, 
                    Data = _mapper.Map<ReferenceResponse>(res)
                };
            }
            catch(Exception ex)
            {
                return FailedResponse<ReferenceResponse>(ex.Message);
            }
        }

        public async Task<Response<ReferenceResponse>> UpdateReferenceAsync(FullReferenceRequest request)
        {
            try
            {
                _logger.LogInformation("Updating reference: {UpdateReferenceRequest}", JsonSerializer.Serialize(request));

                var res = await _referenceRepository.UpdateReferenceAsync(_mapper.Map<Reference>(request));

                await _loggerRepository.CreateAsync(request.AffiliateId, OperationType.Update);

                return new Response<ReferenceResponse>
                {
                    Success = true,
                    Data = _mapper.Map<ReferenceResponse>(res)
                };
            }
            catch(Exception ex)
            {
                return FailedResponse<ReferenceResponse>(ex.Message);
            }
        }
    }
}
