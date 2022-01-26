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

        public IPostbackService GetPostbackService() => CreateGrpcService<IPostbackService>();
        public IEventReferenceLogService GetEventReferenceLogService() => CreateGrpcService<IEventReferenceLogService>();
    }
}
