namespace board;

abstract class Piece
{
  public Position? Position { get; set; }
  public Color Color { get; protected set; }
  public int Moviments { get; protected set; }
  public Board Board { get; protected set; }
  public List<int[]> PossibleDirections { get; protected set; }

  public Piece(Board board, Color color)
  {
    Position = null;
    Board = board;
    Color = color;
    Moviments = 0;
    PossibleDirections = new List<int[]>();
  }

  public void IncreaseMoviments()
  {
    Moviments++;
  }

  public abstract bool[,] GetPossibleMoviments();

  protected bool CanMove(Position position)
  {
    Piece? piece = Board.GetPieceInPosition(position);
    return piece is null || piece.Color != Color;
  }

  protected void AllowDirection(Directions direction)
  {
    switch (direction)
    {
      case Directions.NORTH:
        PossibleDirections.Add(new int[] { -1, 0 });
        break;
      case Directions.NORTHEAST:
        PossibleDirections.Add(new int[] { -1, 1 });
        break;
      case Directions.EAST:
        PossibleDirections.Add(new int[] { 0, 1 });
        break;
      case Directions.SOUTHEAST:
        PossibleDirections.Add(new int[] { 1, 1 });
        break;
      case Directions.SOUTH:
        PossibleDirections.Add(new int[] { 1, 0 });
        break;
      case Directions.SOUTHWEST:
        PossibleDirections.Add(new int[] { 1, -1 });
        break;
      case Directions.WEST:
        PossibleDirections.Add(new int[] { 0, -1 });
        break;
      case Directions.NORTHWEST:
        PossibleDirections.Add(new int[] { -1, -1 });
        break;
    }
  }
}