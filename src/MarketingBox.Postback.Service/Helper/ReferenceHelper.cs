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
                .ToLowerInvariant()
                .Replace("{sub1}", additionalInfo.Sub1)
                .Replace("{sub2}", additionalInfo.Sub2)
                .Replace("{sub3}", additionalInfo.Sub3)
                .Replace("{sub4}", additionalInfo.Sub4)
                .Replace("{sub5}", additionalInfo.Sub5)
                .Replace("{sub6}", additionalInfo.Sub6)
                .Replace("{sub7}", additionalInfo.Sub7)
                .Replace("{sub8}", additionalInfo.Sub8)
                .Replace("{sub9}", additionalInfo.Sub9)
                .Replace("{sub10}", additionalInfo.Sub10)
                .Replace("{funnel}", additionalInfo.Funnel)
                .Replace("{affcode}", additionalInfo.AffCode);
        }
    }
}
