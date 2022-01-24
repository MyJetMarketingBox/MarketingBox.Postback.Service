using MarketingBox.Registration.Service.Messages.Registrations;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Engines
{
    public interface IRegistrationUpdateEngine
    {
        Task HandleRegistration(long affiliateId, RegistrationAdditionalInfo additionalInfo);
    }
}