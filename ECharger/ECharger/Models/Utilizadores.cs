using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class Utilizadores
    {
        public string nome { get; set; }
        public List<Reservas> reservas { get; set; }
        public CartaoUtilizador cartao { get; set; }

        public Utilizadores(string n) //SO MAIS TARDE É QUE FAZ RESERVAS E QUE ADICIONA CARTAO
        {
            nome = n;
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
            if(existeReserva(nova))
                reservas.Remove(nova);
        }

        public string ToString()
        {
            return $"Nome do Utilizador: {nome}\n" + cartao.ToString(); //FALTA MOSTRAR RESERVAS
        }
    }
}
