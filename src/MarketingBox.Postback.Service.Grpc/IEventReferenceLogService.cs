using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Sdk.Common.Models.Grpc;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IEventReferenceLogService
    {
        [OperationContract]
        Task<Response<IReadOnlyCollection<EventReferenceLog>>> GetAsync(ByAffiliateIdPaginatedRequest request);

        [OperationContract]
        Task<Response<IReadOnlyCollection<EventReferenceLog>>> SearchAsync(FilterLogsRequest request);
    }
}
