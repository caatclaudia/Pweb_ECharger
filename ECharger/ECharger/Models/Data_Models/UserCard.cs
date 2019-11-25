using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECharger
{
    public class UserCard
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string ID { get; set; }

        public ICollection<Reservation> reservations { get; set; }
        
        public ICollection<PaymentMethod> paymentMethod { get; set; }

        public bool existReservation(Reservation ob)
        {
            foreach (Reservation aux in reservations)
            {
                if (aux.StartTime == ob.StartTime && aux.EndTime == ob.EndTime)
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
