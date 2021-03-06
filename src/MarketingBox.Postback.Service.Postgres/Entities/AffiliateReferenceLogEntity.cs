using MarketingBox.Postback.Service.Domain.Models;
using System;

namespace MarketingBox.Postback.Service.Postgres.Entities
{
    public class AffiliateReferenceLogEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public OperationType Operation { get; set; }
        public long? ReferenceId { get; set; }
        public string TenantId { get; set; }
    }
}
