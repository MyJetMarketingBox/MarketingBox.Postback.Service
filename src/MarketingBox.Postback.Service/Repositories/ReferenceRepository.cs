using System;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Exceptions;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MarketingBox.Postback.Service.Repositories
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly ILogger<ReferenceRepository> _logger;
        private readonly DatabaseContextFactory _factory;

        public ReferenceRepository(
            ILogger<ReferenceRepository> logger,
            DatabaseContextFactory factory)
        {
            _logger = logger;
            this._factory = factory;
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
                var message = "Exception occured while deleting reference.";
                _logger.LogError(ex, message);
                if (ex is NotFoundException)
                {
                    throw;
                }
                throw new InternalException(message, ex);
            }
        }

        public async Task<Reference> SaveReferenceAsync(Reference request)
        {
            try
            {
                await using var context = _factory.Create();
                context.References.Upsert(
                        new Postgres.Entities.ReferenceEntity
                        {
                            AffiliateId = request.AffiliateId,
                            CallType = request.CallType,
                            DepositReference = request.DepositReference,
                            DepositTGReference = request.DepositTGReference,
                            RegistrationReference = request.RegistrationReference,
                            RegistrationTGReference = request.RegistrationTGReference,
                        });
                await context.SaveChangesAsync();

                return await GetReferenceAsync(request.AffiliateId);
            }
            catch (Exception ex)
            {
                var message = "Exception occured while saving reference.";
                _logger.LogError(ex, message);
                throw new InternalException(message, ex);
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

                entityToUpdate = new Postgres.Entities.ReferenceEntity
                {
                    AffiliateId = request.AffiliateId,
                    CallType = request.CallType,
                    DepositReference = request.DepositReference,
                    DepositTGReference = request.DepositTGReference,
                    RegistrationReference = request.RegistrationReference,
                    RegistrationTGReference = request.RegistrationTGReference,
                };
                await context.SaveChangesAsync();

                return await GetReferenceAsync(request.AffiliateId);
            }
            catch (Exception ex)
            {
                var message = "Exception occured while updating reference.";
                _logger.LogError(ex, message);
                if (ex is NotFoundException)
                {
                    throw;
                }
                throw new InternalException(message, ex);
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
                var message = "Exception occured while getting reference.";
                _logger.LogError(ex, message);
                if (ex is NotFoundException)
                {
                    throw;
                }
                throw new InternalException(message, ex);
            }
        }
    }
}
