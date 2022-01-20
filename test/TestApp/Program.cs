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
            var client = factory.GetPostbackService();

            var resp = await client.GetReferenceAsync(new ReferenceByAffiliateRequest() { AffiliateId = 1 });
            if (resp?.Success == true)
            {
                var data = resp.Data;
                Console.WriteLine(data?.AffiliateId);
                Console.WriteLine(data?.DepositReference);
                Console.WriteLine(data?.DepositTGReference);
                Console.WriteLine(data?.RegistrationReference);
                Console.WriteLine(data?.RegistrationTGReference);
                Console.WriteLine(data?.HttpQueryType);

            }
            else
            {
                Console.WriteLine(resp?.ErrorMessage);
            }

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
