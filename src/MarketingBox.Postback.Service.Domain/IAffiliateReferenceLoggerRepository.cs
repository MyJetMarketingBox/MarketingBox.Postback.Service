using MarketingBox.Postback.Service.Domain.Models;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IAffiliateReferenceLoggerRepository
    {
        Task CreateAsync(long affiliateId, long referenceId, string tenantId, OperationType operationType);
    }
}