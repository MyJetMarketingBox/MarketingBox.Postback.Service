using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class UpdateReferenceRequest : ValidatableEntity
    {
        [DataMember(Order = 1), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long? RequestedBy { get; set; }

        [DataMember(Order = 2)]
        public string RegistrationReference { get; set; }

        [DataMember(Order = 3)]
        public string RegistrationTGReference { get; set; }

        [DataMember(Order = 4)]
        public string DepositReference { get; set; }

        [DataMember(Order = 5)]
        public string DepositTGReference { get; set; }

        [DataMember(Order = 6), Required, IsEnum]
        public HttpQueryType? HttpQueryType { get; set; }
        
        [DataMember(Order = 7), Required]
        public string TenantId { get; set; }
        
        [DataMember(Order = 8), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long? PostbackId { get; set; }
    }
}