using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECharger
{
    public class ChargingStation
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Charging Station Name")]
        [StringLength(255, ErrorMessage = "Name needs to be under 255 characters long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Street Name")]
        [StringLength(255, ErrorMessage = "Street Name needs to be under 255 characters long!")]
        public string StreetName { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "City needs to be under 255 characters long!")]
        public string City { get; set; }
        public string Operator { get; set; }
        public ICollection<Reservation> reservations { get; set; }

        [Required]
        [Display(Name = "Price per minute")]
        [Range(0,1)]
        public double PricePerMinute { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string CompanyID { get; set; }

       

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
