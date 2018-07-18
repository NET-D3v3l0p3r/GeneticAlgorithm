using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestingNNEvolution.AI.NeuralNetwork;
using TestingNNEvolution.Extensions;

namespace TestingNNEvolution.AI.Evolution
{
    public class GeneralizedCreature
    {
        public Brain CreatureBrain { get; set; }
        public double Fitness { get; set; }
        
        public Texture2D Texture { get; set; }

        public GeneralizedCreature(Color color)
        {
            Texture = Extension.GetColor(color);
        }

        public GeneralizedCreature() { }

        public virtual void Perceive() { }
        public virtual void Reset() { }
        
        public T GetChildren<T>(double mutationRate, int amount) where T : GeneralizedCreature, new()
        {
            if (CreatureBrain == null)
                return default(T);
            if (!CreatureBrain.IsMeshed)
                return default(T);

            DNA _DNAChild = DNA.CrossOver(CreatureBrain.DNA, CreatureBrain.DNA);
            _DNAChild.Mutate(mutationRate, amount);


            T _NewChild = new T()
            {
                CreatureBrain =
                new Brain(
                    CreatureBrain.Layers[0].NeuroneCount,
                    CreatureBrain.Layers[1].NeuroneCount,
                    CreatureBrain.Layers[2].NeuroneCount,
                    CreatureBrain.Layers[3].NeuroneCount,
                    _DNAChild)
            };

            _NewChild.CreatureBrain.Mesh();

            return (T)_NewChild;
        }

        public virtual void Draw(SpriteBatch sBatch) { }
    }
}
