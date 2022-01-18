using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using MarketingBox.Postback.Service.Grpc;

namespace MarketingBox.Postback.Service.Client
{
    [UsedImplicitly]
    public class PostbackServiceClientFactory: MyGrpcClientFactory
    {
        public PostbackServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
