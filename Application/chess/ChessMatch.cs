using System.Security.Cryptography;
using board;

namespace chess;

class ChessMatch
{
  public Board Board { get; private set; }
  public bool Finished { get; private set; }
  public int Turn { get; private set; }
  public Color CurrentPlayer { get; private set; }
  public HashSet<Piece> CapturedPieces { get; private set; }
  public HashSet<Piece> InGamePieces { get; private set; }
  public bool InCheck { get; private set; }

  public ChessMatch()
  {
    Board = new Board(8, 8);
    Turn = 1;
    CurrentPlayer = Color.White;
    Finished = false;
    InGamePieces = new HashSet<Piece>();
    CapturedPieces = new HashSet<Piece>();

    PlaceAllPieces();
  }

  public Piece? MakeMove(Position origin, Position destination)
  {
    Piece piece = Board.RemovePiece(origin)!;
    piece.IncreaseMovements();
    Piece? capturedPiece = Board.RemovePiece(destination);
    if (capturedPiece is not null)
    {
      CapturedPieces.Add(capturedPiece);
      InGamePieces.Remove(capturedPiece);
    };
    Board.PlacePiece(piece, destination);
    return capturedPiece;
  }

  public void UndoMove(Position origin, Position destination, Piece? capturedPiece)
  {
    Piece piece = Board.RemovePiece(destination)!;
    piece.DecreaseMovements();
    if (capturedPiece != null)
    {
      Board.PlacePiece(capturedPiece, destination);
      CapturedPieces.Remove(capturedPiece);
      InGamePieces.Add(capturedPiece);
    }
    Board.PlacePiece(piece, origin);
  }

  public void MakePlay(Position origin, Position destination)
  {
    Piece? capturedPiece = MakeMove(origin, destination);
    if (IsInCheck(CurrentPlayer))
    {
      UndoMove(origin, destination, capturedPiece);
      throw new BoardException("You can't put you in CHECK.");
    }
    InCheck = IsInCheck(AdversaryColor(CurrentPlayer));
    if (IsCheckMate(AdversaryColor(CurrentPlayer)))
    {
      Finished = true;
      return;
    }
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
    if (!pieceInPosition.IsPossibleMovement(destination)) throw new BoardException("You can't move this piece to this position.");
  }

  public HashSet<Piece> CapturedPiecesByColor(Color color)
  {
    HashSet<Piece> filteredCapturedPieces = new();
    foreach (Piece piece in CapturedPieces)
    {
      if (piece.Color == color) filteredCapturedPieces.Add(piece);
    }
    return filteredCapturedPieces;
  }

  public HashSet<Piece> InGamePiecesByColor(Color color)
  {
    HashSet<Piece> filteredInGamePiecesPieces = new();
    foreach (Piece piece in InGamePieces)
    {
      if (piece.Color == color) filteredInGamePiecesPieces.Add(piece);
    }
    return filteredInGamePiecesPieces;
  }

  public bool IsInCheck(Color color)
  {
    Piece king = FindKing(color);
    foreach (Piece piece in InGamePiecesByColor(AdversaryColor(color)))
    {
      bool[,] possibleMovements = piece.GetPossibleMovements();
      if (possibleMovements[king.Position!.Line, king.Position.Column])
      {
        return true;
      }
    }
    return false;
  }

  public bool IsCheckMate(Color color)
  {
    if (!IsInCheck(color))
    {
      return false;
    }
    foreach (Piece piece in InGamePiecesByColor(color))
    {
      bool[,] possibleMoviments = piece.GetPossibleMovements();

      for (int l = 0; l < Board.Lines; l++)
      {
        for (int c = 0; c < Board.Columns; c++)
        {
          if (possibleMoviments[l, c])
          {
            Position origin = new(piece.Position!.Line, piece.Position.Column);
            Position destination = new(l, c);
            Piece? capturedPiece = MakeMove(origin, destination);
            bool stillInCheck = IsInCheck(color);
            UndoMove(origin, destination, capturedPiece);
            if (!stillInCheck) return false;
          }
        }
      }
    }
    return true;
  }

  private static Color AdversaryColor(Color color)
  {
    return Color.White == color ? Color.Black : Color.White;
  }

  private Piece FindKing(Color color)
  {
    foreach (Piece piece in InGamePiecesByColor(color))
    {
      if (piece is King)
      {
        return piece;
      }
    }
    throw new Exception("No king piece has found (????)");
  }

  private void PlaceNewPiece(Piece piece, ChessPosition chessPosition)
  {
    Board.PlacePiece(piece, chessPosition.ToPosition());
    InGamePieces.Add(piece);
  }

  private void PlaceAllPieces()
  {
    // PlaceNewPiece(new Rook(Board, Color.White), new ChessPosition('c', 1));
    // PlaceNewPiece(new Rook(Board, Color.White), new ChessPosition('c', 2));
    // PlaceNewPiece(new Rook(Board, Color.White), new ChessPosition('d', 2));
    // PlaceNewPiece(new Rook(Board, Color.White), new ChessPosition('e', 2));
    // PlaceNewPiece(new Rook(Board, Color.White), new ChessPosition('e', 1));
    // PlaceNewPiece(new King(Board, Color.White), new ChessPosition('d', 1));
    // PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8));
    // PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7));
    // PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7));
    // PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7));
    // PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8));
    // PlaceNewPiece(new King(Board, Color.Black), new ChessPosition('d', 8));

    PlaceNewPiece(new King(Board, Color.White), new ChessPosition('a', 8));
    PlaceNewPiece(new Rook(Board, Color.White), new ChessPosition('b', 8));
    PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('h', 7));
    PlaceNewPiece(new Rook(Board, Color.Black), new ChessPosition('c', 1));
    PlaceNewPiece(new King(Board, Color.Black), new ChessPosition('d', 1));
  }
}
