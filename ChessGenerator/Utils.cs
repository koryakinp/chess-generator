using MathNet.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace ChessGenerator
{
    public static class Utils
    {
        public static readonly List<Coordinate> Coordinates;

        static Utils()
        {
            Coordinates = new List<Coordinate>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Coordinates.Add(new Coordinate(i, j));
                }
            }
        }

        public static List<Coordinate> GenerateKingCoordinates(List<Coordinate> pieceCoordinates)
        {
            var kingsCoordinates = new List<Coordinate>();
            double distance = 0;
            do
            {
                kingsCoordinates = Coordinates
                    .Where(q => pieceCoordinates.All(w => w.X != q.X || w.Y != q.Y))
                    .SelectCombination(2)
                    .ToList();

                var eucl = kingsCoordinates
                    .Select(q => new double[] { q.X, q.Y })
                    .ToArray();

                distance = Distance.Euclidean(eucl[0], eucl[1]);
            }
            while (distance < 2);

            return kingsCoordinates;
        }

        public static List<Coordinate> GeneratePiecesCoordinates(int numberOdPieces)
        {
            return Coordinates.SelectCombination(numberOdPieces).ToList();
        }
    }
}
