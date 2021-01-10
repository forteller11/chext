using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(bool isWhite) : base(isWhite, 'p') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece[][] cells, List<Move> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}