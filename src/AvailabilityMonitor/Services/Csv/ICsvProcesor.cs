namespace AvailabilityMonitor.Services.Csv
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.AspNetCore.Http;

    public interface ICsvProcesor
    {
        IEnumerable<T> Parse<T>(IFormFile file);
    }
}
