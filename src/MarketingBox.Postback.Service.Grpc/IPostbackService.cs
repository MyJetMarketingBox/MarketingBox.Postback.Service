using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IPostbackService
    {
        [OperationContract]
        Task<Response<ReferenceResponse>> GetReferenceAsync(ReferenceByAffiliateRequest request);

        [OperationContract]
        Task<Response<ReferenceResponse>> CreateReferenceAsync(FullReferenceRequest request);

        [OperationContract]
        Task<Response<ReferenceResponse>> UpdateReferenceAsync(FullReferenceRequest request);

        [OperationContract]
        Task<Response<bool>> DeleteReferenceAsync(ReferenceByAffiliateRequest request);
    }
}