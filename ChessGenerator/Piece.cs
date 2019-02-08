using System;

namespace ChessGenerator
{
    public class Piece
    {
        public readonly Coordinate Coordinate;
        public readonly Type Type;
        public readonly Color Color;

        public Piece(Coordinate coordinate, Type type, Color color)
        {
            Coordinate = coordinate;
            Type = type;
            Color = color;
        }

        public override string ToString()
        {
            var fen = GetFENSymbol();
            if(Color == Color.White)
            {
                return fen.ToUpper();
            }

            return fen;
        }

        public string GetColorSymbol()
        {
            return Color == Color.White ? "w" : "b";
        }

        public string GetFENSymbol()
        {
            switch (Type)
            {
                case Type.Bishop: return "b";
                case Type.Knight: return "n";
                case Type.King: return "k";
                case Type.Queen: return "q";
                case Type.Rook: return "r";
                case Type.Pawn: return "p";
                default: throw new Exception();
            }
        }
    }
}
