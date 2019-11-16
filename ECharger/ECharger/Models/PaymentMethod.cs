using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class PaymentMethod
    {
        public string name { get; set; }
        private double Value;

        public PaymentMethod(string n, double v)
        {
            name = n;
            Value = v;
        }

        public double Value_
        {
            get { return Value; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Value)} has to be >= 0");
                }

                Value = value;
            }
        }

        public string toString()
        {
            return $"Payment Method: {name} with the value {Value:F2} €\n";
        }
    }
}
