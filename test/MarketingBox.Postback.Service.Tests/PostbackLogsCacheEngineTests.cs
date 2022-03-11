using System.Collections.Generic;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Postback.Service.Engines;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace MarketingBox.Postback.Service.Tests;

[TestFixture]
public class PostbackLogsCacheEngineTests
{
    private const int Capacity = 100;
    private AutoMocker _autoMocker;
    private PostbackLogsCacheEngine _postbackLogsCacheEngine;
    private EventReferenceLog[] _loadedLogs = TakeLogs(1);

    private static EventReferenceLog[] TakeLogs(int count)
    {
        var list = new List<EventReferenceLog>(count);
        for (var i = count-1; i >= 0; i--)
        {
            list.Add(
                new EventReferenceLog
                {
                    RegistrationUId = i.ToString(),
                    EventType = EventType.Approved
                });
        }
        return list.ToArray();
    }
    
    [SetUp]
    public void Setup()
    {
        _autoMocker = new AutoMocker();
        _autoMocker.Setup<IEventReferenceLoggerRepository, Task<EventReferenceLog[]>>(
                x => x.SearchAsync(
                    It.Is<FilterLogsRequest>(f => f.Asc == false && f.Take == Capacity)))
            .ReturnsAsync(() => _loadedLogs)
            .Verifiable();

        _postbackLogsCacheEngine = _autoMocker.CreateInstance<PostbackLogsCacheEngine>();
    }


    [Test, Order(1)]
    public void CheckAndUpdateCacheContainsRecordTest()
    {
        _postbackLogsCacheEngine.Start();
        var model = new PostbackLogsCacheModel("0", EventType.Approved);
        
        var result = _postbackLogsCacheEngine.CheckAndUpdateCache(model);
        
        Assert.IsTrue(result, $"Cache does not contain {model}");
        _autoMocker.Verify();
    }
    
    [TestCase("0",EventType.Registered)]
    [TestCase("1",EventType.Approved)]
    [TestCase("1",EventType.Registered)]
    [Test, Order(2)]
    public void CheckAndUpdateCacheDoesNotContainRecordTest(string registrationUid,EventType eventType)
    {
        _postbackLogsCacheEngine.Start();
        var model = new PostbackLogsCacheModel(registrationUid, eventType);
        
        var result = _postbackLogsCacheEngine.CheckAndUpdateCache(model);
        
        Assert.IsFalse(result, $"Cache contains {model}");
        _autoMocker.Verify();
    }

    [Test, Order(3)]
    public void CheckAndUpdateCacheLimitIsReachedTest()
    {
        _loadedLogs = TakeLogs(Capacity);
        _postbackLogsCacheEngine.Start();
        
        var model = new PostbackLogsCacheModel("0", EventType.Approved);
        var result = _postbackLogsCacheEngine.CheckAndUpdateCache(model);
        Assert.IsTrue(result, $"Cache does not contain {model}");

        model = new PostbackLogsCacheModel("100", EventType.Approved); 
        result = _postbackLogsCacheEngine.CheckAndUpdateCache(model);
        Assert.False(result, $"Cache contains {model}");
        
        model = new PostbackLogsCacheModel("0", EventType.Approved); 
        result = _postbackLogsCacheEngine.CheckAndUpdateCache(model);
        Assert.False(result, $"Cache contains {model}");
        
        _autoMocker.Verify();
    }
}