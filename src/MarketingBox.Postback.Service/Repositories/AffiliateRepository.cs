using System;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Postback.Service.Repositories
{
    public class AffiliateRepository:RepositoryBase<AffiliateRepository>, IAffiliateRepository
    {
        public AffiliateRepository(
            ILogger<AffiliateRepository> logger,
            DatabaseContextFactory factory):base(logger,factory) 
        { }
        public async Task CreateAsync(Domain.Models.Affiliate affiliate)
        {
            try
            {
                await using var context = _factory.Create();
                await context.Affiliates.Upsert(affiliate).RunAsync();
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Exception while saving affiliate");
                throw;
            }
        }
    }
}