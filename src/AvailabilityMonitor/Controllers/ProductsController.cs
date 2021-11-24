namespace AvailabilityMonitor.Controllers
{
    using System.Linq;

    using AvailabilityMonitor.Comparer;
    using AvailabilityMonitor.Models.Compare;
    using AvailabilityMonitor.Services.Products;
    using AvailabilityMonitor.Services.Comparer;

    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Net;
    using AvailabilityMonitor.Services.Excel;
    using System;

    public class ProductsController : Controller
    {
        private readonly ICompareService compareService;
        private readonly IExcelService excelService;

        public ProductsController(
            ICompareService compareService,
            IExcelService excelService)
        {
            this.compareService = compareService;
            this.excelService = excelService;
        }

        public IActionResult Results(int input)
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public IActionResult Results(FilesInputModel input)
        {
            if (!input.CsvFile.FileName.EndsWith(".csv")
                || !input.XmlFile.FileName.EndsWith(".xml")/*
                || !input.XmlFileArt.FileName.EndsWith(".xml")*/)
            {
                TempData["Incorrect files"] = "Incorrect files";
                return RedirectToAction("Compare", "Compare");
            }

            var products = new List<ProductListingModel>();

            try
            {
                if (/*(string)TempData["NewProducts"] == "yes"*/input.NewProducts)
                {
                    products = this.compareService
                        .NewProducts(input.CsvFile, input.XmlFile)
                        .Select(x => new ProductListingModel
                        {
                            Sku = x.Sku,
                            Brand = x.Brand,
                        })
                        .Distinct(new ProductEqualityComparer())
                        .OrderBy(x => x.Brand)
                        .ToList();
                }
                else
                {
                    products = this.compareService
                        .ProductsForStockChange(input.CsvFile, input.XmlFile)
                        .GetAwaiter()
                        .GetResult()
                        .Select(x => new ProductListingModel
                        {
                            Sku = x.Sku,
                            Brand = x.Brand,
                            StockType = x.StockType == StockType.ToHide ? "За скриване" : "За показване"
                        })
                        .Distinct(new ProductEqualityComparer())
                        .OrderBy(x => x.Brand)
                        .ToList();
                }
            }
            catch (Exception)
            {
                //return BadRequest("One of the xml file endpoints is not working");
                return BadRequest("Incorect files");
            }

            return View(products);
        }
    }
}
