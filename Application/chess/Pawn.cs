using board;

namespace chess;
class Pawn : Piece
{
  private ChessMatch _chessMatch;
  public Pawn(ChessMatch chessMatch, Color color) : base(chessMatch.Board, color)
  {
    _chessMatch = chessMatch;
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
      if (Board.IsPositionValid(projectedPosition) && HasEnemyPiece(projectedPosition))
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
      }

      projectedPosition = new(Position!.Line, Position.Column);
      projectedPosition.Change(projectedPosition.Line + direction[0], projectedPosition.Column + direction[1] - 1);
      if (Board.IsPositionValid(projectedPosition) && HasEnemyPiece(projectedPosition))
      {
        possibleMovements[projectedPosition.Line, projectedPosition.Column] = true;
      }
    }

    return EnPassant(possibleMovements);
  }

  private bool HasEnemyPiece(Position position)
  {
    Piece? piece = Board.GetPieceInPosition(position);
    if (piece is null) return false;
    return piece.Color != Color;
  }

  private bool[,] EnPassant(bool[,] possibleMovements)
  {
    int enPassantLine = Color == Color.White ? 3 : 4;
    if (Position!.Line == enPassantLine)
    {
      int line = Color == Color.White ? -1 : 1;
      Position leftSide = new(Position.Line, Position.Column - 1);
      if (HasEnemyPiece(leftSide) && _chessMatch.VunerableToEnPassant == Board.GetPieceInPosition(leftSide))
      {
        possibleMovements[leftSide.Line + line, leftSide.Column] = true;
      }
      Position rightSide = new(Position.Line, Position.Column + 1);
      if (HasEnemyPiece(rightSide) && _chessMatch.VunerableToEnPassant == Board.GetPieceInPosition(rightSide))
      {
        possibleMovements[rightSide.Line + line, rightSide.Column] = true;
      }
    }

    return possibleMovements;
  }
}