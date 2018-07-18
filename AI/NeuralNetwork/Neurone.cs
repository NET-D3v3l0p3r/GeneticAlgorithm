using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingNNEvolution.AI.NeuralNetwork
{
    [Serializable]
    public class Neurone
    {
        public IActivation Activation { get; set; }
        public List<Connection> Connections { get; set; }

        public double WeigtedInput { get; set; }
        public double Bias { get; set; }

        public Neurone(IActivation activation, double bias)
        {
            Activation = activation;
            Connections = new List<Connection>();

            Bias = bias;
        }
        public void Fire() { Connections.ForEach(p => p.Fire()); }
        public void Clear()
        {
            WeigtedInput = 0;
        }

        public void Set(double value) { WeigtedInput += value; }
        public double Get() { return Activation.Activation(Bias + WeigtedInput); }
    }
}
