using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text.Json;
using MarketingBox.Affiliate.Service.Client.Interfaces;
using MarketingBox.Affiliate.Service.Domain.Models.Affiliates;
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
        private readonly IAffiliateClient _affiliateClient;
        private readonly IAffiliateRepository _affiliateRepository;

        private async Task UpsertUserInformation(long userId, string tenantId)
        {
            _logger.LogInformation("Getting information about user with id {UserId}", userId);
            var affiliate = await _affiliateClient.GetAffiliateById(userId, tenantId, true);

            _logger.LogWarning("Saving information about user with id {UserId}", userId);
            await _affiliateRepository.UpsertAsync(
                new Domain.Models.Affiliate
                {
                    Id = affiliate.AffiliateId,
                    Name = affiliate.GeneralInfo.Username,
                    TenantId = affiliate.TenantId
                });
        }

        public PostbackService(ILogger<PostbackService> logger,
            IReferenceRepository referenceRepository,
            IAffiliateReferenceLoggerRepository loggerRepository,
            IAffiliateRepository affiliateRepository,
            IAffiliateClient affiliateClient)
        {
            _logger = logger;
            _referenceRepository = referenceRepository;
            _loggerRepository = loggerRepository;
            _affiliateRepository = affiliateRepository;
            _affiliateClient = affiliateClient;
        }

        public async Task<Response<bool>> DeleteAsync(ByIdRequest request)
        {
            try
            {
                request.ValidateEntity();

                await UpsertUserInformation(request.RequestedBy.Value, request.TenantId);

                _logger.LogInformation("Deleting reference entity with id {Id}",
                    request.PostbackId);
                var res = await _referenceRepository.DeleteAsync(request.PostbackId.Value);

                await _loggerRepository.CreateAsync(
                    request.RequestedBy.Value,
                    res.Id,
                    res.TenantId,
                    OperationType.Delete);

                return new Response<bool> {Status = ResponseStatus.Ok, Data = true};
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<bool>();
            }
        }

        public async Task<Response<Reference>> GetAsync(ByIdRequest request)
        {
            try
            {
                request.ValidateEntity();

                await UpsertUserInformation(request.RequestedBy.Value, request.TenantId);
                
                _logger.LogInformation("Getting reference entity with id {Id}",
                    request.PostbackId);

                var res = await _referenceRepository.GetAsync(request.PostbackId.Value);

                await _loggerRepository.CreateAsync(request.RequestedBy.Value, res.Id, res.TenantId, OperationType.Get);

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

        public async Task<Response<IReadOnlyCollection<Reference>>> SearchAsync(SearchReferenceRequest request)
        {
            try
            {
                request.ValidateEntity();

                _logger.LogInformation("Search reference entities for request {@Request}",
                    request);

                var (res, total) = await _referenceRepository.SearchAsync(request);

                return new Response<IReadOnlyCollection<Reference>>
                {
                    Status = ResponseStatus.Ok,
                    Data = res,
                    Total = total
                };
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<IReadOnlyCollection<Reference>>();
            }
        }

        public async Task<Response<Reference>> CreateAsync(CreateReferenceRequest request)
        {
            try
            {
                request.ValidateEntity();

                await UpsertUserInformation(request.AffiliateId.Value, request.TenantId);

                if (request.AffiliateId != request.CreatedBy)
                {
                    await UpsertUserInformation(request.CreatedBy.Value, request.TenantId);
                }

                _logger.LogInformation("Saving reference: {@SaveReferenceRequest}", request);

                var res = await _referenceRepository.CreateAsync(request);

                await _loggerRepository.CreateAsync(
                    request.CreatedBy.Value,
                    res.Id,
                    res.TenantId,
                    OperationType.Create);

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

        public async Task<Response<Reference>> UpdateAsync(UpdateReferenceRequest request)
        {
            try
            {
                request.ValidateEntity();
                
                await UpsertUserInformation(request.RequestedBy.Value, request.TenantId);

                _logger.LogInformation("Updating reference: {@UpdateReferenceRequest}", request);

                var res = await _referenceRepository.UpdateAsync(request);

                await _loggerRepository.CreateAsync(
                    request.RequestedBy.Value,
                    res.Id,
                    res.TenantId,
                    OperationType.Update);

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