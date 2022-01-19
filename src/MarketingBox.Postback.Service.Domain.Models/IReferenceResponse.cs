namespace MarketingBox.Postback.Service.Domain.Models
{
    public interface IReferenceResponse
    {
        string RegistrationReference { get; set; }
        string RegistrationTGReference { get; set; }
        string DepositReference { get; set; }
        string DepositTGReference { get; set; }
        int AffiliateId { get; set; }
        CallTypeEnum CallType { get; set; }
    }
}
