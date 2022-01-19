using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Postgres.Entities
{
    public class ReferenceEntity
    {
        public long ReferenceId { get; set; }
        
        public long AffiliateId { get; set; }

        public string RegistrationReference { get; set; }

        public string RegistrationTGReference { get; set; }

        public string DepositReference { get; set; }

        public string DepositTGReference { get; set; }

        public CallType CallType { get; set; }
    }
}