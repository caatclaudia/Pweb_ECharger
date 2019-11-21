using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECharger.Models.Data_Models
{
    public class ReservationDataCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;

            if (reservation.EndTime > reservation.StartTime)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("End Time needs to be bigger than Start Time!");
        }
    }
}