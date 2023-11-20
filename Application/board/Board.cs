namespace board;

class Board
{
  public int Lines { get; set; }
  public int Columns { get; set; }
  private Piece[,] _pieces;

  public Board(int lines, int columns)
  {
    Lines = lines;
    Columns = columns;
    _pieces = new Piece[lines, columns];
  }

  public Piece GetPieceInPosition(int line, int column)
  {
    return _pieces[line, column];
  }

  public Piece GetPieceInPosition(Position position)
  {
    return _pieces[position.Line, position.Column];
  }

  public void PlacePiece(Piece piece, Position position)
  {
    if (HasPiece(position))
    {
      throw new BoardException("This position already has a piece");
    }
    _pieces[position.Line, position.Column] = piece;
  }

  public void ValidatePosition(Position position)
  {
    if (position.Line < 0
    || position.Line >= Lines
    || position.Column < 0
    || position.Column >= Columns)
    {
      throw new BoardException("Invalid Position");
    }
  }

  public bool HasPiece(Position position)
  {
    ValidatePosition(position);
    return GetPieceInPosition(position) is not null;
  }
}