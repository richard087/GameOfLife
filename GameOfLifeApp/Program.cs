using GameOfLifeLib;
using System;
using System.Threading.Tasks;

namespace GameOfLifeApp
{
    class Program
    {
        /// <summary>
        /// Game Of Life
        /// </summary>
        /// <param name="x">horizontal size. Default is 5.</param>
        /// <param name="y">vertical size. Default is 5.</param>
        public static async Task<int> Main(string[] args)
        {
            foreach (string arg in args)
            {
                Console.Out.WriteLine("Got arg " + arg);
            }
            GameOfLife g = new GameOfLife(5, 5);
            g.setState(2, 1).setState(2, 2).setState(2, 3);
            Console.Out.Write(renderWorld(g.render()));
            await Task.Run(() => { g.run(); });
            Console.Out.Write(renderWorld(g.render()));
            Console.Out.Write(renderWorld(g.run().render()));
            return 0;
        }

        static string renderWorld(bool[,] world)
        {
            string res = "";
            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world.GetLength(1); j++)
                {
                    res += world[i, j] ? '#' : ' ';
                }
                res += "\n";
            }
            return res;
        }
    }
}
