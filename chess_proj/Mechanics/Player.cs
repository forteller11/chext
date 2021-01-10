using System.Collections.Generic;
using chess_proj.Mechanics.Pieces;

namespace chess_proj.Mechanics
{
    public class Player
    {
        public List<Piece> Captured = new List<Piece>();
        public readonly bool IsWhite;
        public readonly ulong Id;

        public Player(ulong id, bool isWhite)
        {
            Id = id;
            IsWhite = isWhite;
        }
    }
}