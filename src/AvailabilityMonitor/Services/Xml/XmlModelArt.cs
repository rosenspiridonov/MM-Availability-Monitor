namespace AvailabilityMonitor.Services.Xml
{
    using System.Xml.Serialization;

    [XmlType("art")]
    public class XmlModelArt
    {
        [XmlElement(ElementName = "name")]
        public string ProductName { get; set; }

        [XmlElement(ElementName = "kol_free")]
        public string InStock { get; set; }

        [XmlElement(ElementName = "nomer")]
        public string Sku { get; set; }

        [XmlElement(ElementName = "art_brand_name")]
        public string Brand { get; set; }
    }
}
