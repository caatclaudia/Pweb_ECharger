using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECharger.Models.Data_Models.Validatitions
{
    public class StartReservationDataCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;

            if (reservation.StartTime > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Start Time needs to be greater than the current time!");
        }
    }
}