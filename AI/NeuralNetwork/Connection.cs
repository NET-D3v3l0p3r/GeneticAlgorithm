using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingNNEvolution.Extensions;
namespace TestingNNEvolution.AI.NeuralNetwork
{
    [Serializable]
    public class Connection
    {
        public Neurone Dendrite { get; set; }
        public Neurone Axon { get; set; }

        public double Weight { get; set; }

        public Connection(Neurone a, Neurone b, double weight)
        {
            Dendrite = a;
            Axon = b;

            Weight = weight;
        }

        public void Fire()
        {
            Axon.Set(Dendrite.Get() * Weight);
        }

    }
}
