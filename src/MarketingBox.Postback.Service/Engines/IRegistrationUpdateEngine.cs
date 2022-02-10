using System.Threading.Tasks;
using MarketingBox.Registration.Service.Messages.Registrations;

namespace MarketingBox.Postback.Service.Engines
{
    public interface IRegistrationUpdateEngine
    {
        Task HandleRegistration(RegistrationUpdateMessage message);
    }
}