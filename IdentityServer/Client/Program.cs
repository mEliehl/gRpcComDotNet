using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<JwtInterceptor>();
                    services.AddHttpClient<ApiClient>(c =>
                    {
                        c.BaseAddress = new Uri("https://localhost:3001");
                    });

                    services.AddGrpcClient<Greeter.GreeterClient>(o =>
                    {
                        o.Address = new Uri("https://localhost:5001");
                    }).AddInterceptor<JwtInterceptor>();

                    services.AddHostedService<Worker>();
                });
    }
}
