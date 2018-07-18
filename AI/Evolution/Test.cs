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
    public class Test : GeneralizedCreature
    {
        public static Vector2 DESTINATION { get; set; }

        public static double BEST_FITNESS { get; set; }
        public static Test BEST_CREATURE { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Test()
        {
            Texture = Extension.GetColor(Color.Green);
            X = 5;
            Y = 90;
        }

        public Test(Color color) : base(color)
        {
            X = 5;
            Y = 90;

            CreatureBrain = new Brain(1, 16, 16, 4);
            CreatureBrain.Mesh();
        } 

        public override void Perceive() 
        {
            Fitness = (DESTINATION - new Vector2(X, Y)).Length();
            if(Fitness < Test.BEST_FITNESS)
            {
                Test.BEST_FITNESS = Fitness;
                Test.BEST_CREATURE = this;
            }

            double[] _results = CreatureBrain.Push(new double[] { Fitness });

            if (_results[0] > .5)
                X--;
            if (_results[1] > .5)
                X++;
            if (_results[2] > .5)
                Y--;
            if (_results[3] > .5)
                Y++;

            base.Perceive();
        }

        public override void Reset()
        {
            X = 5;
            Y = 90;

            base.Reset();
        }

        public static int SCALE = 10;

        public static int GX = 0;
        public static int GY = 0;

        public override void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(Texture, new Rectangle(X * SCALE + GX, Y * SCALE + GY, SCALE, SCALE), Color.White);
            base.Draw(sBatch);
        }
    }
}
