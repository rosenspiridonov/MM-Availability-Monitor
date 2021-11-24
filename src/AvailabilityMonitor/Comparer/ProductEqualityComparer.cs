namespace AvailabilityMonitor.Comparer
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AvailabilityMonitor.Services.Products;

    public class ProductEqualityComparer : IEqualityComparer<ProductListingModel>
    {

        public bool Equals(ProductListingModel product1, ProductListingModel product2)
        {
            if (product1.Sku == product2.Sku)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode([DisallowNull] ProductListingModel obj)
        {
            return base.GetHashCode();
        }
    }
}
