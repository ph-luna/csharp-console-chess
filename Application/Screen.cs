
using board;
using chess;
using Pastel;

class Screen
{
  private const string WhiteColor = "#FFFFFF";
  private const string BlackColor = "#000000";
  private const string YellowColor = "#ffe599";
  private const string GreyColor = "#5a5a5a";
  private const string SelectedGray = "#5fb073";
  private const string SelectedBlack = "#315c3b";

  public static void PrintMatch(ChessMatch chessMatch)
  {
    PrintBoard(chessMatch.Board);
    Console.WriteLine($"\nTurn: {chessMatch.Turn}");
    string currentPlayerColor = chessMatch.CurrentPlayer == Color.White ? WhiteColor : YellowColor;
    Console.Write($"Current Player: {chessMatch.CurrentPlayer}".Pastel(currentPlayerColor));
    if (chessMatch.InCheck)
    {
      Console.Write(" IN CHECK!!!!".Pastel(currentPlayerColor));
    }
    Console.WriteLine("\n\n-- Captured Pieces --");
    PrintPiecesSet(chessMatch, Color.White);
    PrintPiecesSet(chessMatch, Color.Black);

  }

  public static void PrintPiecesSet(ChessMatch chessMatch, Color color)
  {
    HashSet<Piece> pieces = chessMatch.CapturedPiecesByColor(color);
    string printColor = color == Color.White ? WhiteColor : YellowColor;
    Console.Write(color == Color.White ? "\nWhite: " : "\nBlack: ".Pastel(printColor));
    Console.Write("[".Pastel(printColor));
    foreach (Piece piece in pieces)
    {
      Console.Write(piece.ToString().Pastel(printColor) + " ");
    }
    Console.Write("]".Pastel(printColor));
  }

  public static void PrintCheckMate(ChessMatch chessMatch)
  {
    string printColor = chessMatch.CurrentPlayer == Color.White ? WhiteColor : YellowColor;
    Console.Clear();
    PrintBoard(chessMatch.Board);
    Console.WriteLine($"\n\nCHECKMATE! {chessMatch.CurrentPlayer} is the winner!\n\nPress any key to continue...".Pastel(printColor));
    Console.ReadKey();
  }

  public static void PrintPiece(Piece piece, bool isOdd, bool isPossible = false)
  {
    var background = isPossible ? SelectedBlack : BlackColor;
    var pieceColor = YellowColor;
    if (isOdd)
    {
      background = isPossible ? SelectedGray : GreyColor;
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

  public static void PrintBoard(Board board, bool[,] possibleMovements)
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
            string bgColor = possibleMovements[l, c] ? SelectedGray : GreyColor;
            Console.Write("  ".PastelBg(bgColor));
          }
          else
          {
            string bgColor = possibleMovements[l, c] ? SelectedBlack : BlackColor;
            Console.Write("  ".PastelBg(bgColor));
          }
        }
        else
        {
          bool isPossible = possibleMovements[l, c];
          PrintPiece(piece, isOdd, isPossible);
        }
      }
      Console.WriteLine();
    }
    Console.WriteLine("  a b c d e f g h");
  }

  public static ChessPosition ReadChessPosition()
  {
    string input = Console.ReadLine()?.ToLower() ?? "";
    char column = input[0];
    int line = int.Parse(input[1] + "");
    return new ChessPosition(column, line);
  }

  public static void PrintMessage(string message)
  {
    Console.Clear();
    Console.WriteLine($"\n[Message]: {message}\n\n\nPress any key to continue...");
    Console.ReadKey();
  }
}