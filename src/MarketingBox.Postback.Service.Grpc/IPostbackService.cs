using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Sdk.Common.Models.Grpc;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IPostbackService
    {
        [OperationContract]
        Task<Response<Reference>> GetAsync(ByAffiliateIdRequest request);

        [OperationContract]
        Task<Response<Reference>> CreateAsync(CreateOrUpdateReferenceRequest request);

        [OperationContract]
        Task<Response<Reference>> UpdateAsync(CreateOrUpdateReferenceRequest request);

        [OperationContract]
        Task<Response<bool>> DeleteAsync(ByAffiliateIdRequest request);
    }
}