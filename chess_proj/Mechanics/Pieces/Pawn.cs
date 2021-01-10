using System.Collections.Generic;
using chext.Math;

namespace chext.Mechanics.Pieces
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