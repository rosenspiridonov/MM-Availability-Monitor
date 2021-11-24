namespace AvailabilityMonitor.Services.Csv
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using CsvHelper;

    using Microsoft.AspNetCore.Http;

    public class CsvProcesor : ICsvProcesor
    {
        public IEnumerable<T> Parse<T>(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var csvRecords = csv.GetRecords<T>().ToList();
            return csvRecords;
        }
    }
}
