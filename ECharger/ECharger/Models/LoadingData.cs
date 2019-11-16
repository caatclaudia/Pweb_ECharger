using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECharger
{
    class LoadingData
    {
        private double duration;
        private double totalPrice;

        public LoadingData(double dur, double val)
        {
            duration = dur;
            totalPrice = val * duration;
        }

        public double Duration
        {
            get { return duration; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(duration)} has to be >= 0");
                }

                duration = value;
            }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(totalPrice)} has to be >= 0");
                }

                totalPrice = value;
            }
        }

        public string toString()
        {
            return $"Duration: {duration:F2}\n" + $"Total Price: {totalPrice:F2}\n";
        }
    }
}
