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

            var reservationsWithSelectedChargingStation = db.Reservations.Where(r => r.ChargingStationID == chargingStation.ID);

            var chargingStationsReservationsAfter = reservationsWithSelectedChargingStation
                .Where(r => r.EndTime <= reservation.StartTime)
                .ToList();

            var chargingStationsReservationsBefore = reservationsWithSelectedChargingStation
                .Where(r => r.StartTime <= reservation.EndTime)
                .ToList();


            if (chargingStationsReservationsBefore.Count == 0 && chargingStationsReservationsAfter.Count == 0)
            {
                return ValidationResult.Success;
            } 

            return new ValidationResult("The Charging Station already has a reservation in that time period!");
        }
    }
}