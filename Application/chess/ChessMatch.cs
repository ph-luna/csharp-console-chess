using board;

namespace chess;

class ChessMatch
{
  public Board Board { get; private set; }
  public bool Finished { get; private set; }
  private int _turn;
  private Color _currentPlayer;

  public ChessMatch()
  {
    Board = new Board(8, 8);
    _turn = 1;
    _currentPlayer = Color.White;
    Finished = false;

    PlaceAllPieces();
  }

  public void MakeMove(Position origin, Position destination)
  {
    Piece? piece = Board.RemovePiece(origin);
    if (piece is null) return;
    piece.IncreaseMoviments();
    Board.RemovePiece(destination);
    Board.PlacePiece(piece, destination);
  }

  private void PlaceAllPieces()
  {
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('a', 1).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('h', 1).ToPosition());

    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('a', 8).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('h', 8).ToPosition());
  }
}
