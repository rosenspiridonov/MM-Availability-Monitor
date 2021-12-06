namespace AvailabilityMonitor.Services.Comparer
{
    using AvailabilityMonitor.Services.Csv;
    using AvailabilityMonitor.Services.Xml;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    public class CompareService : ICompareService
    {
        private readonly ICsvProcesor csvProcesor;
        private readonly IXmlProcesor xmlProcesor;
        private readonly IWebHostEnvironment environment;

        public CompareService(
            ICsvProcesor csvProcesor,
            IXmlProcesor xmlProcesor,
            IWebHostEnvironment environment)
        {
            this.csvProcesor = csvProcesor;
            this.xmlProcesor = xmlProcesor;
            this.environment = environment;
        }

        public List<NewProductModel> NewProducts(IFormFile csvFile, IFormFile xmlFile)
        {
            var csvRecords = this.GetCsvRecords(csvFile);

            var xmlRecords = this.GetXmlRecords(xmlFile)
                .GetAwaiter()
                .GetResult()
                .Where(x => DataConstants.Brands.Any(b => b.ToLower().Contains(x.Brand.ToLower())))
                .ToList();

            // File comparison
            var products = new List<NewProductModel>();

            foreach (var xmlItem in xmlRecords)
            {
                if (!csvRecords.Any(x => x.Sku == xmlItem.Sku))
                {
                    if (xmlItem.InStock)
                    {
                        products.Add(new NewProductModel()
                        {
                            Sku = xmlItem.Sku,
                            Brand = xmlItem.Brand,
                            Name = xmlItem.ProductName
                        });
                    }
                }
            }

            return products;
        }

        public async Task<List<StockChangeModel>> ProductsForStockChange(IFormFile csvFile, IFormFile xmlFile)
        {
            List<CsvModel> csvRecords = GetCsvRecords(csvFile);

            List<XmlModel> xmlRecords = await this.GetXmlRecords(xmlFile);

            // File comparison
            var products = new List<StockChangeModel>();

            if (!xmlRecords.Any())
            {
                return products;
            }

            foreach (var csvItem in csvRecords)
            {
                var product = new StockChangeModel()
                {
                    Sku = csvItem.Sku,
                    Brand = csvItem.Brand,
                    Name = csvItem.ProductName
                };

                var xmlItem = xmlRecords.FirstOrDefault(x => x.Sku == csvItem.Sku);

                

                if (xmlItem != null) // In XML file
                {
                    if (!xmlItem.InStock && csvItem.IsHidden == "0") // Out of stock && Visible in site
                    {
                        product.StockType = StockType.ToHide;
                    }
                    else if (xmlItem.InStock && csvItem.IsHidden == "1") // In stock && Hidden in site
                    {
                        product.StockType = StockType.ToShow;
                    }
                }
                else // Not in XML file
                {
                    if (csvItem.IsHidden == "0")
                    {
                        product.StockType = StockType.ToHide;
                    }
                }

                if (product.StockType != 0)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        private List<CsvModel> GetCsvRecords(IFormFile csvFile) => this.csvProcesor
               .Parse<CsvModel>(csvFile)
               .Where(x => DataConstants.Brands.Contains(x.Brand.ToLower()))
               .ToList();

        private async Task<List<XmlModel>> GetXmlRecords(IFormFile xmlFile)
        {
            //var artListXmlText = "";

            //try
            //{
            //    artListXmlText = await GetAsync("http://89.190.211.184:8080/KetenREST/resources/export/art_xml?service_name=modmasa");
            //}
            //catch (Exception)
            //{
            //    var filePath = Path.Combine(this.environment.WebRootPath, "files", "xml", "art_list-2021-11-10.xml");
            //    artListXmlText = await File.ReadAllTextAsync(filePath);
            //}

            //var artListXml = this.xmlProcesor
            //    .Parse<XmlModelArt>(artListXmlText)
            //    .ToList();

            var artListXml = this.xmlProcesor
                .Parse<XmlModelArt>(xmlFile)
                .ToList();

            var giftshopXmlText = await GetAsync("http://www.vip-giftshop.com/media/amfeed/feeds/nvb_all.xml");
            var giftshopXml = this.xmlProcesor
                .Parse<XmlModelGiftshop>(giftshopXmlText)
                .ToList();

            var models = new List<XmlModel>();

            models.AddRange(artListXml.Select(x => new XmlModel
            {
                Sku = x.Sku,
                InStock = x.InStock.ToLower() == "yes",
                Brand = x.Brand,
                ProductName = x.ProductName
            }).ToList());

            models.AddRange(giftshopXml.Select(x => new XmlModel
            {
                Sku = x.Sku,
                InStock = x.InStock.ToLower() == "in stock",
                Brand = x.Brand,
                ProductName = x.ProductName
            }).ToList());

            return models;
        }

        private async Task<string> GetAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
