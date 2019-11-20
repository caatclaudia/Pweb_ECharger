using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    public class ChargingStation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Operator { get; set; }
        public ICollection<Reservation> reservations { get; set; }
        private double pricePerMinute;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CompanyID { get; set; }

        public double PricePerMinute
        {
            get { return pricePerMinute; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(pricePerMinute)} has to be >= 0");
                }

                pricePerMinute = value;
            }
        }

        public bool existReservation(Reservation nova)
        {
            foreach (Reservation aux in reservations)
            {
                if (aux.ID == nova.ID)
                    return true;
            }

            return false;
        }

        public List<Reservation> searchReservationDate(DateTime date)
        {
            List<Reservation> reservationsD = new List<Reservation>();
            foreach (Reservation aux in reservations)
            {
                if (aux.StartTime == date)
                    reservationsD.Add(aux);
            }
            return reservationsD;
        }

        public void addReservation(Reservation ob)
        {
            if (existReservation(ob))
                return;
            reservations.Add(ob);
        }

        public void removeReservation(Reservation ob)
        {
            if (existReservation(ob))
                reservations.Remove(ob);
        }
    }
}
