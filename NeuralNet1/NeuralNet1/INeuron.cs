using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet1
{
    public interface INeuron : INeuronSignal, INeuronReceptor   
    {
        void Pulse(INeuralLayer layer);
        void ApplyLearning(INeuralLayer layer);

        NeuralFactor Bias { get; set; }
        double BiasWeight { get; set; }
        double Error { get; set; }
    }
}
