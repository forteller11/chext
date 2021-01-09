using System.Collections.Generic;
using chess_proj.Mechanics.Pieces;

namespace chess_proj.Mechanics
{
    public class Player
    {
        public List<Piece> Captured = new List<Piece>();
        public bool IsWhite;

        public Player(bool isWhite)
        {
            IsWhite = isWhite;
        }
    }
}