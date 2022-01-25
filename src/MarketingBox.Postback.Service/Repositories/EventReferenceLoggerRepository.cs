using AutoMapper;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Postgres.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Repositories
{
    public class EventReferenceLoggerRepository : 
        RepositoryBase<EventReferenceLoggerRepository>,
        IEventReferenceLoggerRepository
    {
        private readonly IMapper _mapper;

        public EventReferenceLoggerRepository(
            ILogger<EventReferenceLoggerRepository> logger,
            DatabaseContextFactory factory,
            IMapper mapper) : base(logger, factory)
        {
            _mapper = mapper;
        }

        public async Task CreateLogAsync(EventReferenceLog eventReferenceLog)
        {
            try
            {
                await using var context = _factory.Create();
                await context.EventReferenceLogs.AddAsync(_mapper.Map<EventReferenceLogEntity>(eventReferenceLog));
                await context.SaveChangesAsync();

                _logger.LogInformation("Event {Event} was logged.", eventReferenceLog.EventStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
