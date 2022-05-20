using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Grpc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
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

        public EventReferenceLogService(
            ILogger<EventReferenceLogService> logger,
            IEventReferenceLoggerRepository eventReferenceLogger)
        {
            _logger = logger;
            _eventReferenceLogger = eventReferenceLogger;
        }

        public async Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchAsync(FilterLogsRequest request)
        {
            try
            {
                request.ValidateEntity();

                _logger.LogInformation("Searching logs by filter: {@FilterLogsRequest}", request);

                var (res, total) = await _eventReferenceLogger.SearchAsync(request);

                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    Status = ResponseStatus.Ok,
                    Data = res,
                    Total = total
                };
            }
            catch (Exception ex)
            {
                return ex.FailedResponse<IReadOnlyCollection<EventReferenceLog>>();
            }
        }
    }
}