using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Domain
{
    public record PostbackLogsCacheModel(
        string registrationUid,
        EventType eventType);
}