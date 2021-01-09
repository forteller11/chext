using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class King : Piece
    {
        public King(Player owner) : base(owner, 'k') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece[][] cells, List<Move> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}