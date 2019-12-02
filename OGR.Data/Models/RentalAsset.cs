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

        public string AltImgUrl1 { get; set; }

        public string AltImgUrl2 { get; set; }

        public string AltImgUrl3 { get; set; }

        public string AltImgUrl4 { get; set; }

        public string AltImgUrl5 { get; set; }

        public string AltImgUrl6 { get; set; }
    }
}
