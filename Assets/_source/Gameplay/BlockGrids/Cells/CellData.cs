namespace Infrastructure.Services.GameFactoryServices
{
    public class CellData
    {
        public CellData(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public Coordinates Coordinates { get; }
    }

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