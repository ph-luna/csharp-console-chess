using board;

namespace chess;

class Rook : Piece
{
  public Rook(Board board, Color color) : base(board, color)
  {
    for (Directions i = 0; (int)i < 8; i += 2)
    {
      AllowDirection(i);
    }
  }

  public override string ToString()
  {
    return "♖";
  }

  public override bool[,] GetPossibleMoviments()
  {
    bool[,] possibleMoviments = new bool[Board.Lines, Board.Columns];

    foreach (int[] direction in PossibleDirections)
    {
      Position projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
      while (Board.IsPositionValid(projectedPosition) && CanMove(projectedPosition))
      {
        possibleMoviments[projectedPosition.Line, projectedPosition.Column] = true;
        if (Board.GetPieceInPosition(projectedPosition) is not null && Board.GetPieceInPosition(projectedPosition)!.Color != Color) break;
        projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1]);
      }
    }

    return possibleMoviments;
  }
}