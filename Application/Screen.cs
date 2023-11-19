
using board;

class Screen
{
  public static void printBoard(Board board)
  {
    for (int l = 0; l < board.Lines; l++)
    {
      for (int c = 0; c < board.Columns; c++)
      {
        var piece = board.GetPieceInPosition(l, c);
        if (piece is null)
        {
          bool isOdd = (l + c) % 2 != 0;
          Console.Write(isOdd ? "□ " : "■ ");
        }
        else
        {
          Console.Write(piece + " ");
        }
      }
      Console.WriteLine();
    }
  }
}