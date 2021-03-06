using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Helper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarketingBox.Registration.Service.Messages.Registrations;
using MarketingBox.Sdk.Common.Enums;

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
                HttpResponseMessage postbackResponse;
                var affiliateId = message.RouteInfo.AffiliateId;
                var additionalInfo = message.AdditionalInfo;
                var registrationUId = message.GeneralInfoInternal.RegistrationUid;
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

                var referenceEntity = await _repository.GetAsync(affiliateId);

                var log = new EventReferenceLog
                {
                    AffiliateId = affiliateId,
                    RegistrationUId = registrationUId,
                    EventMessage = JsonConvert.SerializeObject(message),
                    EventType = eventType,
                    TenantId = message.TenantId
                };

                var reference = eventType switch
                {
                    EventType.Registered => referenceEntity.RegistrationReference,
                    EventType.Approved => referenceEntity.DepositReference,
                    _ => throw new ArgumentOutOfRangeException(nameof(eventType))
                };

                var postbackReference =
                    reference.ConfigureReference(additionalInfo);
                switch (referenceEntity.HttpQueryType)
                {
                    case HttpQueryType.Get:
                    {
                        using var client = new HttpClient();
                        postbackResponse = await client.GetAsync(postbackReference);
                        break;
                    }
                    case HttpQueryType.Post:
                    {
                        var data = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                        using var client = new HttpClient();
                        postbackResponse = await client.PostAsync(
                            postbackReference,
                            data);
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(referenceEntity.HttpQueryType));
                }

                log.PostbackReference = postbackReference;
                log.PostbackResponseStatus = postbackResponse is {StatusCode: HttpStatusCode.OK}
                    ? PostbackResponseStatus.Ok
                    : PostbackResponseStatus.Failed;
                log.PostbackResponse = JsonConvert.SerializeObject(postbackResponse);
                log.HttpQueryType = referenceEntity.HttpQueryType;
                log.Date = DateTime.UtcNow;

                await _eventReferenceLogger.CreateAsync(log);
                // telegram.Handle();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}