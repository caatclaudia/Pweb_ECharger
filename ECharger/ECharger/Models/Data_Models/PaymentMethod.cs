using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class PaymentMethod
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private double value_;
        public int UserID { get; set; }
        public UserCard User { get; set; }

        public PaymentMethod(string n, double v)
        {
            Name = n;
            Value = v;
        }

        public double Value
        {
            get { return value_; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Value)} has to be >= 0");
                }

                value_ = value;
            }
        }

        public string toString()
        {
            return $"Payment Method: {Name} with the value {Value:F2} €\n";
        }
    }
}
