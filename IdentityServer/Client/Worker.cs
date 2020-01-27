using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server;
using static Server.Greeter;

namespace Client
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly GreeterClient _grpcClient;

        public Worker(ILogger<Worker> logger, GreeterClient grpcClient)
        {
            _logger = logger;
            _grpcClient = grpcClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var reply = await _grpcClient.SayHelloAsync(new HelloRequest { Name = "GrpcClient" });
                    Console.WriteLine("Greeting: " + reply.Message);
                }
                catch (RpcException ex)
                {
                    _logger.LogError($"RpcException: {ex}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception: {ex}");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
