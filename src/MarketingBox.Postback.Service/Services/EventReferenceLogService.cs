using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Grpc.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Helper;
using FilterLogsRequest = MarketingBox.Postback.Service.Domain.Models.FilterLogsRequest;

namespace MarketingBox.Postback.Service.Services
{
    public class EventReferenceLogService : IEventReferenceLogService
    {
        private readonly ILogger<EventReferenceLogService> _logger;
        private readonly IEventReferenceLoggerRepository _eventReferenceLogger;

        public EventReferenceLogService(
            ILogger<EventReferenceLogService> logger,
            IEventReferenceLoggerRepository eventReferenceLogger)
        {
            _logger = logger;
            _eventReferenceLogger = eventReferenceLogger;
        }
        public async Task<Response<IReadOnlyCollection<EventReferenceLog>>> GetLogsAsync(ByAffiliateIdRequest request)
        {
            try
            {
                _logger.LogInformation("Getting logs for affiliate: {AffiliateId}", request.AffiliateId);

                var res = await _eventReferenceLogger.GetAsync(request.AffiliateId);

                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    StatusCode = StatusCode.Ok,
                    Data = res.ToArray()
                };
            }
            catch(Exception ex)
            {
                return ex.FailedResponse<IReadOnlyCollection<EventReferenceLog>>();
            }
        }


        public async Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchLogsAsync(FilterLogsRequest request)
        {
            try
            {
                _logger.LogInformation("Searching logs by filter: {FilterLogsRequest}", JsonConvert.SerializeObject(request));

                var res = await _eventReferenceLogger.SearchAsync(request);

                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    StatusCode = StatusCode.Ok,
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