using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
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
        public string CrmStatus { get; set; }

        [DataMember(Order = 4), Required] public GeneralInfo GeneralInfo { get; set; }
    }
}