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
        [DataMember(Order = 1),Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long ClickId { get; set; }

        [DataMember(Order = 2), Required, IsEnum]
        public BrandEventType? EventType { get; set; }

        [DataMember(Order = 3), IsEnum, RequiredOnlyIf(nameof(EventType), BrandEventType.ChangedCrm)]
        public CrmStatus? CrmStatus { get; set; }

        [DataMember(Order = 4), Required] 
        public GeneralInfo GeneralInfo { get; set; }

        [DataMember(Order = 6)]
        public AdditionalInfo AdditionalInfo { get; set; }
        
        [DataMember(Order = 7)]
        public RegistrationBrandInfo RegistrationBrandInfo { get; set; }
    }
}