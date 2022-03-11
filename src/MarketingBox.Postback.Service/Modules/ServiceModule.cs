using Autofac;
using FluentValidation;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Engines;
using MarketingBox.Postback.Service.Postgres;
using MarketingBox.Postback.Service.Repositories;
using MarketingBox.Postback.Service.Subscribers;
using MarketingBox.Postback.Service.Validators;

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
                .RegisterType<AffiliateReferenceLoggerRepository>()
                .As<IAffiliateReferenceLoggerRepository>()
                .SingleInstance();
            builder
                .RegisterType<EventReferenceLoggerRepository>()
                .As<IEventReferenceLoggerRepository>()
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
            
            builder.RegisterType<AffiliateRepository>()
                .As<IAffiliateRepository>()
                .SingleInstance();
            
            builder.RegisterType<PostbackLogsCacheEngine>()
                .As<IStartable>()
                .As<IPostbackLogsCacheEngine>()
                .SingleInstance()
                .AutoActivate();
            
            builder.RegisterType<CreateOrUpdateReferenceRequestValidator>()
                .As<IValidator<CreateOrUpdateReferenceRequest>>()
                .SingleInstance();
            builder.RegisterType<ByAffiliateIdRequestValidator>()
                .As<IValidator<ByAffiliateIdRequest>>()
                .SingleInstance();
            builder.RegisterType<ByAffiliateIdPaginatedRequestValidator>()
                .As<IValidator<ByAffiliateIdPaginatedRequest>>()
                .SingleInstance();
            builder.RegisterType<FilterLogsRequestValidator>()
                .As<IValidator<FilterLogsRequest>>()
                .SingleInstance();
        }
    }
}