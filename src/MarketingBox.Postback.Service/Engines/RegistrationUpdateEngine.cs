using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Helper;
using MarketingBox.Registration.Service.Domain.Registrations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarketingBox.Registration.Service.Messages.Registrations;

namespace MarketingBox.Postback.Service.Engines
{
    public class RegistrationUpdateEngine : IRegistrationUpdateEngine
    {
        private readonly ILogger<RegistrationUpdateEngine> _logger;
        private readonly IReferenceRepository _repository;
        private readonly IEventReferenceLoggerRepository _eventReferenceLogger;
        private readonly IPostbackLogsCacheEngine _cache;

        public RegistrationUpdateEngine(
            ILogger<RegistrationUpdateEngine> logger,
            IReferenceRepository repository,
            IEventReferenceLoggerRepository eventReferenceLogger,
            IPostbackLogsCacheEngine cache)
        {
            _logger = logger;
            _repository = repository;
            _eventReferenceLogger = eventReferenceLogger;
            _cache = cache;
        }

        public async Task HandleRegistration(RegistrationUpdateMessage message)
        {
            try
            {
                HttpResponseMessage postbackResponse = null;
                var affiliateId = message.RouteInfo.AffiliateId;
                var additionalInfo = message.AdditionalInfo;
                var registrationUId = message.GeneralInfo.RegistrationUId;
                EventType eventType;
                switch (message.RouteInfo.Status)
                {
                    case RegistrationStatus.Registered:
                        eventType = EventType.Registered;
                        break;
                    case RegistrationStatus.Approved:
                        eventType = EventType.Approved;
                        break;
                    default:
                        _logger.LogWarning(
                            "Message with {status} status was ignored. Only messages with 'Registered' and 'Approved' statuses are handled.",
                            message.RouteInfo.Status);
                        return;
                }
                
                if (_cache.CheckAndUpdateCache(new PostbackLogsCacheModel(registrationUId, eventType)))
                {
                    return;
                }

                var referenceEntity = await _repository.GetReferenceAsync(affiliateId);

                var log = new EventReferenceLog
                {
                    AffiliateId = affiliateId,
                    RegistrationUId = registrationUId,
                    EventMessage = JsonConvert.SerializeObject(message),
                    EventType = eventType
                };

                var reference = eventType switch
                {
                    EventType.Registered => referenceEntity.RegistrationReference,
                    EventType.Approved => referenceEntity.DepositReference,
                    _ => throw new ArgumentOutOfRangeException(nameof(eventType))
                };


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
                            log.RequestBody = json; 
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(referenceEntity.HttpQueryType));
                }

                log.ResponseStatus = postbackResponse is {StatusCode: HttpStatusCode.OK}
                    ? ResponseStatus.Ok
                    : ResponseStatus.Failed;
                log.PostbackResponse = JsonConvert.SerializeObject(postbackResponse);
                log.HttpQueryType = referenceEntity.HttpQueryType;
                log.Date = DateTime.UtcNow;

                await _eventReferenceLogger.CreateAsync(log);
                // telegram.Handle();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
