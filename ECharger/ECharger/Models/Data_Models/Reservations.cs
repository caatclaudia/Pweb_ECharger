using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class Reservations
    {
        public DateTime date { get; set; }
        public LoadingData data { get; set; }

        public Reservations(DateTime dat, LoadingData ob) //CONFIRMAR DEVIDO A PASSAGEM DO PRECO PARA DADOSCARREGAMENTO
        {
            date = dat;
            data = ob;
        }

        public string toString()
        {
            return $"Data: {date}\n" + data.ToString();
        }
    }
}
