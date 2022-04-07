using GameOfLifeLib;
using System;
using System.Text;
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
            const int edgeLength = 20;
            const int maxTicks = 1000000;
            int currentTick = 0;
            int liveCells = -1;
            Task nextTick;
            foreach (string arg in args)
            {
                Console.Out.WriteLine("Got arg " + arg);
            }
            GameOfLife g = new GameOfLife(edgeLength, edgeLength);
            g.setState(2, 1).setState(2, 2).setState(2, 3);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Out.Write(renderWorld(g.render()));
            nextTick = Task.Run(() => { g.run(); });
            Console.SetCursorPosition(0, 0);
            while (currentTick < maxTicks)
            {
                Console.Out.Flush();
                await nextTick;
                Console.Out.Write(renderWorld(g.render()));
                //liveCells = g.GetLiveCount();
                nextTick = Task.Run(() => { g.run(); });
                Console.WriteLine(liveCells);
                Console.SetCursorPosition(0, 0);
                currentTick++;
            }
            Console.Out.Flush();
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
