﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECharger
{
    public class PaymentMethod
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Name needs to be under 255 characters long!")]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public double Value { get; set; }

        [StringLength(255)]
        public string UserCardID { get; set; }

        [Required]
        public UserCard UserCard { get; set; }


        public string toString()
        {
            return $"Payment Method: {Name} with the value {Value:F2} €\n";
        }
    }
}
