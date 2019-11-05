using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public abstract class RentalAsset
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Available { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public string ImageUrl { get; set; }
    }
}
