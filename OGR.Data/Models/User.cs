using System;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public string Telephone { get; set; }

        public DateTime DateOfBirth { get; set; }
      

     


    }
}
