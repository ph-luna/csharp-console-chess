using board;

namespace chess;

class ChessMatch
{
  public Board Board { get; private set; }
  public bool Finished { get; private set; }
  public int Turn { get; private set; }
  public Color CurrentPlayer { get; private set; }

  public ChessMatch()
  {
    Board = new Board(8, 8);
    Turn = 1;
    CurrentPlayer = Color.White;
    Finished = false;

    PlaceAllPieces();
  }

  public void MakeMove(Position origin, Position destination)
  {
    Piece? piece = Board.RemovePiece(origin);
    if (piece is null) return;
    piece.IncreaseMovements();
    Board.RemovePiece(destination);
    Board.PlacePiece(piece, destination);
  }

  public void MakePlay(Position origin, Position destination)
  {
    MakeMove(origin, destination);
    Turn++;
    CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
  }

  public void ValidateOrigin(Position origin)
  {
    Piece? pieceInPosition = Board.GetPieceInPosition(origin) ?? throw new BoardException("No piece in this position.");
    if (pieceInPosition.Color != CurrentPlayer) throw new BoardException("You can't move other player's piece.");
    if (!pieceInPosition.HasPossibleMovements()) throw new BoardException("That piece has no possible movements.");
  }

  public void ValidateDestination(Position origin, Position destination)
  {
    Piece pieceInPosition = Board.GetPieceInPosition(origin)!;
    if (!pieceInPosition.CanMoveTo(destination)) throw new BoardException("You can't move this piece to this position.");
  }

  private void PlaceAllPieces()
  {
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
    Board.PlacePiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
    Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
    Board.PlacePiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
  }
}
