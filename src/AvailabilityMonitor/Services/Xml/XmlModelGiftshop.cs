namespace AvailabilityMonitor.Services.Xml
{
    using System.Xml.Serialization;

    [XmlType("item")]
    public class XmlModelGiftshop
    {
        [XmlElement(ElementName = "Name")]
        public string ProductName { get; set; }

        [XmlElement(ElementName = "InStock")]
        public string InStock { get; set; }

        [XmlElement(ElementName = "SKU")]
        public string Sku { get; set; }

        [XmlElement(ElementName = "Brand")]
        public string Brand { get; set; }
    }
}
