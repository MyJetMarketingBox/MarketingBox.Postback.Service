namespace MarketingBox.Postback.Service.Domain.Models
{
    public class Reference
    {
        public long AffiliateId { get; set; }

        public string RegistrationReference { get; set; }

        public string RegistrationTGReference { get; set; }

        public string DepositReference { get; set; }

        public string DepositTGReference { get; set; }

        public HttpQueryType CallType { get; set; }
    }
}
