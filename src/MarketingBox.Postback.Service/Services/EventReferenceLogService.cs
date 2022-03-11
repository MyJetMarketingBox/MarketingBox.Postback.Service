using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Grpc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using FilterLogsRequest = MarketingBox.Postback.Service.Domain.Models.Requests.FilterLogsRequest;
using ResponseStatus = MarketingBox.Sdk.Common.Models.Grpc.ResponseStatus;

namespace MarketingBox.Postback.Service.Services
{
    public class EventReferenceLogService : IEventReferenceLogService
    {
        private readonly ILogger<EventReferenceLogService> _logger;
        private readonly IEventReferenceLoggerRepository _eventReferenceLogger;
        private readonly IValidator<ByAffiliateIdPaginatedRequest> _paginatedValidator;
        private readonly IValidator<FilterLogsRequest> _filterLogsValidator;

        public EventReferenceLogService(
            ILogger<EventReferenceLogService> logger,
            IEventReferenceLoggerRepository eventReferenceLogger,
            IValidator<ByAffiliateIdPaginatedRequest> paginatedValidator,
            IValidator<FilterLogsRequest> filterLogsValidator)
        {
            _logger = logger;
            _eventReferenceLogger = eventReferenceLogger;
            _paginatedValidator = paginatedValidator;
            _filterLogsValidator = filterLogsValidator;
        }
        public async Task<Response<IReadOnlyCollection<EventReferenceLog>>> GetAsync(ByAffiliateIdPaginatedRequest request)
        {
            try
            {
                await _paginatedValidator.ValidateAndThrowAsync(request);
                
                _logger.LogInformation("Getting logs for affiliate: {AffiliateId}", request.AffiliateId);

                var res = await _eventReferenceLogger.GetAsync(request);

                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    Status = ResponseStatus.Ok,
                    Data = res.ToArray()
                };
            }
            catch(Exception ex)
            {
                return ex.FailedResponse<IReadOnlyCollection<EventReferenceLog>>();
            }
        }


        public async Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchAsync(FilterLogsRequest request)
        {
            try
            {
                await _filterLogsValidator.ValidateAndThrowAsync(request);
                
                _logger.LogInformation("Searching logs by filter: {FilterLogsRequest}", JsonConvert.SerializeObject(request));

                var res = await _eventReferenceLogger.SearchAsync(request);

                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    Status = ResponseStatus.Ok,
                    Data = res.ToArray()
                };
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<IReadOnlyCollection<EventReferenceLog>>();
            }
        }
    }
}