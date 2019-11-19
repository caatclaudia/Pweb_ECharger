using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class User
    {
        public string name { get; set; }
        public List<Reservations> reservations { get; set; }
        public UserCard card { get; set; }

        public User(string n) //SO MAIS TARDE É QUE FAZ RESERVAS E QUE ADICIONA CARTAO
        {
            name = n;
        }

        public bool existReservation(Reservations ob)
        {
            foreach (Reservations aux in reservations)
            {
                if (aux.data == ob.data)
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

        public void removeReservation(Reservations ob)
        {
            if(existReservation(ob))
                reservations.Remove(ob);
        }

        public string toString()
        {
            return $"User name: {name}\n" + card.ToString(); //FALTA MOSTRAR RESERVAS
        }
    }
}
