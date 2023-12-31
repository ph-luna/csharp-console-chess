namespace board;

class Board
{
  public int Lines { get; set; }
  public int Columns { get; set; }
  private Piece?[,] _pieces;

  public Board(int lines, int columns)
  {
    Lines = lines;
    Columns = columns;
    _pieces = new Piece[lines, columns];
  }

  public Piece? GetPieceInPosition(int line, int column)
  {
    return _pieces[line, column];
  }

  public Piece? GetPieceInPosition(Position position)
  {
    return _pieces[position.Line, position.Column];
  }

  public void PlacePiece(Piece piece, Position position)
  {
    if (HasPiece(position))
    {
      throw new BoardException("This position already has a piece");
    }
    piece.Position = position;
    _pieces[position.Line, position.Column] = piece;
  }

  public Piece? RemovePiece(Position position)
  {
    Piece? piece = GetPieceInPosition(position);
    if (piece == null) return null;
    piece.Position = null;
    _pieces[position.Line, position.Column] = null;
    return piece;
  }

  public bool IsPositionValid(Position position)
  {
    return !(position.Line < 0
    || position.Line >= Lines
    || position.Column < 0
    || position.Column >= Columns);
  }

  public bool HasPiece(Position position)
  {
    if (!IsPositionValid(position)) throw new BoardException("Invalid Position");
    return GetPieceInPosition(position) is not null;
  }
}