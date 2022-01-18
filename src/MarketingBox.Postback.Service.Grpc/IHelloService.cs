using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}