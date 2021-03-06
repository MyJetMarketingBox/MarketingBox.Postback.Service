using System;
using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Domain.Models
{
    [DataContract]
    public class EventReferenceLog
    {
        [DataMember(Order = 1)]
        public long AffiliateId { get; set; }
        
        [DataMember(Order = 2)]
        public string RegistrationUId { get; set; }
        
        [DataMember(Order = 3)]
        public EventType EventType { get; set; }
        
        [DataMember(Order = 4)]
        public HttpQueryType HttpQueryType { get; set; }
        
        [DataMember(Order = 5)]
        public string EventMessage { get; set; }
        
        [DataMember(Order = 6)]
        public string PostbackReference { get; set; }
        
        [DataMember(Order = 7)]
        public string PostbackResponse { get; set; }
        
        [DataMember(Order = 8)]
        public PostbackResponseStatus PostbackResponseStatus { get; set; }
        
        [DataMember(Order = 9)]
        public DateTime Date { get; set; }
        
        [DataMember(Order = 10)]
        public long Id { get; set; }
        
        [DataMember(Order = 11)]
        public Affiliate Affiliate { get; set; }
        
        [DataMember(Order = 12)]
        public string TenantId { get; set; }
    }
}