using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;

namespace ChessGenerator
{
    class Program
    {
        public static DiscreteUniform NumberOfPiecesDist = new DiscreteUniform(3, 13);
        public static DiscreteUniform AllPiecesDist = new DiscreteUniform(0, 9);
        public static DiscreteUniform NotPawnPiecesDist = new DiscreteUniform(0, 6);
        public static DiscreteUniform ColorDist = new DiscreteUniform(0, 1);
        public static DiscreteUniform BoardsDist = new DiscreteUniform(1, 28);
        public static DiscreteUniform PiecesDist = new DiscreteUniform(1, 32);

        public static readonly int NumberOfPositions = 100000;

        static void Main(string[] args)
        {
            for (int i = 0; i < NumberOfPositions; i++)
            {
                GeneratePosition();
                Console.WriteLine($"{i}/{NumberOfPositions}");
            }
        }

        public static void GeneratePosition()
        {
            var pieces = GeneratePieces();
            var board = GenerateBoard();
            board.Pieces.AddRange(pieces);
            board.SaveAsImage();
        }

        public static List<Piece> GeneratePieces()
        {
            List<Piece> pieces = new List<Piece>();

            var pieceCoordinates = Utils.GeneratePiecesCoordinates(NumberOfPiecesDist.Sample());
            var kingsCoordinates = Utils.GenerateKingCoordinates(pieceCoordinates);

            pieces.Add(new Piece(kingsCoordinates[0], Type.King, Color.White));
            pieces.Add(new Piece(kingsCoordinates[1], Type.King, Color.Black));

            foreach (var coordinate in pieceCoordinates)
            {
                if (coordinate.Y == 0 || coordinate.Y == 7)
                {
                    pieces.Add(new Piece(
                        coordinate,
                        Generator.GenerateNotPawnPiece(NotPawnPiecesDist.Sample()),
                        Generator.GenerateColor(ColorDist.Sample())));
                }
                else
                {
                    pieces.Add(new Piece(
                        coordinate,
                        Generator.GeneratePiece(AllPiecesDist.Sample()),
                        Generator.GenerateColor(ColorDist.Sample())));
                }
            }

            return pieces;
        }

        public static Board GenerateBoard()
        {
            return new Board(BoardsDist.Sample(), PiecesDist.Sample());
        }
    }
}
