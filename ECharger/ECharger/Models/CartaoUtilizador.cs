using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class CartaoUtilizador
    {
        public int pontos { get; set; }
        public List<MetodoPagamento> metodoPagamento { get; set; }

        public CartaoUtilizador(MetodoPagamento metodo)
        {
            metodoPagamento.Add(metodo);
            pontos = 0;
        }

        public void addPontos(int soma)
        {
            pontos += soma;
            
        }

        public void removePontos(int menos)
        {
            pontos -= menos;

        }

        public bool existeMetodo(MetodoPagamento novo)
        {
            foreach (MetodoPagamento aux in metodoPagamento)
            {
                if (aux.nome == novo.nome)
                    return true;
            }

            return false;
        }

        public MetodoPagamento procuraMetodo(string nome)
        {
            foreach (MetodoPagamento aux in metodoPagamento)
            {
                if (aux.nome == nome)
                    return aux;
            }
            return null;
        }
        
        public void addMetodo(MetodoPagamento novo)
        {
            if (existeMetodo(novo))
                return;
            metodoPagamento.Add(novo);
        }

        public void removeMetodo(MetodoPagamento novo)
        {
            if (existeMetodo(novo))
                metodoPagamento.Remove(novo);
        }

        public string ToString()
        {
            return $"Pontos: {pontos}\n"; //FALTA METODOS PAGAMENTO
        }
    }
}
