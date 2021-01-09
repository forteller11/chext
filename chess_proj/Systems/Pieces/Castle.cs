using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Castle : Piece
    {
        
        public Castle(Player owner) : base(owner, 'c') { }

        public override void RefreshValidMoves(in List<Int2> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}