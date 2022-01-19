﻿using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Grpc.Models;
using MarketingBox.Postback.Service.Repositories;

namespace MarketingBox.Postback.Service.Services
{
    public class PostbackService: IPostbackService
    {
        private readonly ILogger<PostbackService> _logger;
        private readonly IReferenceRepository _referenceRepository;

        public PostbackService(ILogger<PostbackService> logger,
            IReferenceRepository referenceRepository)
        {
            _logger = logger;
            _referenceRepository = referenceRepository;
        }

        public async Task DeleteReferenceAsync(ReferenceByAffiliateRequest request)
        {
            _logger.LogInformation("Deleting reference entity for affiliate id {id}", request.AffiliateId);
            await _referenceRepository.DeleteReferenceAsync(request.AffiliateId);
        }

        public async Task<ReferenceResponse> GetReferenceAsync(ReferenceByAffiliateRequest request)
        {
            _logger.LogInformation("Getting reference entity for affiliate id {id}", request.AffiliateId);

            return await _referenceRepository.GetReferenceAsync(request.AffiliateId);
        }

        public async Task<ReferenceResponse> SaveReferenceAsync(FullReferenceRequest request)
        {
            _logger.LogInformation("Saving reference entity for affiliate id {id}", request);
            return await _referenceRepository.SaveReferenceAsync(request);
        }

        public async Task<ReferenceResponse> UpdateReferenceAsync(FullReferenceRequest request)
        {
            _logger.LogInformation("Updating reference entity for affiliate id {id}", request.AffiliateId);
            return await _referenceRepository.UpdateReferenceAsync(request);
        }
    }
}