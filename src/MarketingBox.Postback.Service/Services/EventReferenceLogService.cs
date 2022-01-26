using AutoMapper;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Grpc.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Services
{
    public class EventReferenceLogService : IEventReferenceLogService
    {
        private readonly ILogger<EventReferenceLogService> _logger;
        private readonly IMapper _mapper;
        private readonly IEventReferenceLoggerRepository _eventReferenceLogger;

        public EventReferenceLogService(
            ILogger<EventReferenceLogService> logger,
            IMapper mapper,
            IEventReferenceLoggerRepository eventReferenceLogger)
        {
            _logger = logger;
            _mapper = mapper;
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
                    Success = true,
                    Data = res.Select(_mapper.Map<EventReferenceLog>).ToArray()
                };
            }
            catch(Exception ex)
            {
                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchLogsAsync(Grpc.Models.FilterLogsRequest request)
        {
            try
            {
                _logger.LogInformation("Searching logs by filter: {FilterLogsRequest}", JsonConvert.SerializeObject(request));

                var res = await _eventReferenceLogger.SearchAsync(_mapper.Map<Domain.FilterLogsRequest>(request));

                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    Success = true,
                    Data = res.Select(_mapper.Map<EventReferenceLog>).ToArray()
                };
            }
            catch (Exception ex)
            {
                return new Response<IReadOnlyCollection<EventReferenceLog>>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}