using MarketingBox.Postback.Service.Domain.Models;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IAffiliateReferenceLoggerRepository
    {
        Task CreateLogAsync(long affiliateId, OperationType operationType);
    }
}