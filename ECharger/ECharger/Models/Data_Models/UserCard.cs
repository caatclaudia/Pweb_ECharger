using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    public class UserCard
    {
        public int ID { get; set; }
        public ICollection<Reservations> reservations { get; set; }
        public ICollection<PaymentMethod> paymentMethod { get; set; }

        public UserCard()
        {
            reservations = new List<Reservations>();
            paymentMethod = new List<PaymentMethod>();
        }

        public bool existReservation(Reservations ob)
        {
            foreach (Reservations aux in reservations)
            {
                if (aux.StartTime == ob.StartTime && aux.EndTime == ob.EndTime)
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
            if(existReservation(ob))
                reservations.Remove(ob);
        }

        public bool existPaymentMethod(PaymentMethod ob)
        {
            foreach (PaymentMethod aux in paymentMethod)
            {
                if (aux.ID == ob.ID)
                    return true;
            }

            return false;
        }

        public PaymentMethod searchMethod(int ID)
        {
            foreach (PaymentMethod aux in paymentMethod)
            {
                if (aux.ID == ID)
                    return aux;
            }
            return null;
        }

        public void addPaymentMethod(PaymentMethod ob)
        {
            if (existPaymentMethod(ob))
                return;
            paymentMethod.Add(ob);
        }

        public void removePaymentMethod(PaymentMethod ob)
        {
            if (existPaymentMethod(ob))
                paymentMethod.Remove(ob);
        }

        public string toString()
        {
            return $"User Card ID: {ID}\n";
        }
    }
}
