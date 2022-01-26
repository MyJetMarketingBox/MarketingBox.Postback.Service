using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Postgres.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Repositories
{
    public class AffiliateReferenceLoggerRepository : 
        RepositoryBase<AffiliateReferenceLoggerRepository>,
        IAffiliateReferenceLoggerRepository
    {
        public AffiliateReferenceLoggerRepository(
            ILogger<AffiliateReferenceLoggerRepository> logger, 
            DatabaseContextFactory factory) : base(logger,factory)
        {
        }

        public async Task CreateAsync(long affiliateId, OperationType operationType)
        {
            try
            {
                await using var context = _factory.Create();
                await context.AffiliateReferenceLogs.AddAsync(
                    new AffiliateReferenceLogEntity
                    {
                        AffiliateId = affiliateId,
                        Date = DateTime.UtcNow,
                        Operation = operationType
                    });
                await context.SaveChangesAsync();


            _logger.LogInformation("Operation {Operation} was logged for affiliate {AffiliateId}.", operationType, affiliateId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
