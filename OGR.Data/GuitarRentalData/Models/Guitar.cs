using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class Guitar : RentalAsset
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Style { get; set; }

        [Required]
        public int NumberOfStrings { get; set; }

        public string Specifications { get; set; }
    }
}
