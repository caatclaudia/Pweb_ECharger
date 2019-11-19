using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class LoadingStations
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<Reservations> reservations { get; set; }
        //horario
        private double price;
        private long contact;
        public bool available { get; set; }
        //localizacao

        public LoadingStations(string n, string t, double p, long c)
        {
            name = n;
            type = t;
            price = p;
            contact = c;
            available = false;
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(price)} has to be >= 0");
                }

                price = value;
            }
        }

        public long Contact
        {
            get { return contact; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(contact)} has to be valid");
                }

                contact = value;
            }
        }

        public bool existReservation(Reservations nova)
        {
            foreach (Reservations aux in reservations)
            {
                if (aux.date == nova.date)
                    return true;
            }

            return false;
        }

        public List<Reservations> searchReservationDate(DateTime date)
        {
            List<Reservations> reservationsD = new List<Reservations>();
            foreach (Reservations aux in reservations)
            {
                if (aux.date == date)
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

        public void removeReserva(Reservations ob)
        {
            if (existReservation(ob))
                reservations.Remove(ob);
        }

        public string toString()
        {
            return $"Station Name: {name}\n" + $"Type: {type}\n" + $"Price: {price:F2}\n" + $"Contact: {contact}\n"; //FALTA RESERVAS
        }
    }
}
