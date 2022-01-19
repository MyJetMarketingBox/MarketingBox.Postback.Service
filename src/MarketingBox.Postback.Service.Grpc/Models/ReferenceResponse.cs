using System.Runtime.Serialization;
using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Grpc.Models
{
    [DataContract]
    public class ReferenceResponse : IReferenceResponse
    {
        [DataMember(Order = 1)]
        public int AffiliateId { get; set; }

        [DataMember(Order = 2)]
        public string RegistrationReference { get; set; }

        [DataMember(Order = 3)]
        public string RegistrationTGReference { get; set; }

        [DataMember(Order = 4)]
        public string DepositReference { get; set; }

        [DataMember(Order = 5)]
        public string DepositTGReference { get; set; }

        [DataMember(Order = 6)]
        public CallTypeEnum CallType { get; set; }
    }
}