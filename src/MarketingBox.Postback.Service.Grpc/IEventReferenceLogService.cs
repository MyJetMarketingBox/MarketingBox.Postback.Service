using MarketingBox.Postback.Service.Grpc.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IEventReferenceLogService
    {
        [OperationContract]
        Task<Response<IReadOnlyCollection<EventReferenceLog>>> GetLogsAsync(ByAffiliateIdRequest request);

        [OperationContract]
        Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchLogsAsync(FilterLogsRequest request);
    }
}
