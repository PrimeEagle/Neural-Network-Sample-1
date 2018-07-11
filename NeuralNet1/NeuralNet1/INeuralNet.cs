using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet1
{
    public interface INeuralNet
    {
        INeuralLayer PerceptionLayer { get; set; }
        INeuralLayer HiddenLayer { get; set; }
        INeuralLayer OutputLayer { get; set; }

        void ApplyLearning();
        void Pulse();
    }
}
