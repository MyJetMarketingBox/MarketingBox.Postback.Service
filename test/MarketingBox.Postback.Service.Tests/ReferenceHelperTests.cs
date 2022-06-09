using MarketingBox.Postback.Service.Helper;
using MarketingBox.Registration.Service.Domain.Models.Registrations;
using NUnit.Framework;

namespace MarketingBox.Postback.Service.Tests;

[TestFixture]
public class ReferenceHelperTests
{
    private const string ExpectedReference =
        "http://test.com/?param1=1&param2=2&param3=3&param4=4&param5=5&param6=6&param7=7&param8=8&param9=9&param10=10&param11=funnel1&param12=affcode1";

    private const string SourceReference =
        "http://test.com/?param1={sub1}&param2={sub2}&param3={sub3}&param4={sub4}&param5={sub5}&param6={sub6}&param7={sub7}&param8={sub8}&param9={sub9}&param10={sub10}&param11={funnel}&param12={affcode}";
    
    private static readonly RegistrationAdditionalInfo AdditionalInfo =
        new ()
        {
            Sub1 = "1",
            Sub2 = "2",
            Sub3 = "3",
            Sub4 = "4",
            Sub5 = "5",
            Sub6 = "6",
            Sub7 = "7",
            Sub8 = "8",
            Sub9 = "9",
            Sub10 = "10",
            Funnel = "funnel1",
            AffCode = "affcode1"
        };
    public static object[] GetTestData => new object[]
    {
        new object[]
        {
            ExpectedReference,
            SourceReference,
            AdditionalInfo
        },
        new object[]
        {
            ExpectedReference,
            SourceReference.ToUpper(),
            AdditionalInfo
        },
        new object[]
        {
            SourceReference,
            SourceReference,
            new RegistrationAdditionalInfo{
                Sub1 = "",
                Sub2 = "",
                Sub3 = "",
                Sub4 = "",
                Sub5 = "",
                Sub6 = "",
                Sub7 = "",
                Sub8 = "",
                Sub9 = "",
                Sub10 = "",
                AffCode = "",
                Funnel = ""
            }
        },
        new object[]
        {
            SourceReference,
            SourceReference,
            new RegistrationAdditionalInfo()
        },
        new object[]
        {
            "http://test.com/?param1=3",
            "http://test.com/?param1={sub3}",
            AdditionalInfo
        },
    };

    [TestCaseSource(nameof(GetTestData))]
    public void ReferenceTest(string expected, string source, RegistrationAdditionalInfo additionalInfo)
    {
        Assert.AreEqual(expected,source.ConfigureReference(additionalInfo));
    } 
}