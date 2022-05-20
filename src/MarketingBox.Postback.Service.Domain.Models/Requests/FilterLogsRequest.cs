using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class FilterLogsRequest : ValidatableEntity
    {
        [DataMember(Order = 1)] public List<long> AffiliateIds { get; set; } = new();

        [DataMember(Order = 2)] public string AffiliateName { get; set; }
        [DataMember(Order = 3)] public EventType? EventType { get; set; }

        [DataMember(Order = 4)] public HttpQueryType? HttpQueryType { get; set; }

        [DataMember(Order = 5)] public DateTime? FromDate { get; set; }

        [DataMember(Order = 6)] public DateTime? ToDate { get; set; }

        [DataMember(Order = 7), AdvancedCompare(ComparisonType.GreaterThanOrEqual, 1)]
        public long? Cursor { get; set; }

        [DataMember(Order = 8), AdvancedCompare(ComparisonType.GreaterThanOrEqual, 1)]
        public int? Take { get; set; }

        [DataMember(Order = 9)] public bool Asc { get; set; }

        [DataMember(Order = 10)] public PostbackResponseStatus? ResponseStatus { get; set; }

        [DataMember(Order = 11)] public string RegistrationUId { get; set; }

        [DataMember(Order = 12)]
        public string TenantId { get; set; }
    }
}