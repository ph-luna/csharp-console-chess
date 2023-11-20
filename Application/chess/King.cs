using board;

namespace chess;
class King : Piece
{
  public King(Board board, Color color) : base(board, color)
  {
    for (Directions i = 0; (int)i < 8; i++)
    {
      AllowDirection(i);
    }
  }

  public override string ToString()
  {
    return "♔";
  }

  public override bool[,] GetPossibleMovements()
  {
    bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];

    foreach (int[] direction in PossibleDirections)
    {
      Position projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
      if (CanMove(projectedPosition))
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
      }
    }

    return possibleMovements;
  }
}