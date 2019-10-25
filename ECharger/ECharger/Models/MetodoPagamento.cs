using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class MetodoPagamento
    {
        public string nome { get; set; }
        private double valor;

        public MetodoPagamento(string n, double v)
        {
            nome = n;
            valor = v;
        }

        public double Valor
        {
            get { return valor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(valor)} tem que ser >= 0");
                }

                valor = value;
            }
        }

        public string ToString()
        {
            return $"Metodo de Pagamento: {nome} com o valor de {valor:F2} €\n";
        }
    }
}
