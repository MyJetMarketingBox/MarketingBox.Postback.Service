using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Domain.Models
{
    [DataContract]
    public class ByAffiliateIdRequest
    {
        [DataMember(Order = 1)]
        public long AffiliateId { get; set; }
        
        [DataMember(Order = 2)] 
        public long? Cursor { get; set; }

        [DataMember(Order = 3)] 
        public int Take { get; set; }

        [DataMember(Order = 4)] 
        public bool Asc { get; set; }
    }
}