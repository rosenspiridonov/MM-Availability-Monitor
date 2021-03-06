using AvailabilityMonitor.Services.Products;

namespace AvailabilityMonitor.Services.Comparer
{
    public class StockChangeModel
    {
        public string Name { get; set; }

        public string Sku { get; set; }

        public string Brand { get; set; }

        public StockType StockType { get; set; }
    }
}
