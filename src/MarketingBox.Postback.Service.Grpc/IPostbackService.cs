using System.Collections.Generic;
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
        Task<Response<Reference>> GetAsync(ByIdRequest request);
        [OperationContract]
        Task<Response<IReadOnlyCollection<Reference>>> SearchAsync(SearchReferenceRequest request);

        [OperationContract]
        Task<Response<Reference>> CreateAsync(CreateReferenceRequest request);

        [OperationContract]
        Task<Response<Reference>> UpdateAsync(UpdateReferenceRequest request);

        [OperationContract]
        Task<Response<bool>> DeleteAsync(ByIdRequest request);
    }
}