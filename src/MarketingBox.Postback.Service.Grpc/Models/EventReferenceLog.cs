using MarketingBox.Postback.Service.Domain.Models;
using System;
using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Grpc.Models
{
    [DataContract]
    public class EventReferenceLog
    {
        [DataMember(Order = 1)]
        public long AffiliateId { get; set; }

        [DataMember(Order = 2)]
        public Status EventStatus { get; set; }

        [DataMember(Order = 3)]
        public HttpQueryType HttpQueryType { get; set; }

        [DataMember(Order = 4)]
        public string PostbackReference { get; set; }

        [DataMember(Order = 5)]
        public string PostbackResult { get; set; }

        [DataMember(Order = 6)]
        public DateTime Date { get; set; }

        [DataMember(Order = 7)]
        public long Id { get; set; }
    }
}