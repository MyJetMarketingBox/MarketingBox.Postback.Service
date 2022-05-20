using System.Collections.Generic;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class SearchReferenceRequest : ValidatableEntity
    {
        [DataMember(Order = 1)] public List<long> AffiliateIds { get; set; } = new();
        [DataMember(Order = 2)] public string AffiliateName { get; set; }
        [DataMember(Order = 3)] public HttpQueryType? HttpQueryType { get; set; }
        
        [DataMember(Order = 4), AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public long? Cursor { get; set; }

        [DataMember(Order = 5), AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public int? Take { get; set; }

        [DataMember(Order = 6)] public bool Asc { get; set; }
        [DataMember(Order = 7)] public string TenantId { get; set; }
    }
}