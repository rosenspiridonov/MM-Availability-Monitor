namespace AvailabilityMonitor.Services.Xml
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using System.Xml;

    public class XmlProcesor : IXmlProcesor
    {
        public IEnumerable<T> Parse<T>(string xmlString)
        {
            //var content = new StringBuilder();
            //using var reader = new StreamReader(file.OpenReadStream());
            //while (reader.Peek() >= 0)
            //{
            //    content.AppendLine(reader.ReadLine());
            //}

            var xmlRootAttributeName = this.GetRootName(xmlString);
            var result = XmlConverter.Deserializer<T>(xmlString, xmlRootAttributeName);
            return result.ToList();
        }

        public IEnumerable<T> Parse<T>(IFormFile file)
        {
            var content = new StringBuilder();
            using var reader = new StreamReader(file.OpenReadStream());
            while (reader.Peek() >= 0)
            {
                content.AppendLine(reader.ReadLine());
            }

            var xmlString = content.ToString();

            var xmlRootAttributeName = this.GetRootName(xmlString);
            var result = XmlConverter.Deserializer<T>(xmlString, xmlRootAttributeName);
            return result.ToList();
        }

        private string GetRootName(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            XmlElement root = doc.DocumentElement;
            return root.Name;
        }
    }
}
