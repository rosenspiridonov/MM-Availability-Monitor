namespace AvailabilityMonitor.Services.Xml
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public interface IXmlProcesor
    {
        IEnumerable<T> Parse<T>(string xmlString);

        IEnumerable<T> Parse<T>(IFormFile file);
    }
}
