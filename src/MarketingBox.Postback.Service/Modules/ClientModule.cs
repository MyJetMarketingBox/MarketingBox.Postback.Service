using Autofac;
using MarketingBox.Affiliate.Service.Client;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.Registration.Service.Messages.Registrations;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;

namespace MarketingBox.Postback.Service.Modules
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
        }
    }
}
