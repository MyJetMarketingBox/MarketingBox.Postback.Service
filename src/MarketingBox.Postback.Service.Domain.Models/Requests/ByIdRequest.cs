using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class ByIdRequest : ValidatableEntity
    {
        [DataMember(Order = 1), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long? PostbackId { get; set; }
        
        [DataMember(Order = 2), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long? RequestedBy { get; set; }
        
        [DataMember(Order = 3), Required]
        public string TenantId { get; set; }
    }
}