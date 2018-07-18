using TestingNNEvolution.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingNNEvolution.AI.NeuralNetwork;

namespace TestingNNEvolution.AI.NeuralNetwork
{
    [Serializable]
    public class Layer
    {
        public IActivation ActivationFunction { get; set; }

        public Layer PreviousLayer { get; set; }
        public Layer NextLayer { get; set; }

        public int NeuroneCount { get; private set; }
        public Neurone[] Neurones { get; private set; }

        public double Bias { get; private set; }

        public DNA Parent { get; private set; }
        public DNA DNA { get; private set; }

        private double[] _ParentWeights;

        public Layer(int n, IActivation f, DNA parent = null)
        {
            NeuroneCount = n;
            ActivationFunction = f;

            Neurones = new Neurone[n];

            Parent = parent;
            Bias = Extension.Random.Next(-1000, 1000) * 0.001;
            if (parent != null)
            {
                Bias = parent.GetBias();
                _ParentWeights = parent.ToWeights();
            }
            DNA = new DNA(Bias);

            for (int i = 0; i < Neurones.Length; i++)
                Neurones[i] = new Neurone(f, Bias);
        }

        public void MeshWithNextLayer()
        {
            int index = 0;
            for (int i = 0; i < Neurones.Length; i++)
            {
                Neurone current = Neurones[i];
                for (int j = 0; j < NextLayer.Neurones.Length; j++)
                {
                    var weight = Extension.Random.Next(-1000, 1000) * 0.001;
                    if (Parent != null)
                        weight = _ParentWeights[index++];

                    current.Connections.Add(new Connection(current, NextLayer.Neurones[j], weight));
                    DNA.AddWeight(weight);
                }
            }
        }



    

        
    }
}
