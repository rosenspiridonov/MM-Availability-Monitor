namespace AvailabilityMonitor.Services.Csv
{
    using CsvHelper.Configuration.Attributes;

    public class CsvModel
    {
        [Name("Име на продукт")]
        public string ProductName { get; set; }

        [Name("sku")]
        public string Sku { get; set; }

        [Name("Марка")]
        public string Brand { get; set; }

        [Name("Скрит продукт")]
        public string IsHidden { get; set; } // Не/Да
    }
}
