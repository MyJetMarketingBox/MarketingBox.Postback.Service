using MarketingBox.Postback.Service.Grpc.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IEventReferenceLogService
    {
        [OperationContract]
        Task<Response<IReadOnlyCollection<EventReferenceLog>>> GetLogsAsync(ByAffiliateIdPaginatedRequest request);

        [OperationContract]
        Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchLogsAsync(FilterLogsRequest request);
    }
}
