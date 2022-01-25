using Autofac;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Engines;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Repositories;
using MarketingBox.Postback.Service.Subscribers;

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

            builder.RegisterType<RegistrationUpdateMessageSubscriber>()
                .As<IStartable>()
                .SingleInstance()
                .AutoActivate();

            builder.RegisterType<RegistrationUpdateEngine>()
                .As<IRegistrationUpdateEngine>()
                .SingleInstance();
        }
    }
}