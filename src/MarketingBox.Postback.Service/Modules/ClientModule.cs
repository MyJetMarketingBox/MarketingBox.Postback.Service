using Autofac;
using MarketingBox.Affiliate.Service.Client;
using MarketingBox.Affiliate.Service.MyNoSql.Affiliates;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.Registration.Service.Client;
using MarketingBox.Registration.Service.Messages.Registrations;
using MarketingBox.Reporting.Service.Client;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;

namespace MarketingBox.Postback.Service.Modules
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var noSqlClient = builder.CreateNoSqlClient(
                Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort).Invoke(),
                new LoggerFactory());
            builder.RegisterMyNoSqlReader<AffiliateNoSql>(noSqlClient, AffiliateNoSql.TableName);

            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(
                Program.ReloadedSettings(e => e.MarketingBoxServiceBusHostPort),
                Program.LogFactory);

            builder.RegisterMyServiceBusPublisher<TrackingLinkUpdateRegistrationIdMessage>(
                serviceBusClient, TrackingLinkUpdateRegistrationIdMessage.Topic, false);

            builder.RegisterMyServiceBusSubscriberSingle<RegistrationUpdateMessage>(
                serviceBusClient,
                RegistrationUpdateMessage.Topic,
                "MarketingBox-Postback-Service",
                TopicQueueType.PermanentWithSingleConnection);

            builder.RegisterAffiliateServiceClient(Program.Settings.AffiliateServiceUrl);
            builder.RegisterReportingServiceClient(Program.Settings.ReportingServiceUrl);
            builder.RegisterRegistrationServiceClient(Program.Settings.RegistrationServiceUrl);
        }
    }
}