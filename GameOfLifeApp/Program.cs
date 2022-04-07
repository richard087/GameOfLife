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
        public static int Main(string[] args) {
            const int edgeLength = 40;
            const int maxTicks = 10000;
            bool[,] world = CreateWorld(0.5, edgeLength);
            GameOfLife g = new GameOfLife(world);
            // Give the test as good a chance as possible
            // of avoiding garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            DateTime start = DateTime.Now;
            g.Run(maxTicks);
            DateTime end = DateTime.Now;
            Console.WriteLine("  {0,-20} {1}", "single thread", end - start);
            g = new GameOfLife(world);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            start = DateTime.Now;
            g.RunParallel(maxTicks);
            end = DateTime.Now;
            Console.WriteLine("  {0,-20} {1}", "parallel", end - start);
            return 0;
        }

        private static bool[,] CreateWorld(double populated, int edgeLength) { 
            Random random = new Random();
            bool[,] world = new bool[edgeLength, edgeLength];
            for (int x = 0; x < edgeLength; x++)
            {
                for (int y = 0; y < edgeLength; y++) {
                    world[x, y] = random.NextDouble() <= populated;
                }
            }
            return world;
        }
        static int ShowAnimation()
        {
            const int edgeLength = 20;
            const int maxTicks = 100000;
            int currentTick = 0;
            int liveCells = -1;
            Task nextTick;

            GameOfLife g = new GameOfLife(edgeLength, edgeLength);
            g.setState(2, 1).setState(2, 2).setState(2, 3);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Out.Write(RenderWorld(g.render()));
            nextTick = Task.Run(() => { g.Run(); });
            Console.SetCursorPosition(0, 0);
            while (currentTick < maxTicks)
            {
                Console.Out.Flush();
                nextTick.Wait();
                Console.Out.Write(RenderWorld(g.render()));
                //liveCells = g.GetLiveCount();
                nextTick = Task.Run(() => { g.Run(); });
                Console.WriteLine(liveCells);
                Console.SetCursorPosition(0, 0);
                currentTick++;
            }
            Console.Out.Flush();
            return 0;
        }

        static string RenderWorld(bool[,] world)
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
