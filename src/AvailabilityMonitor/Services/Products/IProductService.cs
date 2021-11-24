namespace AvailabilityMonitor.Services.Products
{
    using System.Collections.Generic;

    public interface IProductService
    {
        void SaveResults(string userId, IEnumerable<ProductListingModel> products);
    }
}
