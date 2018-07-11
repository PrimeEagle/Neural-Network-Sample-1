using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet1
{
    public class NeuralNet : INeuralNet
    {
        public INeuralLayer HiddenLayer { get; set; }
        public INeuralLayer PerceptionLayer { get; set; }
        public INeuralLayer OutputLayer { get; set; }
        private double LearningRate { get; set; }

        public void Initialize(int randomSeed, int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount)
        {
            int layerCount;
            Random rand;
            INeuralLayer layer;

            rand = new Random(randomSeed);
            this.PerceptionLayer = new NeuralLayer();
            this.HiddenLayer = new NeuralLayer();
            this.OutputLayer = new NeuralLayer();

            for (int i = 0; i < inputNeuronCount; i++)
                this.PerceptionLayer.Add(new Neuron());

            for (int i = 0; i < outputNeuronCount; i++)
                this.OutputLayer.Add(new Neuron());

            for (int i = 0; i < hiddenNeuronCount; i++)
                this.HiddenLayer.Add(new Neuron());

            for (int i = 0; i < this.HiddenLayer.Count; i++)
                for (int j = 0; j < this.PerceptionLayer.Count; i++)
                    this.HiddenLayer[i].Input.Add(this.PerceptionLayer[j], new NeuralFactor(rand.NextDouble()));

            for (int i = 0; i < this.OutputLayer.Count; i++)
                for (int j = 0; j < this.HiddenLayer.Count; i++)
                    this.OutputLayer[i].Input.Add(this.HiddenLayer[j], new NeuralFactor(rand.NextDouble()));
        }

        public void ApplyLearning()
        {
            lock(this)
            {
                this.HiddenLayer.ApplyLearning(this);
                this.OutputLayer.ApplyLearning(this);
            }
        }

        public void Pulse()
        {
            lock(this)
            {
                this.HiddenLayer.Pulse(this);
                this.OutputLayer.Pulse(this);
            }
        }

        private void BackPropagation(double[] desiredResults)
        {
            int i, j;
            double temp, error;

            INeuron outputNode, inputNode, hiddenNode, node, node2;

            // Calcualte output error values
            for (i = 0; i < this.OutputLayer.Count; i++)
            {
                temp = this.OutputLayer[i].Output;
                this.OutputLayer[i].Error = (desiredResults[i] - temp) * temp * (1.0F - temp);
            }

            // calculate hidden layer error values
            for (i = 0; i < this.HiddenLayer.Count; i++)
            {
                node = this.HiddenLayer[i];

                error = 0;

                for (j = 0; j < this.OutputLayer.Count; j++)
                {
                    outputNode = this.OutputLayer[j];
                    error += outputNode.Error * outputNode.Input[node].Weight * node.Output * (1.0 - node.Output);
                }

                node.Error = error;
            }

            // adjust output layer weight change
            for (i = 0; i < this.HiddenLayer.Count; i++)
            {
                node = this.HiddenLayer[i];

                for (j = 0; j < this.OutputLayer.Count; j++)
                {
                    outputNode = this.OutputLayer[j];
                    outputNode.Input[node].Weight += this.LearningRate * this.OutputLayer[j].Error * node.Output;
                    outputNode.Bias.Delta += this.LearningRate * this.OutputLayer[j].Error * outputNode.Bias.Weight;
                }
            }

            // adjust hidden layer weight change
            for (i = 0; i < this.PerceptionLayer.Count; i++)
            {
                inputNode = this.PerceptionLayer[i];

                for (j = 0; j < this.HiddenLayer.Count; j++)
                {
                    hiddenNode = this.HiddenLayer[j];
                    hiddenNode.Input[inputNode].Weight += this.LearningRate * hiddenNode.Error * inputNode.Output;
                    hiddenNode.Bias.Delta += this.LearningRate * hiddenNode.Error * inputNode.Bias.Weight;
                }
            }
        }

        public void Train(double[] input, double[] desiredResult)
        {
            if (input.Length != this.PerceptionLayer.Count)
                throw new ArgumentException($"Expecting {this.PerceptionLayer.Count} inputs for this net");

            // initialize data
            for (int i = 0; i < this.PerceptionLayer.Count; i++)
            {
                Neuron n = this.PerceptionLayer[i] as Neuron;

                if (n != null)
                    n.Output = input[i];
            }

            Pulse();
            BackPropagation(desiredResult);
        }

        public void Train(double[][] inputs, double[][] expected)
        {
            for (int i = 0; i < inputs.Length; i++)
                Train(inputs[i], expected[i]);
        }
    }
}
