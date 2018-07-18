using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingNNEvolution.Extensions;

namespace TestingNNEvolution.AI.NeuralNetwork
{
    [Serializable]
    public class DNA
    {
        public string Sequence { get; set; }
        public int Length { get; private set; }

        public List<DNA> FullSequence { get; private set; }

        public DNA(double bias)
        {
            FullSequence = new List<DNA>();
            AddWeight(bias);
        }

        public void AddWeight(double value)
        {
            value /= 0.001;
            string bin = Extension.GetBinary10Bit(value);
            Sequence += bin;

            Length += bin.Length;
        }
        public double GetBias()
        {
            return Extension.GetDouble10Bit(Sequence.Substring(0, 11)) * 0.001;
        }
        public double[] ToWeights()
        {
            List<double> _Seq = new List<double>();
            string[] _s = Sequence.GetParts(11);

            for (int i = 1; i < _s.Length; i++)
                _Seq.Add(Extension.GetDouble10Bit(_s[i]) * 0.001);

            return _Seq.ToArray();
        }
        public void Add(DNA dna)
        {
            FullSequence.Add(dna);
            Sequence += dna.Sequence;
            Length += dna.Sequence.Length;
        } 
        public string Cut(int index)
        {
            string[] _s = Sequence.GetParts(11);
            string newDNA = "";
            for (int i = 0; i < _s.Length; i++)
            {
                if (i == index)
                    continue;
                newDNA += _s[i];
            }

            Length -= 11;
            Sequence = newDNA;

            return _s[index];
        }

        public static DNA Combine(DNA a, DNA b)
        {
            char[] seqA = a.Sequence.ToCharArray();
            char[] seqB = b.Sequence.ToCharArray();

            if (seqA.Length != seqB.Length)
                return null;

            int crossOverPoint = Extension.Random.Next(0, seqA.Length);


            List<char> newDNA = new List<char>();
            var currentDNA = seqA;

            for (int i = 0; i < currentDNA.Length; i++)
            {
                if (i >= crossOverPoint)
                    currentDNA = seqB;

                newDNA.Add(currentDNA[i]);
            }

            DNA combined = new DNA(1)
            {
                Sequence = new string(newDNA.ToArray()),
                Length = newDNA.Count
            };

            return combined;


        }



        public static DNA CrossOver(DNA a, DNA b)
        {
            
            DNA parent = new DNA(1);
            parent.Cut(0);

            for (int i = 0; i < a.FullSequence.Count; i++)
            {
                DNA _combined = DNA.Combine(a.FullSequence[i], b.FullSequence[i]);
                parent.Add(_combined);
            }


            return parent;
        }

        public void Mutate(double mutationRate, int amount = 1)
        {

            if (amount == 0)
                return;

            int treshold = (int)((double)Sequence.Length * mutationRate);
            int count = 0;
            for (int i = 0; i < FullSequence.Count; i++)
            {
                if (Extension.Random.Next(0, Sequence.Length) > treshold)
                    continue;

                char[] _seq = FullSequence[i].Sequence.ToCharArray();
                for (int j = 0; j < FullSequence[i].Sequence.Length; j++)
                {
                    if (Extension.Random.Next(0, Sequence.Length) > treshold)
                        continue;
                    _seq[j] = _seq[j] == '0' ? '1' : '0';
                    count++;
                }

                FullSequence[i].Sequence = new string(_seq);
            }


 

            Mutate(mutationRate, amount - 1);
        }
    }
}
