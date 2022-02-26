using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IAffiliateRepository
    {
        Task CreateAsync(Domain.Models.Affiliate affiliate);
    }
}