using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    public class GameOfLife
    {
        private int Height { get; }
        private int Width { get; }
        private HashSet<Coords> liveSet;
        public GameOfLife(int width, int height)
        {
            Width = width;
            Height = height;
            liveSet = new HashSet<Coords>();
        }
        public GameOfLife setState(int x, int y, bool alive = true)
        {
            if (alive)
            {
                liveSet.Add(new Coords(x, y));
            }
            else
            {
                liveSet.Remove(new Coords(x, y));
            }
            return this;
        }
        public bool[,] render()
        {
            bool[,] world = new bool[Width, Height];
            Array.Clear(world, 0, Width * Height);
            foreach (Coords cell in liveSet)
            {
                world[cell.X, cell.Y] = true;
            }
            return world;
        }
        public GameOfLife run(uint generations = 1)
        {
            ConcurrentBag<Coords> nextGen;
            ConcurrentBag<Coords> mightLive;
            for (uint gen = 0; gen < generations; gen++)
            {
                nextGen = new ConcurrentBag<Coords>();
                mightLive = new ConcurrentBag<Coords>();
                int liveNeighbours;
                Parallel.ForEach(liveSet, cell =>
                {
                    Coords[] neighbours = cell.GetNeighbours(Width - 1, Height - 1);
                    Parallel.ForEach(neighbours, cell => {
                        mightLive.Add(cell);
                    });
                    liveNeighbours = liveSet.Intersect(neighbours).Count();
                    if (liveNeighbours == 2 || liveNeighbours == 3)
                    {
                        nextGen.Add(cell);
                    }
                });
                Parallel.ForEach(mightLive.Distinct(), cell =>
                {
                    Coords[] neighbours = cell.GetNeighbours(Width - 1, Height - 1);
                    liveNeighbours = liveSet.Intersect(neighbours).Count();
                    if (liveNeighbours == 3)
                    {
                        nextGen.Add(cell);
                    }
                });
                liveSet = new HashSet<Coords>(nextGen);
            }
            return this;
        }

    }
}
