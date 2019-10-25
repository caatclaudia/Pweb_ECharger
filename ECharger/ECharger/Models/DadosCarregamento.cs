using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class DadosCarregamento
    {
        private double duracao;
        private double precoTotal;

        public DadosCarregamento(double dur, double valor)
        {
            duracao = dur;
            precoTotal = valor * duracao;
        }

        public double Duracao
        {
            get { return duracao; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(duracao)} tem que ser >= 0");
                }

                duracao = value;
            }
        }

        public double PrecoTotal
        {
            get { return precoTotal; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(precoTotal)} tem que ser >= 0");
                }

                precoTotal = value;
            }
        }

        public string ToString()
        {
            return $"Duracao: {duracao:F2}\n" + $"PrecoTotal: {precoTotal:F2}\n";
        }
    }
}
