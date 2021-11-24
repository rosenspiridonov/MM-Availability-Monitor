namespace AvailabilityMonitor.Services.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class ProductService : IProductService
    {
        public void SaveResults(string userId, IEnumerable<ProductListingModel> products)
        {
            // TODO: Save Results in history
        }
    }
}
