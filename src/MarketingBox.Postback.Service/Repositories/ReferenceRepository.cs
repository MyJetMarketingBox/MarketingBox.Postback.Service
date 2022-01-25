using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Exceptions;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MarketingBox.Postback.Service.Repositories
{
    public class ReferenceRepository : RepositoryBase<ReferenceRepository>, IReferenceRepository
    {
        private readonly IMapper _mapper;

        public ReferenceRepository(
            ILogger<ReferenceRepository> logger,
            IMapper mapper,
            DatabaseContextFactory factory) : base(logger,factory)
        {
            _mapper = mapper;
        }

        public async Task DeleteReferenceAsync(long AffiliateId)
        {
            try
            {
                await using var context = _factory.Create();
                var entityToDelete = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == AffiliateId);

                if (entityToDelete is null)
                {
                    throw new NotFoundException(nameof(AffiliateId), AffiliateId);
                }

                context.References.Remove(entityToDelete);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while deleting reference. AffiliateId: {AffiliateId}.",
                    AffiliateId);
                throw;
            }
        }

        public async Task<Reference> CreateReferenceAsync(Reference request)
        {
            try
            {
                await using var context = _factory.Create();

                var entityToCreate = _mapper.Map<ReferenceEntity>(request);
                var affiliateInDb = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == request.AffiliateId);

                if (affiliateInDb != null)
                {
                    throw new AlreadyExistsException(nameof(request.AffiliateId), request.AffiliateId);
                }
                await context.References.AddAsync(entityToCreate);
                await context.SaveChangesAsync();

                return await GetReferenceAsync(request.AffiliateId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while saving reference. Request: {CreateReferenceRequest}.",
                    JsonSerializer.Serialize(request));
                throw;
            }
        }

        public async Task<Reference> UpdateReferenceAsync(Reference request)
        {
            try
            {
                await using var context = _factory.Create();
                var entityToUpdate = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == request.AffiliateId);

                if (entityToUpdate == null)
                {
                    throw new NotFoundException(nameof(request.AffiliateId), request.AffiliateId);
                }

                entityToUpdate.DepositReference = request.DepositReference;
                entityToUpdate.DepositTGReference = request.DepositTGReference;
                entityToUpdate.RegistrationReference = request.RegistrationReference;
                entityToUpdate.RegistrationTGReference = request.RegistrationTGReference;
                entityToUpdate.HttpQueryType = request.HttpQueryType;

                await context.SaveChangesAsync();

                return await GetReferenceAsync(request.AffiliateId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while updating reference. Request: {CreateReferenceRequest}.",
                    JsonSerializer.Serialize(request));
                throw;
            }
        }

        public async Task<Reference> GetReferenceAsync(long AffiliateId)
        {
            try
            {
                await using var context = _factory.Create();
                var result = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == AffiliateId);

                if (result is null)
                {
                    throw new NotFoundException(nameof(AffiliateId), AffiliateId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while getting reference. AffiliateId: {AffiliateId}.",
                    AffiliateId);
                throw;
            }
        }
    }
}
