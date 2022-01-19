using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Grpc.Models
{
    [DataContract]
    public class ReferenceByAffiliateRequest
    {
        [DataMember(Order = 1)]
        public long AffiliateId { get; set; }
    }
}