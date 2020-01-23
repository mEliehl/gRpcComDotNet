using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Server;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Product.ProductClient(channel);

            await CreateProduct(client);

            await GetProducts(client);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task CreateProduct(Product.ProductClient client)
        {
            var newProduct = new CreateProductRequest()
            {
                Name = "Test Product",
                Sku = "00001",
                Price = 20.60m
            };

            Console.WriteLine($"Creating product {newProduct.Name}...");
            var response = await client.CreateAsync(newProduct);
            Console.WriteLine($"Product {response.Id} at {response.CreatedAt}");
            Console.WriteLine("");
        }

        private static async Task GetProducts(Product.ProductClient client)
        {
            Console.WriteLine($"Getting products...");
            var response = await client.GetAsync(new Empty());
            foreach (var product in response.Products)
            {
                Console.WriteLine($"Product: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Sku: {product.Sku}");
                Console.WriteLine($"Price: {(decimal)product.Price}");
                Console.WriteLine($"Create at: {product.CreatedAt.ToDateTime().ToString("dd/MM/yyyy hh:mm")}");
                Console.WriteLine("");
            }
        }
    }
}
