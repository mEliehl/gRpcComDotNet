using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class MovieService : Movie.MovieBase
    {
        private readonly ILogger<MovieService> _logger;
        public MovieService(ILogger<MovieService> logger)
        {
            _logger = logger;
        }

        public override async Task StarWarsIntro(Google.Protobuf.WellKnownTypes.Empty request, Grpc.Core.IServerStreamWriter<ScenesResponse> responseStream, Grpc.Core.ServerCallContext context)
        {
            _logger.LogInformation("Start Streaming...");

            await responseStream.WriteAsync(new ScenesResponse() { Frame = Scenes.INTRO });
            _logger.LogInformation("Sending a Frame...");
            await Task.Delay(TimeSpan.FromSeconds(2));

            await responseStream.WriteAsync(new ScenesResponse() { Frame = Scenes.LOGO });
            _logger.LogInformation("Sending a Frame...");
            await Task.Delay(TimeSpan.FromSeconds(1));

            foreach (var frame in Scenes.FlyingLetters())
            {
                await responseStream.WriteAsync(new ScenesResponse() { Frame = frame });
                _logger.LogInformation("Sending a Frame...");
                await Task.Delay(600);
            }
        }

    }
}