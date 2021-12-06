using AvailabilityMonitor.Services.Comparer;

namespace AvailabilityMonitor.Services.Products
{
    public class ProductListingModel
    {
        public ProductListingModel()
        {
            IsCompleted = false;
        }

        public string Name { get; set; }

        public string Sku { get; set; }

        public string Brand { get; set; }

        public string StockType { get; set; }

        public bool IsCompleted { get; set; }
    }
}
