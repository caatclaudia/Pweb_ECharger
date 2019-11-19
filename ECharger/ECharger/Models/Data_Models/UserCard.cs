using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class UserCard
    {
        public int points { get; set; }
        public List<PaymentMethod> paymentMethod { get; set; }

        public UserCard(PaymentMethod ob)
        {
            paymentMethod.Add(ob);
            points = 0;
        }

        public void addPoints(int num)
        {
            points += num;
            
        }

        public void removePoints(int num)
        {
            points -= num;

        }

        public bool existMethod(PaymentMethod ob)
        {
            foreach (PaymentMethod aux in paymentMethod)
            {
                if (aux.name == ob.name)
                    return true;
            }

            return false;
        }

        public PaymentMethod searchMethod(string name)
        {
            foreach (PaymentMethod aux in paymentMethod)
            {
                if (aux.name == name)
                    return aux;
            }
            return null;
        }
        
        public void addMethod(PaymentMethod ob)
        {
            if (existMethod(ob))
                return;
            paymentMethod.Add(ob);
        }

        public void removeMetodo(PaymentMethod ob)
        {
            if (existMethod(ob))
                paymentMethod.Remove(ob);
        }

        public string toString()
        {
            return $"Points: {points}\n"; //FALTA METODOS PAGAMENTO
        }
    }
}
