
using board;
using Pastel;

class Screen
{
  private const string WhiteColor = "#FFFFFF";
  private const string BlackColor = "#000000";
  private const string YellowColor = "#ffe599";
  private const string GreyColor = "#5a5a5a";
  public static void PrintPiece(Piece piece, bool isOdd)
  {
    var background = BlackColor;
    var pieceColor = YellowColor;
    if (isOdd)
    {
      background = GreyColor;
    }
    if (piece.Color == Color.White)
    {
      pieceColor = WhiteColor;
    }

    Console.Write(piece.ToString().Pastel(pieceColor).PastelBg(background) + " ".PastelBg(background));
  }

  public static void PrintBoard(Board board)
  {
    for (int l = 0; l < board.Lines; l++)
    {
      Console.Write(8 - l + " ");
      for (int c = 0; c < board.Columns; c++)
      {
        bool isOdd = (l + c) % 2 != 0;
        var piece = board.GetPieceInPosition(l, c);
        if (piece is null)
        {
          if (isOdd)
          {
            Console.Write("  ".PastelBg(GreyColor));
          }
          else
          {
            Console.Write("  ");
          }
        }
        else
        {
          PrintPiece(piece, isOdd);
        }
      }
      Console.WriteLine();
    }
    Console.WriteLine("  a b c d e f g h");
  }
}