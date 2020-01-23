using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server;
using TinyCsvParser;

namespace Client
{
    public class ImportProductsWorker : BackgroundService
    {
        private readonly ILogger<ImportProductsWorker> _logger;
        private readonly Product.ProductClient _client;

        public ImportProductsWorker(ILogger<ImportProductsWorker> logger,
        Product.ProductClient client)
        {
            _logger = logger;
            _client = client;
        }

        private async void CreateFileEvent(object source, FileSystemEventArgs e)
        {
            // work around to not read file too fast
            await Task.Delay(1000);

            _logger.LogInformation($"Reading file : {e.FullPath}...");
            var products = ReadCsv(e);

            foreach (var product in products)
            {
                await _client.CreateAsync(new CreateProductRequest()
                {
                    Name = product.Name,
                    Sku = product.Sku,
                    Price = product.Price
                });
            }

            var destination = Path.Combine(FileHelper.ProcessPath,
                $"{FileHelper.DateFile}-{e.Name}");
            File.Move(e.FullPath, destination);

            _logger.LogInformation($"file moved to {destination}");
        }

        private IEnumerable<CSV.ImportProduct> ReadCsv(FileSystemEventArgs e)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CSV.CsvImportProductMapping csvMapper = new CSV.CsvImportProductMapping();
            CsvParser<CSV.ImportProduct> csvParser = new CsvParser<CSV.ImportProduct>(csvParserOptions, csvMapper);

            var result = csvParser
                            .ReadFromFile(e.FullPath, Encoding.ASCII)
                            .Select(s => s.Result)
                            .ToList();

            return result;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using FileSystemWatcher watcher =
                new FileSystemWatcherWrapper(FileHelper.ProductPath, CreateFileEvent);

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(10000, stoppingToken);
        }
    }
}
