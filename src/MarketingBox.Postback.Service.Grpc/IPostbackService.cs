using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IPostbackService
    {
        [OperationContract]
        Task<ReferenceResponse> GetReferenceAsync(ReferenceByAffiliateRequest request);

        [OperationContract]
        Task<ReferenceResponse> SaveReferenceAsync(FullReferenceRequest request);

        [OperationContract]
        Task<ReferenceResponse> UpdateReferenceAsync(FullReferenceRequest request);

        [OperationContract]
        Task DeleteReferenceAsync(ReferenceByAffiliateRequest request);
    }

    public record Request(string name,int age);
}