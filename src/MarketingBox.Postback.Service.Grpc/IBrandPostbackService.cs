using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Sdk.Common.Models.Grpc;

namespace MarketingBox.Postback.Service.Grpc;

[ServiceContract]
public interface IBrandPostbackService
{
    [OperationContract]
    Task<Response<bool>> ProcessRequestAsync(BrandPostbackRequest request);
}