using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECharger
{
    public class PaymentMethod
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Payment Method Name")]
        [StringLength(255, ErrorMessage = "Name needs to be under 255 characters long!")]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public double Value { get; set; }

        [Required]
        [StringLength(128)]
        public string UserCardID { get; set; }

        public UserCard UserCard { get; set; }


        public string toString()
        {
            return $"Payment Method: {Name} with the value {Value:F2} €\n";
        }
    }
}
