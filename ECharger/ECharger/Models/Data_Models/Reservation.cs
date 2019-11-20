using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    public class Reservations
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        private DateTime endTime { get; set; }
        public double TotalPrice { get; private set; }
        public int ChargingStationID { get; set; }
        public ChargingStation ChargingStation { get; set; }
        public int UserCardID { get; set; }
        public UserCard UserCard { get; set; }

        public Reservations(DateTime startTime, DateTime endTime, ChargingStation chargingStation, UserCard userCard)
        {
            ChargingStation = chargingStation;
            StartTime = startTime;
            EndTime = endTime;
            UserCard = userCard;
        }

        public DateTime EndTime {
            get { return endTime; }
            set
            {
                TimeSpan timeSpan = EndTime - StartTime;
                int totalMinutes = (int) timeSpan.TotalMinutes;

                if (totalMinutes < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(EndTime)} has to be bigger than {nameof(StartTime)}");
                }

                endTime = value;

                TotalPrice = ChargingStation.PricePerMinute * totalMinutes;
            }
        }

        public string toString()
        {
            return $"Start Time: {StartTime}\nEnd Time: {EndTime}\nTotal Price: {TotalPrice}\nCharging Station Id: {ChargingStationID}\nUser Card Id: {UserCardID}";
        }
    }
}
