using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECharger.Models.Data_Models.Validatitions
{
    public class ChargingStationUsage : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;

            var db = new ApplicationDbContext();

            var chargingStation = db.ChargingStations.Find(reservation.ChargingStationID);

            if (chargingStation == null)
            {
                //TODO: Necessary?
                return new ValidationResult("Charging Station is required!");
            }

            var OverlapedReservationsWithSelectedChargingStation = db.Reservations
                .Where(r => r.ChargingStationID == chargingStation.ID)
                .Where(r => r.StartTime < reservation.EndTime && r.EndTime > reservation.StartTime)
                .ToList();


            if (OverlapedReservationsWithSelectedChargingStation.Count == 0)
            {
                return ValidationResult.Success;
            } 

            return new ValidationResult("The Charging Station already has a reservation in that time period!");
        }
    }
}