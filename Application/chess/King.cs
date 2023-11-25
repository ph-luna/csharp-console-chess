using board;

namespace chess;
class King : Piece
{
  private ChessMatch _chessMatch;

  public King(ChessMatch chessMatch, Color color) : base(chessMatch.Board, color)
  {
    _chessMatch = chessMatch;
    for (int i = 0; i < 8; i++)
    {
      AllowDirection((Directions)i);
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
    return Castling(possibleMovements);
  }

  private bool[,] Castling(bool[,] possibleMoviments)
  {
    if (Movements == 0 && !_chessMatch.InCheck)
    {
      Position leftRookPosition = new(Position!.Line, Position.Column + 3);
      Position rightRookPosition = new(Position!.Line, Position.Column - 4);
      if (IsRookAvaibleForCastling(leftRookPosition))
      {
        bool hasPiece = false;
        for (int i = 1; i <= 2; i++)
        {
          if (Board.GetPieceInPosition(Position.Line, Position.Column + i) is not null)
          {
            hasPiece = true;
            break;
          }
        }
        if (!hasPiece)
        {
          possibleMoviments[Position.Line, Position.Column + 2] = true;
        }
      }
      if (IsRookAvaibleForCastling(rightRookPosition))
      {
        bool hasPiece = false;
        for (int i = -1; i >= -3; i--)
        {
          if (Board.GetPieceInPosition(Position.Line, Position.Column + i) is not null)
          {
            hasPiece = true;
            break;
          }
        }
        if (!hasPiece)
        {
          possibleMoviments[Position.Line, Position.Column - 2] = true;
        }
      }
    }
    return possibleMoviments;
  }

  private bool IsRookAvaibleForCastling(Position rookPosition)
  {
    if (!Board.IsPositionValid(rookPosition)) return false;
    Piece? rook = Board.GetPieceInPosition(rookPosition.Line, rookPosition.Column);
    return rook is not null && rook.Color == Color && rook.Movements == 0;
  }
}