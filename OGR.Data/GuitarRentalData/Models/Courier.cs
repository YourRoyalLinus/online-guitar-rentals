using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalData.Models
{
    public class Courier
    {
        public int Id { get; set; }

        public DistributionCenter DistributionCenter { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DeliveryStartTime { get; set; }

        public DateTime DeliveryEndTime { get; set; }

        [Range(0, 6)]
        public int DayOfWeek { get; set; }

      

    }
}
