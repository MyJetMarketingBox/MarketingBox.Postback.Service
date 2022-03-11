﻿using System;
using System.Text.Json;
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
            IMapper mapper) : base(logger,factory)
        {
            _mapper = mapper;
        }

        public async Task<long> DeleteAsync(long affiliateId)
        {
            try
            {
                await using var context = _factory.Create();
                var entityToDelete = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == affiliateId);
                
                if (entityToDelete is null)
                {
                    throw new NotFoundException(nameof(affiliateId), affiliateId);
                }
                var id = entityToDelete.Id;
                context.References.Remove(entityToDelete);

                await context.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while deleting reference. AffiliateId: {AffiliateId}.",
                    affiliateId);
                throw;
            }
        }

        public async Task<Reference> CreateAsync(CreateOrUpdateReferenceRequest request)
        {
            try
            {
                await using var context = _factory.Create();

                var affiliateInDb = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == request.AffiliateId);

                if (affiliateInDb != null)
                {
                    throw new AlreadyExistsException(nameof(request.AffiliateId), request.AffiliateId);
                }
                await context.References.AddAsync(_mapper.Map<Reference>(request));
                await context.SaveChangesAsync();

                return await GetAsync(request.AffiliateId.Value);
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

        public async Task<Reference> UpdateAsync(CreateOrUpdateReferenceRequest request)
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
                entityToUpdate.HttpQueryType = request.HttpQueryType.Value;

                await context.SaveChangesAsync();

                return await GetAsync(request.AffiliateId.Value);
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

        public async Task<Reference> GetAsync(long affiliateId)
        {
            try
            {
                await using var context = _factory.Create();
                var result = await context.References.Include(x => x.Affiliate)
                    .FirstOrDefaultAsync(x => x.AffiliateId == affiliateId);

                if (result is null)
                {
                    throw new NotFoundException(nameof(affiliateId), affiliateId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occured while getting reference. AffiliateId: {AffiliateId}.",
                    affiliateId);
                throw;
            }
        }
    }
}
