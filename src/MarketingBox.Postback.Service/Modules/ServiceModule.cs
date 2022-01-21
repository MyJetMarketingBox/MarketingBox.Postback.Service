using Autofac;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Repositories;

namespace MarketingBox.Postback.Service.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ReferenceRepository>()
                .As<IReferenceRepository>()
                .SingleInstance();

            builder
                .RegisterType<DatabaseContextFactory>()
                .AsSelf()
                .SingleInstance();
        }
    }
}