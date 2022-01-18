using System.Runtime.Serialization;
using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}