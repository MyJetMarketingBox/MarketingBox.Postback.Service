using MarketingBox.Postback.Service.Domain.Models;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IEventReferenceLoggerRepository
    {
        Task CreateLogAsync(EventReferenceLog eventReferenceLog);
    }
}