﻿using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using MarketingBox.Sdk.Common.Exceptions;

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

                _logger.LogInformation("Log {eventReferenceLog} was created.",
                    JsonConvert.SerializeObject(eventReferenceLog));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<EventReferenceLog[]> GetAsync(ByAffiliateIdPaginatedRequest request)
        {
            try
            {
                await using var context = _factory.Create();
                var query = context.EventReferenceLogs
                    .Where(x => x.AffiliateId == request.AffiliateId)
                    .Include(x => x.Affiliate)
                    .AsQueryable();

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
                    query = request.Cursor != null
                        ? query.Where(x => x.Id < request.Cursor)
                        : query.Where(x => x.Id < long.MaxValue);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                await query.LoadAsync();
                var result = query.ToArray();

                if (result.Length == 0)
                {
                    throw new NotFoundException(nameof(request.AffiliateId), request.AffiliateId);
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
                var query = context.EventReferenceLogs
                    .Include(x => x.Affiliate)
                    .AsQueryable();
                if (request.AffiliateId.HasValue)
                {
                    query = query.Where(x => x.AffiliateId == request.AffiliateId.Value);
                }

                if (request.EventType.HasValue)
                {
                    query = query.Where(x => x.EventType == request.EventType.Value);
                }

                if (request.HttpQueryType.HasValue)
                {
                    query = query.Where(x => x.HttpQueryType == request.HttpQueryType.Value);
                }

                if (request.ResponseStatus.HasValue)
                {
                    query = query.Where(x => x.PostbackResponseStatus == request.ResponseStatus.Value);
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
                    query = request.Cursor != null
                        ? query.Where(x => x.Id < request.Cursor)
                        : query.Where(x => x.Id < long.MaxValue);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                await query.LoadAsync();
                var result = query.ToArray();
                if (result.Length == 0)
                {
                    throw new NotFoundException("There is no entity for such request.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}