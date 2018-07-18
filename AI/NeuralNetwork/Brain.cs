using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingNNEvolution.AI.NeuralNetwork;
using TestingNNEvolution.AI.NeuralNetwork.Activation;
using TestingNNEvolution.Extensions;

namespace TestingNNEvolution.AI.NeuralNetwork
{
    [Serializable]
    public class Brain
    {
        public List<Layer> Layers { get; private set; }
        public DNA DNA { get; private set; }
        public bool IsMeshed { get; private set; }

        public Brain(int input, int hidden1, int hidden2, int output, DNA parent = null)
        {
  
            Layers = new List<Layer>
            {
                new Layer(input, new Linear(),   parent?.FullSequence[0]),

                new Layer(hidden1, new Sigmoid(), parent?.FullSequence[1]),
                new Layer(hidden2, new Sigmoid(), parent?.FullSequence[2]),

                new Layer(output, new Sigmoid(),  parent?.FullSequence[3])
            };

            DNA = new DNA(1);
            DNA.Cut(0); 

        }

        public void Mesh()
        {
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                Layer CURRENT = Layers[i];
                Layer NEXT = Layers[i + 1];

                NEXT.PreviousLayer = CURRENT;
                CURRENT.NextLayer = NEXT;

                CURRENT.MeshWithNextLayer();
                DNA.Add(CURRENT.DNA);
            }

            DNA.Add(Layers[Layers.Count - 1].DNA);

            IsMeshed = true;
        }

        public double[] Push(double[] values)
        {
            for (int i = 0; i < Layers[0].Neurones.Length; i++)
            {
                Layers[0].Neurones[i].Set(values[i]);
                Layers[0].Neurones[i].Fire();
            }

            for (int i = 1; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].Neurones.Length; j++)
                {
                    Layers[i].Neurones[j].Fire();
                }
            }

            List<double> results = new List<double>();
            for (int i = 0; i < Layers[Layers.Count - 1].Neurones.Length; i++)
                results.Add(Layers[Layers.Count - 1].Neurones[i].Get());

            for (int i = 0; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].Neurones.Length; j++)
                {
                    Layers[i].Neurones[j].Clear();
                }
            }

            return results.ToArray();
            
        }
    }
}
