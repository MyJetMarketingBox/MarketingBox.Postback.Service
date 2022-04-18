using Autofac;
using MarketingBox.Postback.Service.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.Postback.Service.Client
{
    public static class AutofacHelper
    {
        public static void RegisterPostbackServiceClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new PostbackServiceClientFactory(grpcServiceUrl);

            builder
                .RegisterInstance(factory.GetPostbackService())
                .As<IPostbackService>()
                .SingleInstance();
            builder
                .RegisterInstance(factory.GetEventReferenceLogService())
                .As<IEventReferenceLogService>()
                .SingleInstance();
            builder
                .RegisterInstance(factory.GetBrandPostbackService())
                .As<IBrandPostbackService>()
                .SingleInstance();
        }
    }
}
