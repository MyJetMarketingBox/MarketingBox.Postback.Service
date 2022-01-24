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

        public RegistrationUpdateEngine(
            ILogger<RegistrationUpdateEngine> logger,
            IReferenceRepository repository)
        {
            _logger = logger;
            _repository = repository;
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
                
                switch (status)
                {
                    case RegistrationStatus.Registered:
                        reference = referenceEntity.RegistrationReference;
                        break;
                    case RegistrationStatus.Deposited:
                        reference = referenceEntity.DepositReference;
                        break;
                }

                switch (referenceEntity.HttpQueryType)
                {
                    case HttpQueryType.Get:
                        {
                            var registrationReference = 
                                reference.ConfigureReference(additionalInfo);

                            using var client = new HttpClient();
                            postbackResponse = await client.GetAsync(registrationReference);
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
                            break;
                        }
                }
                // telegram.Handle();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
