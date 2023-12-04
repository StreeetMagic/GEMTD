using System;

namespace Gameplay.Fields
{
    [Serializable]
    public struct Coordinates
    {
        public int X;
        public int Z;

        public Coordinates(int x, int z)
        {
            X = x;
            Z = z;
        }
    }
}