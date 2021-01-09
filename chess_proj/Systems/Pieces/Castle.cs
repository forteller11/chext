using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Castle : Piece
    {
        
        public Castle(Player owner) : base(owner, 'c') { }

        public override void RefreshValidMoves(Piece[][] cells, List<Move> moves)
        {

            //for (int y = 0; ; y++)
            {
                //if (cells[i])
            }
        }
    }
}