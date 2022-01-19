using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Grpc.Models
{
    [DataContract]
    public class ReferenceByAffiliateRequest
    {
        [DataMember(Order = 1)]
        public int AffiliateId { get; set; }
    }
}