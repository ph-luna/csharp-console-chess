using board;
using chess;

try
{
  ChessMatch chessMatch = new();

  while (!chessMatch.Finished)
  {
    Console.Clear();
    Screen.PrintBoard(chessMatch.Board);

    Console.Write("\nPiece at Position: ");
    Position origin = Screen.ReadChessPosition().ToPosition();

    bool[,] possibleMoviments = chessMatch.Board.GetPieceInPosition(origin)!.GetPossibleMoviments();
    Console.Clear();
    Screen.PrintBoard(chessMatch.Board, possibleMoviments);

    Console.Write("\nDestination: ");
    Position destination = Screen.ReadChessPosition().ToPosition();

    chessMatch.MakeMove(origin, destination);
  }
}
catch (Exception ex)
{
  Console.WriteLine(ex);
}
