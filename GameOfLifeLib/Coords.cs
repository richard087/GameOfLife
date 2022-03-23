using System;

namespace GameOfLifeLib
{
    public class Coords : IEquatable<Coords>
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coords);
        }

        public bool Equals(Coords other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public Coords[] GetNeighbours(int maxX, int maxY)
        {
            int[] cols = DoWrap(X, maxX);
            int[] rows = DoWrap(Y, maxY);
            return new Coords[] {   new Coords(cols[0], rows[0]) ,  new Coords(X, rows[0]) , new Coords(cols[1], rows[0]),
                                    new Coords(cols[0], Y) ,                                 new Coords(cols[1], Y),
                                    new Coords(cols[0], rows[1]) ,  new Coords(X, rows[1]) , new Coords(cols[1], rows[1]),};
        }

        private static int[] DoWrap(int val, int max, int min = 0)
        {
            int lesser = val - 1;
            int greater = val + 1;
            if (lesser < min)
            {
                lesser = max;
            }
            if (greater > max)
            {
                greater = min;
            }
            return new int[] { lesser, greater };
        }
        public override string ToString() => $"({X}, {Y})";
    }
}
