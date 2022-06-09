using MarketingBox.Postback.Service.Domain.Models;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IEventReferenceLoggerRepository
    {
        Task CreateAsync(EventReferenceLog eventReferenceLog);
        Task<(EventReferenceLog[], int total)> SearchAsync(FilterLogsRequest filterLogsRequest);
    }
}