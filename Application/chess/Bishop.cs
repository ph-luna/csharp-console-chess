using board;

namespace chess;

class Bishop : Piece
{
  public Bishop(Board board, Color color) : base(board, color)
  {
    for (int i = 1; i < 8; i += 2)
    {
      AllowDirection((Directions)i);
    }
  }

  public override string ToString()
  {
    return "♗";
  }

  public override bool[,] GetPossibleMovements()
  {
    bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];

    foreach (int[] direction in PossibleDirections)
    {
      Position projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
      while (CanMove(projectedPosition))
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
        if (Board.GetPieceInPosition(projectedPosition) is not null && Board.GetPieceInPosition(projectedPosition)!.Color != Color) break;
        projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
      }
    }

    return possibleMovements;
  }
}