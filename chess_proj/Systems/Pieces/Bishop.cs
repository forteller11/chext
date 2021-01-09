using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player owner) : base(owner, 'b') { }

        public override void RefreshValidMoves(Piece[][] cells, List<Move> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}