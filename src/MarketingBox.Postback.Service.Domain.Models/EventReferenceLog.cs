using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingBox.Postback.Service.Domain.Models
{
    public class EventReferenceLog
    {
        public long AffiliateId { get; set; }
        public EventType EventType { get; set; }
        public HttpQueryType HttpQueryType { get; set; }
        public string PostbackReference { get; set; }
        public string PostbackResponse { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationUId { get; set; }
        public string EventMessage { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
