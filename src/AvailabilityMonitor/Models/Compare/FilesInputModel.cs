namespace AvailabilityMonitor.Models.Compare
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class FilesInputModel
    {
        [Required]
        [Display(Name = "CSV file")]
        public IFormFile CsvFile { get; set; }

        [Required]
        [Display(Name = "Art list XML file")]
        public IFormFile XmlFile { get; set; }

        [Display(Name = "New products")]
        public bool NewProducts { get; set; }
    }
}
