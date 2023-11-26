# Console Chess
A project created for fun and practice.

This project is a chess game running in a console/terminal. Each turn, a player (starting with white) selects a piece by inputting its board coordinates position **(e.g., a1)**, and a green highlight will indicate where this piece can be moved. The player can move to any green square displayed by inputting the corresponding board coordinates.

If there is an enemy piece at the new position, this piece will be removed, just like in a real chess game.

All special moves are implemented:
- Pawn Promotion
- En Passant
- Castling

The special move will be indicated by a green square when available.

After each turn, the game checks if a player is in check or checkmate.

If a player is in check, the game will not allow any move that does not remove the check. The game also prevents a player from putting themselves in check.

If the game detects a checkmate, the match will conclude, and the winning player will be displayed.

## How to run
To run this project, you will need:
- .NET v7;
- A terminal with support for Unicode and RGB colors.