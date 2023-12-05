using System;

namespace Gameplay.Fields.Cells
{
    [Serializable]
    public struct CoordinatesValues
    {
        public int X;
        public int Z;

        public CoordinatesValues(int x, int z)
        {
            X = x;
            Z = z;
        }
    }
}