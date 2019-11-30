using System;
using System.ComponentModel.DataAnnotations;

namespace ECharger
{
    class AddressInfo
    {
        [StringLength(255, ErrorMessage = "Address Line needs to be under 255 characters long!")]
        public string AddressLine1 { get; set; }

        [StringLength(255, ErrorMessage = "Town needs to be under 255 characters long!")]
        public string Town { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude needs to be between -90 and 90 degrees!")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude needs to be between -180 and 180 degrees!")]
        public double Longitude { get; set; }
    }
}
