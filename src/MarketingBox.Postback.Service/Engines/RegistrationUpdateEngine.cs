using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Helper;
using MarketingBox.Registration.Service.Domain.Registrations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RegistrationAdditionalInfo = MarketingBox.Registration.Service.Messages.Registrations.RegistrationAdditionalInfo;

namespace MarketingBox.Postback.Service.Engines
{
    public class RegistrationUpdateEngine : IRegistrationUpdateEngine
    {
        private readonly ILogger<RegistrationUpdateEngine> _logger;
        private readonly IReferenceRepository _repository;
        private readonly IEventReferenceLoggerRepository _eventReferenceLogger;

        public RegistrationUpdateEngine(
            ILogger<RegistrationUpdateEngine> logger,
            IReferenceRepository repository,
            IEventReferenceLoggerRepository eventReferenceLogger)
        {
            _logger = logger;
            _repository = repository;
            _eventReferenceLogger = eventReferenceLogger;
        }

        public async Task HandleRegistration(
            long affiliateId,
            RegistrationStatus status,
            RegistrationAdditionalInfo additionalInfo)
        {
            try
            {
                HttpResponseMessage postbackResponse = null;
                string reference = string.Empty;

                var referenceEntity = await _repository.GetReferenceAsync(affiliateId);
                var log = new EventReferenceLog();


                switch (status)
                {
                    case RegistrationStatus.Registered:
                        reference = referenceEntity.RegistrationReference;
                        log.EventStatus = Status.Registered;
                        break;
                    case RegistrationStatus.Deposited:
                        reference = referenceEntity.DepositReference;
                        log.EventStatus = Status.Deposited;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(status));
                }


                switch (referenceEntity.HttpQueryType)
                {
                    case HttpQueryType.Get:
                        {
                            var registrationReference = 
                                reference.ConfigureReference(additionalInfo);

                            using var client = new HttpClient();
                            postbackResponse = await client.GetAsync(registrationReference);

                            log.PostbackReference = registrationReference;
                            break;
                        }
                    case HttpQueryType.Post:
                        {
                            var json = JsonConvert.SerializeObject(additionalInfo);
                            var data = new StringContent(json, Encoding.UTF8, "application/json");
                            
                            using var client = new HttpClient();
                            postbackResponse = await client.PostAsync(
                                reference,
                                data);
                            log.PostbackReference = reference;
                            break;
                        }
                }

                log.PostbackResult = JsonConvert.SerializeObject(postbackResponse);
                log.HttpQueryType = referenceEntity.HttpQueryType;
                log.Date = DateTime.UtcNow;

                await _eventReferenceLogger.CreateLogAsync(log);
                // telegram.Handle();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
