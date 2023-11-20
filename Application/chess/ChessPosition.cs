using board;

namespace chess;

class ChessPosition
{
  public char Column { get; set; }
  public int Line { get; set; }

  public ChessPosition(char column, int line)
  {
    Line = line;
    Column = column;
  }

  public Position ToPosition()
  {
    return new Position(8 - Line, Column - 'a');
  }

  public override string ToString()
  {
    return $"{Column}{Line}";
  }
}