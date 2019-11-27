using ECharger.Models.Data_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    public class Reservation
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [ReservationDataCheck]
        public DateTime EndTime { get; set; }

        [Display(Name = "Total Price")]
      //  [Range(0, double.MaxValue, ErrorMessage ="Value Total Price needs to be bigger than 0")]
        public double TotalPrice { get; set; }

        [Required]
        public int ChargingStationID { get; set; }
        
        public ChargingStation ChargingStation { get; set; }

        [Required]
        [StringLength(128)]
        public string UserCardID { get; set; }
        
        public UserCard UserCard { get; set; }

        [Required]
        public int PaymentMethodID { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }

        public void updateTotalPrice()
        {
            if (ChargingStation != null)
            {
                TimeSpan timeSpan = EndTime - StartTime;
                int totalMinutes = (int)timeSpan.TotalMinutes;

                TotalPrice = ChargingStation.PricePerMinute * totalMinutes;
            }
        }

        public string toString()
        {
            return $"Start Time: {StartTime}\nEnd Time: {EndTime}\nTotal Price: {TotalPrice}\nCharging Station Id: {ChargingStationID}\nUser Card Id: {UserCardID}";
        }
    }
}
