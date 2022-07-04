using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IAffiliateRepository
    {
        Task UpsertAsync(Models.Affiliate affiliate);
    }
}