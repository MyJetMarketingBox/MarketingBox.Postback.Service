using Autofac;
using MarketingBox.Postback.Service.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.Postback.Service.Client
{
    public static class AutofacHelper
    {
        public static void RegisterAssetsDictionaryClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new PostbackServiceClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
