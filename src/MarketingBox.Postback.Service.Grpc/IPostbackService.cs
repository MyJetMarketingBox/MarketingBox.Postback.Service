using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IPostbackService
    {
        [OperationContract]
        Task<Response<Reference>> GetReferenceAsync(ByAffiliateIdRequest request);

        [OperationContract]
        Task<Response<Reference>> CreateReferenceAsync(Reference request);

        [OperationContract]
        Task<Response<Reference>> UpdateReferenceAsync(Reference request);

        [OperationContract]
        Task<Response<bool>> DeleteReferenceAsync(ByAffiliateIdRequest request);
    }
}