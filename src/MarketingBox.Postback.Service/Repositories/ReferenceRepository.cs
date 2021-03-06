using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Sdk.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Postback.Service.Repositories
{
    public class ReferenceRepository : RepositoryBase<ReferenceRepository>, IReferenceRepository
    {
        private readonly IMapper _mapper;

        public ReferenceRepository(
            ILogger<ReferenceRepository> logger,
            DatabaseContextFactory factory,
            IMapper mapper) : base(logger, factory)
        {
            _mapper = mapper;
        }

        public async Task<Reference> DeleteAsync(long postbackId)
        {
            try
            {
                await using var context = _factory.Create();
                var entityToDelete = await context.References.FirstOrDefaultAsync(x => x.Id == postbackId);

                if (entityToDelete is null)
                {
                    throw new NotFoundException(nameof(postbackId), postbackId);
                }

                context.References.Remove(entityToDelete);

                await context.SaveChangesAsync();
                return entityToDelete;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while deleting reference. AffiliateId: {AffiliateId}.",
                    postbackId);
                throw;
            }
        }

        public async Task<(IReadOnlyCollection<Reference>, int)> SearchAsync(SearchReferenceRequest request)
        {
            try
            {
                await using var context = _factory.Create();

                var query = context.References.Include(x => x.Affiliate).AsQueryable();

                if (!string.IsNullOrEmpty(request.AffiliateName))
                {
                    query = query.Where(x =>
                        x.Affiliate.Name.ToLower().Contains(request.AffiliateName.ToLowerInvariant()));
                }

                if (!string.IsNullOrEmpty(request.TenantId))
                {
                    query = query.Where(x => x.TenantId.Equals(request.TenantId));
                }

                if (request.AffiliateIds.Any())
                {
                    query = query.Where(x =>
                        request.AffiliateIds.Contains(x.AffiliateId));
                }

                if (request.HttpQueryType.HasValue)
                {
                    query = query.Where(x => x.HttpQueryType == request.HttpQueryType);
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
                var result = query.ToList();

                return (result, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while processing request: {@SearchPostbackRequest}.", request);
                throw;
            }
        }

        public async Task<Reference> CreateAsync(CreateReferenceRequest request)
        {
            try
            {
                await using var context = _factory.Create();

                var affiliateInDb =
                    await context.References.FirstOrDefaultAsync(x => x.AffiliateId == request.AffiliateId);

                if (affiliateInDb != null)
                {
                    throw new AlreadyExistsException(nameof(request.AffiliateId), request.AffiliateId);
                }

                var reference = _mapper.Map<Reference>(request);
                await context.References.AddAsync(reference);
                await context.SaveChangesAsync();
                await context.Entry(reference).Reference(x => x.Affiliate).LoadAsync();

                return reference;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while saving reference. Request: {@CreateReferenceRequest}",
                    request);
                throw;
            }
        }

        public async Task<Reference> UpdateAsync(UpdateReferenceRequest request)
        {
            try
            {
                await using var context = _factory.Create();
                var entityToUpdate =
                    await context.References
                        .Include(x => x.Affiliate)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(x => x.Id == request.PostbackId);

                if (entityToUpdate == null)
                {
                    throw new NotFoundException(nameof(request.RequestedBy), request.RequestedBy);
                }

                entityToUpdate.DepositReference = request.DepositReference;
                entityToUpdate.DepositTGReference = request.DepositTGReference;
                entityToUpdate.RegistrationReference = request.RegistrationReference;
                entityToUpdate.RegistrationTGReference = request.RegistrationTGReference;
                entityToUpdate.HttpQueryType = request.HttpQueryType.Value;

                await context.SaveChangesAsync();

                return entityToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while updating reference. Request: {@CreateReferenceRequest}",
                    request);
                throw;
            }
        }

        public async Task<Reference> GetAsync(long postbackId)
        {
            try
            {
                await using var context = _factory.Create();
                var result = await context.References.Include(x => x.Affiliate)
                    .FirstOrDefaultAsync(x => x.Id == postbackId);

                if (result is null)
                {
                    throw new NotFoundException(nameof(postbackId), postbackId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while getting reference. AffiliateId: {AffiliateId}.",
                    postbackId);
                throw;
            }
        }
    }
}