using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class Reservas
    {
        public DateTime data { get; set; }
        public DadosCarregamento dados { get; set; }

        public Reservas(DateTime dat, DadosCarregamento dad) //CONFIRMAR DEVIDO A PASSAGEM DO PRECO PARA DADOSCARREGAMENTO
        {
            data = dat;
            dados = dad;
        }

        public string ToString()
        {
            return $"Data: {data}\n" + dados.ToString();
        }
    }
}
