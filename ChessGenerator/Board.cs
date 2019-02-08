using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Point = SixLabors.Primitives.Point;

namespace ChessGenerator
{
    public class Board
    {
        public readonly List<Piece> Pieces;
        public readonly int BoardStyle;
        public readonly int PiecesStyle;

        public Board(int boardStyle, int piecesStyle)
        {
            Pieces = new List<Piece>();
            BoardStyle = boardStyle;
            PiecesStyle = piecesStyle;
        }

        public string GetFEN()
        {
            var fen = "";

            for (int i = 0; i < 8; i++)
            {
                int emptyCounter = 0;

                for (int j = 0; j < 8; j++)
                {
                    if (Pieces.Any(q => q.Coordinate.Y == i && q.Coordinate.X == j))
                    {
                        if(emptyCounter != 0)
                        {
                            fen += emptyCounter.ToString();
                        }
                        fen += Pieces.First(q => q.Coordinate.Y == i && q.Coordinate.X == j).ToString();
                        emptyCounter = 0;
                    }
                    else
                    {
                        emptyCounter++;
                    }
                }

                fen += emptyCounter == 0 ? "" : $"{emptyCounter}";
                if(i != 7)
                {
                    fen += "-";
                }
            }

            return fen;
        }

        public void SaveAsImage()
        {
            using (Image<Rgba32> board = Image.Load($"boards/{BoardStyle}.png"))
            {
                foreach (var piece in Pieces)
                {
                    var path = $"pieces/{PiecesStyle}/{piece.GetFENSymbol()}_{piece.GetColorSymbol()}.png";
                    Image<Rgba32> p = Image.Load(path);
                    var x = piece.Coordinate.Y * 50;
                    var y = piece.Coordinate.X * 50;
                    board.Mutate(q => q.DrawImage(p, new Point(y, x), 1));
                }

                if(!Directory.Exists("dataset"))
                {
                    Directory.CreateDirectory("dataset");
                }

                var file = $"dataset/{GetFEN()}.jpeg";

                if (!File.Exists(file))
                {
                    board.Save(file);
                }
            }
        }
    }
}
