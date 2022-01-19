using System;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Grpc.Models;
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

                }
                context.References.Remove(entityToDelete);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReferenceResponse> SaveReferenceAsync(FullReferenceRequest request)
        {
            try
            {
                await using var context = _factory.Create();
                await context.References.AddAsync(
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReferenceResponse> UpdateReferenceAsync(FullReferenceRequest request)
        {
            try
            {
                await using var context = _factory.Create();
                await context.References.AddAsync(
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReferenceResponse> GetReferenceAsync(long AffiliateId)
        {
            try
            {
                await using var context = _factory.Create();
                var result = await context.References.FirstOrDefaultAsync(x => x.AffiliateId == AffiliateId);

                if (result is null)
                { }

                return new ReferenceResponse
                {
                    AffiliateId = result.AffiliateId,
                    CallType = result.CallType,
                    DepositReference = result.DepositReference,
                    DepositTGReference = result.DepositTGReference,
                    RegistrationReference = result.RegistrationReference,
                    RegistrationTGReference = result.RegistrationTGReference,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
