using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECharger.Models.Data_Models
{
    public class ChargePaymentMethod
    {
        [Range(0, 100)]
        [Display(Name = "Charging Value")]
        public double ChargingValue { get; set; }

        [Required]
        public int PaymentMethodID { get; set; }

        [Display(Name = "Payment Method Name")]
        public PaymentMethod PaymentMethod { get; set; }

    }
}