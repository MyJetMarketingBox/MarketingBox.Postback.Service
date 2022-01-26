using MarketingBox.Postback.Service.Domain.Models;
using System;

namespace MarketingBox.Postback.Service.Domain
{
    public class FilterLogsRequest
    {
        public long? AffiliateId { get; set; }
        public Status? EventStatus { get; set; }
        public HttpQueryType? HttpQueryType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? Cursor { get; set; }
        public int Take { get; set; }
        public bool Asc { get; set; }
    }
}
