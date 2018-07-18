using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TestingNNEvolution.AI.NeuralNetwork;
using TestingNNEvolution.Extensions;
namespace TestingNNEvolution.Extensions
{
    public class Ray2D
    {
        public Vector2 Base;
        public Vector2 Direction;

        private float _delta = .0f;
        public float Delta
        {
            get
            {
                return _delta;
            }

            set
            {
                _delta = value;
                Position = Base + _delta * Direction;
            }
        }

        public Vector2 Position { get; private set; }

        public Ray2D(Vector2 a, Vector2 b)
        {
            Base = a;
            Direction = b - a;
            Direction.Normalize();

        }

        public void GetDeltaByX(float x)
        {
            Delta = (x - Base.X) / Direction.X;
        }
        public void GetDeltaByY(float y)
        {
            Delta = (y - Base.Y) / Direction.Y;
        }

        public float? Intersects(Vector2 pt)
        {
            Vector2 ab = pt - Base;
            float length = ab.Length();

            ab.Normalize();
            if (ab.Equals(Direction))
                return length;
            else return null;

        }


    }
}
