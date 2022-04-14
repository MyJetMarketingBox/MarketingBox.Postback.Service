using System.Runtime.Serialization;

namespace MarketingBox.Postback.Service.Messages;

[DataContract]
public class TrackingLinkUpdateRegistrationIdMessage
{
    public const string Topic = "marketing-box-postback-service-trackinglink-update";
    [DataMember(Order = 1)]
    public long ClickId { get; set; }
    
    [DataMember(Order = 2)]
    public long RegistrationId { get; set; }
}