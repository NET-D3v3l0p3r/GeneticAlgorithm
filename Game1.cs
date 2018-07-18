using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestingNNEvolution.AI.Evolution;
using TestingNNEvolution.AI.NeuralNetwork;
using TestingNNEvolution.Extensions;

namespace TestingNNEvolution
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Test> Creatures = new List<Test>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();


            base.Initialize();
        }

        Texture2D destColor;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Extension.Device = GraphicsDevice;
            destColor = Extension.GetColor(Color.Blue);


            for (int i = 0; i < 150; i++)
            {
                Test c = new Test(Color.Green);
                Creatures.Add(c);
            }


        }
        protected override void UnloadContent()
        {
       
        }

 
        double lastTime;

        Test lastCreature;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Test.DESTINATION = new Vector2(Mouse.GetState().X / Test.SCALE, Mouse.GetState().Y / Test.SCALE);

            for (int i = 0; i < Creatures.Count; i++)
                Creatures[i].Perceive();

            lastCreature?.Perceive();

            if (gameTime.TotalGameTime.TotalSeconds - lastTime > 2)
            {
                lastTime = gameTime.TotalGameTime.TotalSeconds;

                Test bestCreature = Test.BEST_CREATURE;

                if (bestCreature == null)
                    bestCreature = Creatures.OrderBy(p => p.Fitness).ToList()[0];
 
                
                Creatures.Clear();

                for (int i = 0; i < 150; i++)
                {
                    Creatures.Add(bestCreature.GetChildren<Test>(0.1, 15));
                }

                Console.WriteLine("Nearest one: " + Test.BEST_FITNESS);

                lastCreature = bestCreature;
                lastCreature.Reset();

                Test.BEST_CREATURE = null;
                Test.BEST_FITNESS = 1000;

 



            }

  

 

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            spriteBatch.Draw(destColor, new Rectangle((int)Test.DESTINATION.X * Test.SCALE, (int)Test.DESTINATION.Y * Test.SCALE, Test.SCALE, Test.SCALE), Color.White);

            lastCreature?.Draw(spriteBatch);

             
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
