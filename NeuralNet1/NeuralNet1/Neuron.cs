using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet1
{
    public class Neuron : INeuron
    {
        public NeuralFactor Bias { get; set; }
        public double BiasWeight { get; set; }
        public double Error { get; set; }
        public Dictionary<INeuronSignal, NeuralFactor> Input { get; set; }
        public double Output { get; set; }

        public void ApplyLearning(INeuralLayer layer)
        {

        }

        public void Pulse(INeuralLayer layer)
        {
            lock(this)
            {
                this.Output = 0;

                foreach (KeyValuePair<INeuronSignal, NeuralFactor> item in this.Input)
                    this.Output += item.Key.Output * item.Value.Weight;

                this.Output += this.Bias.Weight * this.BiasWeight;
                this.Output = Sigmoid(this.Output);
            }
        }

        public double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }
    }
}
