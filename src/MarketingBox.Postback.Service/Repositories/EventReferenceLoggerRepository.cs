using AutoMapper;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Exceptions;

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

        public async Task CreateAsync(EventReferenceLog eventReferenceLog)
        {
            try
            {
                await using var context = _factory.Create();
                await context.EventReferenceLogs.AddAsync(_mapper.Map<EventReferenceLogEntity>(eventReferenceLog));
                await context.SaveChangesAsync();

                _logger.LogInformation("Log {eventReferenceLog} was created.", JsonConvert.SerializeObject(eventReferenceLog));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        public async Task<EventReferenceLog[]> GetAsync(long affiliateId)
        {
            try
            {
                await using var context = _factory.Create();
                var result = context.EventReferenceLogs.Where(x => x.AffiliateId == affiliateId).ToArray();
                if (result.Length == 0)
                {
                    throw new NotFoundException(nameof(affiliateId), affiliateId);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<EventReferenceLog[]> SearchAsync(FilterLogsRequest request)
        {
            try
            {
                await using var context = _factory.Create();
                var query = context.EventReferenceLogs.AsQueryable();
                if(request.AffiliateId.HasValue)
                {
                    query = query.Where(x=> x.AffiliateId == request.AffiliateId.Value);
                }
                if (request.EventStatus.HasValue)
                {
                    query = query.Where(x => x.EventStatus == request.EventStatus.Value);
                }
                if (request.HttpQueryType.HasValue)
                {
                    query = query.Where(x => x.HttpQueryType == request.HttpQueryType.Value);
                }
                if (request.FromDate.HasValue)
                {
                    query = query.Where(x => x.Date >= request.FromDate.Value);
                }
                if (request.ToDate.HasValue)
                {
                    query = query.Where(x => x.Date <= request.ToDate.Value.Add(new TimeSpan(23, 59, 59)));
                }

                var limit = request.Take <= 0 ? 1000 : request.Take;
                if (request.Asc)
                {
                    if (request.Cursor != null)
                    {
                        query = query.Where(x => x.Id > request.Cursor);
                    }

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (request.Cursor != null)
                    {
                        query = query.Where(x => x.Id < request.Cursor);
                    }

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                await query.LoadAsync();

                return query.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
