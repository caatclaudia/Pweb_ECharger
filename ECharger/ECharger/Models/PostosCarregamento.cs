using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class PostosCarregamento
    {
        public string nome { get; set; }
        public string tipo { get; set; }
        public List<Reservas> reservas { get; set; }
        //horario
        private double preco;
        private long contacto;
        public bool disponivel { get; set; }
        //localizacao

        public PostosCarregamento(string n, string t, double p, long c)
        {
            nome = n;
            tipo = t;
            preco = p;
            contacto = c;
            disponivel = false;
        }

        public double Preco
        {
            get { return preco; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(preco)} tem que ser >= 0");
                }

                preco = value;
            }
        }

        public long Contacto
        {
            get { return contacto; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(contacto)} tem que ser um contacto valido");
                }

                contacto = value;
            }
        }

        public bool existeReserva(Reservas nova)
        {
            foreach (Reservas aux in reservas)
            {
                if (aux.data == nova.data)
                    return true;
            }

            return false;
        }

        public List<Reservas> procuraReservaData(DateTime date)
        {
            List<Reservas> reservasD = new List<Reservas>();
            foreach (Reservas aux in reservas)
            {
                if (aux.data == date)
                    reservasD.Add(aux);
            }
            return reservasD;
        }

        public void addReserva(Reservas nova)
        {
            if (existeReserva(nova))
                return;
            reservas.Add(nova);
        }

        public void removeReserva(Reservas nova)
        {
            if (existeReserva(nova))
                reservas.Remove(nova);
        }

        public string ToString()
        {
            return $"Nome do Posto: {nome}\n" + $"Tipo: {tipo}\n" + $"Preco: {preco:F2}\n" + $"Contacto: {contacto}\n"; //FALTA RESERVAS
        }
    }
}
