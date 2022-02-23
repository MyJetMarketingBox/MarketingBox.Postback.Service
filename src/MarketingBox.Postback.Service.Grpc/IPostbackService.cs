using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Sdk.Common.Models.Grpc;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IPostbackService
    {
        [OperationContract]
        Task<Response<Reference>> GetAsync(ByAffiliateIdRequest request);

        [OperationContract]
        Task<Response<Reference>> CreateAsync(Reference request);

        [OperationContract]
        Task<Response<Reference>> UpdateAsync(Reference request);

        [OperationContract]
        Task<Response<bool>> DeleteAsync(ByAffiliateIdRequest request);
    }
}