using MarketingBox.Postback.Service.Domain;

namespace MarketingBox.Postback.Service.Engines
{
    public interface IPostbackLogsCacheEngine
    {
        bool CheckAndUpdateCache(PostbackLogsCacheModel  record);
    }
}