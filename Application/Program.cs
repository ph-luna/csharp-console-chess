﻿using board;
using chess;

try
{
  ChessMatch chessMatch = new();

  while (!chessMatch.Finished)
  {
    try
    {
      Console.Clear();
      Screen.PrintBoard(chessMatch.Board);
      Console.WriteLine($"\nTurn: {chessMatch.Turn}");
      Console.WriteLine($"Current Player: {chessMatch.CurrentPlayer}");

      Console.Write("\nPiece at Position: ");
      Position origin = Screen.ReadChessPosition().ToPosition();
      chessMatch.ValidateOrigin(origin);

      bool[,] possibleMovements = chessMatch.Board.GetPieceInPosition(origin)!.GetPossibleMovements();
      Console.Clear();
      Screen.PrintBoard(chessMatch.Board, possibleMovements);

      Console.Write("\nDestination: ");
      Position destination = Screen.ReadChessPosition().ToPosition();
      chessMatch.ValidateDestination(origin, destination);

      chessMatch.MakePlay(origin, destination);
    }
    catch (BoardException ex)
    {
      Screen.PrintMessage(ex.Message);
    }
  }
}
catch (Exception ex)
{
  Console.WriteLine(ex);
}
