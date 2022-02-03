using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Grpc.Models
{
    [DataContract]
    public class Response<T>
    {
        [DataMember(Order = 1)]
        public StatusCode StatusCode { get; set; }
        
        [DataMember(Order = 2)]
        public string ErrorMessage { get; set; }

        [DataMember(Order = 3)]
        public T Data { get; set; }
    }
}