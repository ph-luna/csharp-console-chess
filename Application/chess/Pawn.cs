using board;

namespace chess;
class Pawn : Piece
{
  public Pawn(Board board, Color color) : base(board, color)
  {
    if (Color == Color.White)
    {
      AllowDirection(Directions.NORTH);
      return;
    }
    AllowDirection(Directions.SOUTH);
  }
  public override string ToString()
  {
    return "♙";
  }

  public override bool[,] GetPossibleMovements()
  {
    bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];

    foreach (int[] direction in PossibleDirections)
    {
      Position projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
      if (CanMove(projectedPosition) && Board.GetPieceInPosition(projectedPosition) is null)
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
        projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
        if (CanMove(projectedPosition) && Board.GetPieceInPosition(projectedPosition) is null && Movements == 0)
        {
          possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
        }
      }

      projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1] + 1);
      if (Board.IsPositionValid(projectedPosition) && Board.GetPieceInPosition(projectedPosition) is not null)
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
      }

      projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1] - 1);
      if (Board.IsPositionValid(projectedPosition) && Board.GetPieceInPosition(projectedPosition) is not null)
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
      }
    }

    return possibleMovements;
  }
}