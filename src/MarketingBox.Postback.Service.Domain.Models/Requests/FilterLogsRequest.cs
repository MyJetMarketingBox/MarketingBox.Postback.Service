using System;
using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class FilterLogsRequest
    {
        [DataMember(Order = 1)]
        public long? AffiliateId { get; set; }

        [DataMember(Order = 2)]
        public EventType? EventType { get; set; }

        [DataMember(Order = 3)]
        public HttpQueryType? HttpQueryType { get; set; }

        [DataMember(Order = 4)]
        public DateTime? FromDate { get; set; }

        [DataMember(Order = 5)]
        public DateTime? ToDate { get; set; }

        [DataMember(Order = 6)] 
        public long? Cursor { get; set; }

        [DataMember(Order = 7)] 
        public int Take { get; set; }

        [DataMember(Order = 8)] 
        public bool Asc { get; set; }
        
        [DataMember(Order = 9)] 
        public PostbackResponseStatus? ResponseStatus { get; set; }
    }
}