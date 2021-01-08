using System.Collections.Generic;
using chess_proj.Math;

#nullable enable

namespace chess_proj.Systems.Pieces
{
    public abstract class Piece
    {
        public Int2 Position { get; protected set; }
        public readonly Player Owner;

        public Piece(Int2 position, Player owner)
        {
            Position = position;
            Owner = owner;
        }
        public abstract void GetMoves(in List<Int2> moves);
    }
}