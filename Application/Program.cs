using board;
using chess;

try
{
  ChessPosition chessPosition = new('a', 1);

  Console.WriteLine(chessPosition.ToPosition());
}
catch (Exception ex)
{
  Console.WriteLine(ex);
}
