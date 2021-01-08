using System.Collections.Generic;
using chess_proj.Math;

#nullable enable

namespace chess_proj.Mechanics.Pieces
{
    public abstract class Piece
    {
        public readonly Player Owner;
        public readonly string Name; //wasting mem

        public Piece(Player owner)
        {
            Owner = owner;
        }
        public abstract void RefreshValidMoves(in List<Int2> moves);
    }
}