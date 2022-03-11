using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Domain.Models.Requests
{
    [DataContract]
    public class CreateOrUpdateReferenceRequest
    {
        [DataMember(Order = 1)]
        public long? AffiliateId { get; set; }

        [DataMember(Order = 2)]
        public string RegistrationReference { get; set; }

        [DataMember(Order = 3)]
        public string RegistrationTGReference { get; set; }

        [DataMember(Order = 4)]
        public string DepositReference { get; set; }

        [DataMember(Order = 5)]
        public string DepositTGReference { get; set; }

        [DataMember(Order = 6)]
        public HttpQueryType? HttpQueryType { get; set; }
    }
}