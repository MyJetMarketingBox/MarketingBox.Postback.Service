using Autofac;
using MarketingBox.Postback.Service.Engines;
using MarketingBox.Postback.Service.Subscribers;
using MarketingBox.Registration.Service.Messages;
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

            builder.RegisterMyServiceBusSubscriberSingle<RegistrationUpdateMessage>(
                serviceBusClient,
                Topics.RegistrationUpdateTopic,
                "MarketingBox-Postback-Service",
                TopicQueueType.PermanentWithSingleConnection);
        }
    }
}
