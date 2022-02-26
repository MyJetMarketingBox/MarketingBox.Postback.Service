using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Domain.Models
{
    [DataContract]
    public class Affiliate
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        
        [DataMember(Order = 2)]
        public string Name { get; set; }
    }
}