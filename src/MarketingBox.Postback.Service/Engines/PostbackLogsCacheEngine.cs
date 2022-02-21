using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Postback.Service.Engines
{
    public class PostbackLogsCacheEngine : IPostbackLogsCacheEngine, IStartable
    {
        private readonly IEventReferenceLoggerRepository _loggerRepository;
        private readonly ILogger<PostbackLogsCacheEngine> _logger;
        private const int Capacity = 100;

        private static HashSet<PostbackLogsCacheModel> _cache = new(Capacity);
        
        public PostbackLogsCacheEngine(
            IEventReferenceLoggerRepository loggerRepository,
            ILogger<PostbackLogsCacheEngine> logger)
        {
            _loggerRepository = loggerRepository;
            _logger = logger;
        }

        public void Start()
        {
            try
            {
                _logger.LogInformation("Loading logs into cache");
                var result = _loggerRepository
                    .SearchAsync(
                        new FilterLogsRequest
                        {
                            Take = Capacity,
                            Asc = false,
                        })
                    .GetAwaiter()
                    .GetResult();

                _cache = result
                    .Select(p => new PostbackLogsCacheModel(p.RegistrationUId, p.EventType))
                    .Reverse()
                    .ToHashSet();
                _logger.LogInformation("Cache was updated: {Count} records were loaded", _cache.Count);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Could not load to cache last {Capacity} records from database", Capacity);
            }
        }

        public bool CheckAndUpdateCache(PostbackLogsCacheModel record)
        {
            if (_cache.Contains(record))
            {
                _logger.LogInformation("Cache already contains {Record}", record);
                return true;
            }
            
            if (_cache.Count == Capacity)
            {
                var recordToRemove = _cache.First();
                _cache.Remove(recordToRemove);
                _logger.LogInformation("Cache limit is reached: {RecordToRemove} was removed", recordToRemove);
            }

            _cache.Add(record);
            
            _logger.LogInformation("Cache was updated: {Record} was added", record);
            return false;
        }
    }
}