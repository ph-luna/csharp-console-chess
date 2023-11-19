namespace board;

class Piece
{
  public Position Position { get; set; }
  public Color Color { get; protected set; }
  public int Moviments { get; protected set; }
  public Board Board { get; protected set; }

  public Piece(Position position, Board board, Color color)
  {
    Position = position;
    Board = board;
    Color = color;
    Moviments = 0;
  }
}