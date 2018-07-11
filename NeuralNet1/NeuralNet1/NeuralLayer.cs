using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet1
{
    public class NeuralLayer : INeuralLayer
    {
        List<INeuron> Neurons { get; set; }

        public INeuron this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(INeuron item)
        {
            this.Neurons.Add(item);
        }

        public void ApplyLearning(INeuralNet net)
        {
            foreach (INeuron n in this.Neurons)
                n.ApplyLearning(this);
        }

        public void Clear()
        {
            this.Neurons.Clear();
        }

        public bool Contains(INeuron item)
        {
            return this.Neurons.Contains(item);
        }

        public void CopyTo(INeuron[] array, int arrayIndex)
        {
            this.Neurons.CopyTo(array, arrayIndex);
        }

        public IEnumerator<INeuron> GetEnumerator()
        {
            return this.Neurons.GetEnumerator();
        }

        public int IndexOf(INeuron item)
        {
            return this.Neurons.IndexOf(item);
        }

        public void Insert(int index, INeuron item)
        {
            this.Neurons.Insert(index, item);
        }

        public void Pulse(INeuralNet net)
        {
            foreach (INeuron n in this.Neurons)
                n.Pulse(this);
        }

        public bool Remove(INeuron item)
        {
            return this.Neurons.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.Neurons.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Neurons.GetEnumerator();
        }
    }
}
