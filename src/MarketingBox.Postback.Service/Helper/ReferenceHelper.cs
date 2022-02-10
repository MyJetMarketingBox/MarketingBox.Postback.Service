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
                .Replace("{sub1}", string.IsNullOrEmpty(additionalInfo.Sub1) ? "{sub1}": additionalInfo.Sub1)
                .Replace("{sub2}", string.IsNullOrEmpty(additionalInfo.Sub2) ? "{sub2}": additionalInfo.Sub2)
                .Replace("{sub3}", string.IsNullOrEmpty(additionalInfo.Sub3) ? "{sub3}": additionalInfo.Sub3)
                .Replace("{sub4}", string.IsNullOrEmpty(additionalInfo.Sub4) ? "{sub4}": additionalInfo.Sub4)
                .Replace("{sub5}", string.IsNullOrEmpty(additionalInfo.Sub5) ? "{sub5}": additionalInfo.Sub5)
                .Replace("{sub6}", string.IsNullOrEmpty(additionalInfo.Sub6) ? "{sub6}": additionalInfo.Sub6)
                .Replace("{sub7}", string.IsNullOrEmpty(additionalInfo.Sub7) ? "{sub7}": additionalInfo.Sub7)
                .Replace("{sub8}", string.IsNullOrEmpty(additionalInfo.Sub8) ? "{sub8}": additionalInfo.Sub8)
                .Replace("{sub9}", string.IsNullOrEmpty(additionalInfo.Sub9) ? "{sub9}": additionalInfo.Sub9)
                .Replace("{sub10}", string.IsNullOrEmpty(additionalInfo.Sub10)? "{sub10}": additionalInfo.Sub10)
                .Replace("{funnel}", string.IsNullOrEmpty(additionalInfo.Funnel) ? "{funnel}" : additionalInfo.Funnel)
                .Replace("{affcode}", string.IsNullOrEmpty(additionalInfo.AffCode) ? "{affcode}" : additionalInfo.AffCode);
        }
    }
}
