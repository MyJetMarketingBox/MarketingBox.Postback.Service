using MarketingBox.Postback.Service.Domain.Models;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IEventReferenceLoggerRepository
    {
        Task CreateAsync(EventReferenceLog eventReferenceLog);
        Task<EventReferenceLog[]> GetAsync(ByAffiliateIdPaginatedRequest request);
        Task<EventReferenceLog[]> SearchAsync(FilterLogsRequest filterLogsRequest);
    }
}