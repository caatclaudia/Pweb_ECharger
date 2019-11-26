using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECharger.Dtos
{
    public class ChargingStationDto
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Name needs to be under 255 characters long!")]
        public string Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Street Name needs to be under 255 characters long!")]
        public string StreetName { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "City needs to be under 255 characters long!")]
        public string City { get; set; }
        public string Operator { get; set; }
        public ICollection<Reservation> reservations { get; set; }

        [Required]
        [Range(0, 1)]
        public double PricePerMinute { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Latitude needs to be between -90 and 90 degrees!")]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Longitude needs to be between -180 and 180 degrees!")]
        public double Longitude { get; set; }
    }
}