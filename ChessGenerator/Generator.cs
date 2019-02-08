using System;

namespace ChessGenerator
{
    public static class Generator
    {
        public static Color GenerateColor(int seed)
        {
            switch (seed)
            {
                case 0: return Color.White;
                case 1: return Color.Black;
                default: throw new Exception();
            }
        }

        public static Type GeneratePiece(int seed)
        {
            if(seed < 3)
            {
                return Type.Pawn;
            }
            else if(seed < 5)
            {
                return Type.Bishop;
            }
            else if(seed < 7)
            {
                return Type.Knight;
            }
            else if(seed < 9)
            {
                return Type.Rook;
            }
            else
            {
                return Type.Queen;
            }
        }

        public static Type GenerateNotPawnPiece(int seed)
        {
            if (seed < 2)
            {
                return Type.Bishop;
            }
            else if (seed < 4)
            {
                return Type.Knight;
            }
            else if (seed < 6)
            {
                return Type.Rook;
            }
            else
            {
                return Type.Queen;
            }
        }
    }
}
