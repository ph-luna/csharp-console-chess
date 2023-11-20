using board;
using chess;

try
{
  ChessMatch chessMatch = new();

  while (!chessMatch.Finished)
  {
    Console.Clear();
    Screen.PrintBoard(chessMatch.Board);

    Console.Write("\nPiece Position: ");
    Position origin = Screen.ReadChessPosition().ToPosition();

    Console.Write("Destination: ");
    Position destination = Screen.ReadChessPosition().ToPosition();

    chessMatch.MakeMove(origin, destination);
  }
}
catch (Exception ex)
{
  Console.WriteLine(ex);
}
