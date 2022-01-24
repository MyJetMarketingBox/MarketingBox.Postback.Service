using DotNetCoreDecorators;
using MarketingBox.Postback.Service.Engines;
using MarketingBox.Registration.Service.Messages.Registrations;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Subscribers
{
    public class RegistrationUpdateMessageSubscriber
    {
        private readonly IRegistrationUpdateEngine _registrationUpdateEngine;
        private readonly ILogger<RegistrationUpdateMessageSubscriber> _logger;

        public RegistrationUpdateMessageSubscriber(
            ISubscriber<RegistrationUpdateMessage> subscriber,
            IRegistrationUpdateEngine registrationUpdateEngine,
            ILogger<RegistrationUpdateMessageSubscriber> logger)
        {
            _registrationUpdateEngine = registrationUpdateEngine;
            _logger = logger;
            subscriber.Subscribe(HandleRegistrationEvent);
        }

        private async ValueTask HandleRegistrationEvent(RegistrationUpdateMessage message)
        {
            try
            {
                _logger.LogInformation("Handling message {@context}", message);

                await _registrationUpdateEngine.HandleRegistration(
                    message.RouteInfo.AffiliateId,
                    message.RouteInfo.Status,
                    message.AdditionalInfo);

                _logger.LogInformation("Has been handled {@context}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
