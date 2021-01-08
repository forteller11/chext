using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Systems.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player owner) : base(owner)
        {
        }

        public override void RefreshValidMoves(in List<Int2> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}