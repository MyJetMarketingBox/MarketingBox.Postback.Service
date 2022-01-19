using System;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using MarketingBox.Postback.Service.Client;
using MarketingBox.Postback.Service.Grpc.Models;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Console.Write("Press enter to start");
            Console.ReadLine();


            var factory = new PostbackServiceClientFactory("http://localhost:5001");
            var client = factory.GetHelloService();

            var resp = await  client.GetReferenceAsync(new ReferenceByAffiliateRequest(){AffiliateId = 1});
            Console.WriteLine(resp?.AffiliateId);
            Console.WriteLine(resp?.DepositReference);
            Console.WriteLine(resp?.DepositTGReference);
            Console.WriteLine(resp?.RegistrationReference);
            Console.WriteLine(resp?.RegistrationTGReference);
            Console.WriteLine(resp?.CallType);

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
