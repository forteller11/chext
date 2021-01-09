using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player owner) : base(owner, 'n') { }

        public override void RefreshValidMoves(Piece[][] cells, List<Move> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}