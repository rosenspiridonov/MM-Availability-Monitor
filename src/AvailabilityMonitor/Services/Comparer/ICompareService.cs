namespace AvailabilityMonitor.Services.Comparer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICompareService
    {
        Task<List<StockChangeModel>> ProductsForStockChange(IFormFile csvFile, IFormFile xmlFile);

        List<NewProductModel> NewProducts(IFormFile csvFile, IFormFile xmlFile);
    }
}
