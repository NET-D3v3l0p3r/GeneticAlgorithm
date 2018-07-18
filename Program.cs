using System;
using TestingNNEvolution.AI.NeuralNetwork;
using TestingNNEvolution.Extensions;

namespace TestingNNEvolution
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }

    }
#endif
}
