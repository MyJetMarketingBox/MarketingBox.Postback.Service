using MarketingBox.Registration.Service.Domain.Registrations;
using System.Threading.Tasks;
using RegistrationAdditionalInfo = MarketingBox.Registration.Service.Messages.Registrations.RegistrationAdditionalInfo;

namespace MarketingBox.Postback.Service.Engines
{
    public interface IRegistrationUpdateEngine
    {
        Task HandleRegistration(
            long affiliateId,
            RegistrationStatus status,
            RegistrationAdditionalInfo additionalInfo);
    }
}