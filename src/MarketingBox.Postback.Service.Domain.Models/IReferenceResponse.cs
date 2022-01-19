namespace MarketingBox.Postback.Service.Domain.Models
{
    public interface IReferenceResponse
    {
        string RegistrationReference { get; set; }
        string RegistrationTGReference { get; set; }
        string DepositReference { get; set; }
        string DepositTGReference { get; set; }
        long AffiliateId { get; set; }
        CallType CallType { get; set; }
    }
}
