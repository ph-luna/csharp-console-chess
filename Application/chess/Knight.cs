using board;

namespace chess;
class Knight : Piece
{
  public Knight(Board board, Color color) : base(board, color)
  {
    for (int i = 0; i < 8; i += 2)
    {
      AllowDirection((Directions)i);
    }
  }

  public override string ToString()
  {
    return "♘";
  }

  public override bool[,] GetPossibleMovements()
  {
    bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];

    foreach (int[] direction in PossibleDirections)
    {
      Position projectedPosition = new(Position!.Line, Position.Column);

      projectedPosition.Change(projectedPosition.Line + direction[0] * 2, projectedPosition.Column + direction[1] * 2);

      if (CanMove(projectedPosition))
      {
        if (direction[0] == 0)
        {
          projectedPosition.Line++;
          possibleMovements[projectedPosition.Line - 2, projectedPosition.Column] = true;
        }
        if (direction[1] == 0)
        {
          projectedPosition.Column++;
          possibleMovements[projectedPosition.Line, projectedPosition.Column - 2] = true;
        }
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
      }
    }

    return possibleMovements;
  }
}