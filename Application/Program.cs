using board;
using chess;

try
{
  Board board = new(8, 8);
  board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
  board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
  board.PlacePiece(new Rook(board, Color.Black), new Position(7, 7));
  board.PlacePiece(new King(board, Color.Black), new Position(3, 4));
  Screen.printBoard(board);
}
catch (Exception ex)
{
  Console.WriteLine(ex);
}
