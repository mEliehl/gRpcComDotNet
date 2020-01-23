using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace Server
{
    public class ProductService : Product.ProductBase
    {
        private static readonly ICollection<Model.Product> products = new List<Model.Product>();
        public override Task<CreateProductResponse> Create(CreateProductRequest request, Grpc.Core.ServerCallContext context)
        {

            var product = new Model.Product()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow,
                Name = request.Name,
                Sku = request.Sku,
                Price = request.Price,
            };
            products.Add(product);

            return Task.FromResult(new CreateProductResponse()
            {
                Id = product.Id.ToString(),
                CreatedAt = Timestamp.FromDateTimeOffset(product.CreatedAt),
            });
        }
        public override Task<ProductsResponse> Get(Empty _, Grpc.Core.ServerCallContext context)
        {
            var maps = products
                .Select(p => new ProductResponse()
                {
                    Id = p.Id.ToString(),
                    CreatedAt = Timestamp.FromDateTimeOffset(p.CreatedAt),
                    Name = p.Name,
                    Sku = p.Sku,
                    Price = p.Price
                });

            var @return = new ProductsResponse();
            @return.Products.AddRange(maps);
            return Task.FromResult(@return);
        }
    }
}