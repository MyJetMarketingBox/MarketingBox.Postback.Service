using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Repositories
{
    public class EventReferenceLoggerRepository :
        RepositoryBase<EventReferenceLoggerRepository>,
        IEventReferenceLoggerRepository
    {
        public EventReferenceLoggerRepository(
            ILogger<EventReferenceLoggerRepository> logger,
            DatabaseContextFactory factory) : base(logger, factory)
        {
        }

        public async Task CreateAsync(EventReferenceLog eventReferenceLog)
        {
            try
            {
                await using var context = _factory.Create();
                await context.EventReferenceLogs.AddAsync(eventReferenceLog);
                await context.SaveChangesAsync();

                _logger.LogInformation("Log {@EventReferenceLog} was created.", eventReferenceLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(EventReferenceLog[], int)> SearchAsync(FilterLogsRequest request)
        {
            try
            {
                await using var context = _factory.Create();
                var query = context.EventReferenceLogs
                    .Include(x => x.Affiliate)
                    .AsQueryable();
                if (request.AffiliateIds.Any())
                {
                    query = query.Where(x => request.AffiliateIds.Contains(x.AffiliateId));
                }

                if (!string.IsNullOrEmpty(request.AffiliateName))
                {
                    query = query.Where(x =>
                        x.Affiliate.Name.ToLower().Contains(request.AffiliateName.ToLowerInvariant()));
                }

                if (!string.IsNullOrEmpty(request.TenantId))
                {
                    query = query.Where(x =>
                        x.TenantId.Equals(request.TenantId));
                }

                if (!string.IsNullOrEmpty(request.RegistrationUId))
                {
                    query = query.Where(x =>
                        x.RegistrationUId.ToLower().Contains(request.RegistrationUId.ToLowerInvariant()));
                }

                if (request.EventType.HasValue)
                {
                    query = query.Where(x => x.EventType == request.EventType.Value);
                }

                if (request.FromDate.HasValue)
                {
                    query = query.Where(x => x.Date >= request.FromDate.Value);
                }

                if (request.ToDate.HasValue)
                {
                    query = query.Where(x => x.Date <= request.ToDate.Value.Add(new TimeSpan(23, 59, 59)));
                }

                var total = query.Count();
                if (request.Asc)
                {
                    if (request.Cursor.HasValue)
                    {
                        query = query.Where(x => x.Id > request.Cursor);
                    }

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (request.Cursor.HasValue)
                    {
                        query = query.Where(x => x.Id < request.Cursor);
                    }

                    query = query.OrderByDescending(x => x.Id);
                }

                if (request.Take.HasValue)
                {
                    query = query.Take(request.Take.Value);
                }

                await query.LoadAsync();
                var result = query.ToArray();

                return (result, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}