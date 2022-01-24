using MarketingBox.Registration.Service.Messages.Registrations;

namespace MarketingBox.Postback.Service.Helper
{
    public static class ReferenceHelper
    {
        public static string ConfigureReference(
            this string reference,
            RegistrationAdditionalInfo additionalInfo)
        {
            return reference
                .Replace("{Sub1}", additionalInfo.Sub1)
                .Replace("{Sub2}", additionalInfo.Sub2)
                .Replace("{Sub3}", additionalInfo.Sub3)
                .Replace("{Sub4}", additionalInfo.Sub4)
                .Replace("{Sub5}", additionalInfo.Sub5)
                .Replace("{Sub6}", additionalInfo.Sub6)
                .Replace("{Sub7}", additionalInfo.Sub7)
                .Replace("{Sub8}", additionalInfo.Sub8)
                .Replace("{Sub9}", additionalInfo.Sub9)
                .Replace("{Sub10}", additionalInfo.Sub10)
                .Replace("{Funnel}", additionalInfo.Funnel)
                .Replace("{AffCode}", additionalInfo.AffCode);
        }
    }
}
