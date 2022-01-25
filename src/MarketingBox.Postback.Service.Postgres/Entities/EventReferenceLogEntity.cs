using MarketingBox.Postback.Service.Domain.Models;
using System;

namespace MarketingBox.Postback.Service.Postgres.Entities
{
    public class EventReferenceLogEntity : EventReferenceLog
    {
        public long Id { get; set; }
    }
}
