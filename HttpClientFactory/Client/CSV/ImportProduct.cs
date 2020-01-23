using TinyCsvParser.Mapping;

namespace Client.CSV
{
    public class ImportProduct
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
    }

    public class CsvImportProductMapping : CsvMapping<ImportProduct>
    {
        public CsvImportProductMapping()
            : base()
        {
            MapProperty(0, p => p.Name);
            MapProperty(1, p => p.Sku);
            MapProperty(2, p => p.Price);
        }
    }
}