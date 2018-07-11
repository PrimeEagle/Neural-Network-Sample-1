using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet1
{
    public class NeuralFactor
    {
        public double Weight { get; set; }
        public double Delta { get; set; }

        public NeuralFactor(double weight)
        {
            this.Weight = weight;
            this.Delta = 0;
        }

        public void ApplyDelta()
        {
            this.Weight += this.Delta;
            this.Delta = 0;
        }
    }
}
