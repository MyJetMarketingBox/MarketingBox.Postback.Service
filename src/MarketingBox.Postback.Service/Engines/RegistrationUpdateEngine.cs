using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Helper;
using MarketingBox.Registration.Service.Messages.Registrations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task HandleRegistration(long affiliateId, RegistrationAdditionalInfo additionalInfo)
        {
            try
            {
                var reference = await _repository.GetReferenceAsync(affiliateId);
                HttpResponseMessage registrationResponse = null;

                switch (reference.HttpQueryType)
                {
                    case HttpQueryType.Get:
                        {
                            var registrationReference = 
                                reference.RegistrationReference.ConfigureReference(additionalInfo);

                            using var client = new HttpClient();
                            registrationResponse = await client.GetAsync(registrationReference);
                            break;
                        }
                    case HttpQueryType.Post:
                        {
                            var json = JsonConvert.SerializeObject(additionalInfo);
                            var data = new StringContent(json, Encoding.UTF8, "application/json");
                            
                            using var client = new HttpClient();
                            registrationResponse = await client.PostAsync(
                                reference.RegistrationReference,
                                data);
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
