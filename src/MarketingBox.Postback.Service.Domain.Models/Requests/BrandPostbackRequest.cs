using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
using MarketingBox.Sdk.Common.Enums;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class BrandPostbackRequest : ValidatableEntity
    {
        [DataMember(Order = 1), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long ClickId { get; set; }

        [DataMember(Order = 2), Required, IsEnum]
        public BrandEventType? EventType { get; set; }

        [DataMember(Order = 3), RequiredOnlyIf(nameof(EventType), BrandEventType.ChangedCrm)]
        public CrmStatus? CrmStatus { get; set; }

        [DataMember(Order = 4), Required] 
        public GeneralInfo GeneralInfo { get; set; }

        [DataMember(Order = 6)]
        public AdditionalInfo AdditionalInfo { get; set; }
        
        [DataMember(Order = 7)]
        public RegistrationBrandInfo RegistrationBrandInfo { get; set; }
    }

    [DataContract]
    public class AdditionalInfo
    {
        [DataMember(Order = 3)] public string Sub1 { get; set; }

        [DataMember(Order = 4)] public string Sub2 { get; set; }

        [DataMember(Order = 5)] public string Sub3 { get; set; }

        [DataMember(Order = 6)] public string Sub4 { get; set; }

        [DataMember(Order = 7)] public string Sub5 { get; set; }

        [DataMember(Order = 8)] public string Sub6 { get; set; }

        [DataMember(Order = 9)] public string Sub7 { get; set; }

        [DataMember(Order = 10)] public string Sub8 { get; set; }

        [DataMember(Order = 11)] public string Sub9 { get; set; }

        [DataMember(Order = 12)] public string Sub10 { get; set; }

        [DataMember(Order = 13)] public string Funnel { get; set; }

        [DataMember(Order = 14)] public string AffCode { get; set; }
    }
    [DataContract]
    public class RegistrationBrandInfo
    {
        [DataMember(Order = 1)]
        public string CustomerId { get; set; }

        [DataMember(Order = 2)]
        public string Token { get; set; }

        [DataMember(Order = 3)]
        public string LoginUrl { get; set; }

        [DataMember(Order = 4)]
        public string Brand { get; set; }
    }
}