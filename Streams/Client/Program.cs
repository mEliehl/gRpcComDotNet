using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using static Server.Movie;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new MovieClient(channel);

            using var streamingCall = client.StarWarsIntro(new Empty());

            try
            {
                Console.Clear();
                await foreach (var data in streamingCall.ResponseStream.ReadAllAsync())
                    Console.WriteLine(data.Frame);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }

            Console.WriteLine("The End");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
