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
        public string name { get; set; }
        public string type { get; set; }
        public ICollection<Reservations> reservations { get; set; }
        private double pricePerMinute;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CompanyID { get; set; }

        public ChargingStation(string n, string t, double p, double lat, double lon, int companyID)
        {
            name = n;
            type = t;
            pricePerMinute = p;
            Latitude = lat;
            Longitude = lon;
            reservations = new List<Reservations>();
            CompanyID = companyID;
        }

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

        public bool existReservation(Reservations nova)
        {
            foreach (Reservations aux in reservations)
            {
                if (aux.ID == nova.ID)
                    return true;
            }

            return false;
        }

        public List<Reservations> searchReservationDate(DateTime date)
        {
            List<Reservations> reservationsD = new List<Reservations>();
            foreach (Reservations aux in reservations)
            {
                if (aux.StartTime == date)
                    reservationsD.Add(aux);
            }
            return reservationsD;
        }

        public void addReservation(Reservations ob)
        {
            if (existReservation(ob))
                return;
            reservations.Add(ob);
        }

        public void removeReservation(Reservations ob)
        {
            if (existReservation(ob))
                reservations.Remove(ob);
        }

        public string toString()
        {
            return $"Station Name: {name}\n" + $"Type: {type}\n" + $"Price: {pricePerMinute:F2}\n"; //FALTA RESERVAS
        }
    }
}
