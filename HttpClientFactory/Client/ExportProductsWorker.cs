using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server;

namespace Client
{
    public class ExportProductsWorker : BackgroundService
    {
        private readonly ILogger<ExportProductsWorker> _logger;
        private readonly Product.ProductClient _client;

        public ExportProductsWorker(ILogger<ExportProductsWorker> logger,
        Product.ProductClient client)
        {
            _logger = logger;
            _client = client;
        }

        private async void CreateFileEvent(object source, FileSystemEventArgs e)
        {
            // work around to not read file too fast
            await Task.Delay(1000);

            _logger.LogInformation($"New file : {e.FullPath}...");
            var response = await _client.GetAsync(new Empty());

            var contents = new StringBuilder();
            contents.AppendLine("Id,Name,Sku,Price,CreatedAt");

            response.Products.Aggregate(contents, (sb, p) =>
                sb.AppendLine($"{p.Id},{p.Name},{p.Sku},{((decimal)p.Price).ToString().Replace(',', '.')},{p.CreatedAt.ToDateTime().ToString("ddMMyyyy-HHmmss")}")
            );

            var destination = Path.Combine(FileHelper.ProductCreatedPath, $"{FileHelper.DateFile}.csv");
            await File.WriteAllTextAsync(destination, contents.ToString());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using FileSystemWatcher watcher =
                new FileSystemWatcherWrapper(FileHelper.ProcessPath, CreateFileEvent);

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(10000, stoppingToken);
        }
    }
}
