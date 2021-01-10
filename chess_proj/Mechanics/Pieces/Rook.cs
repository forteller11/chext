using System.Collections.Generic;
using chext.Math;

#nullable enable
namespace chext.Mechanics.Pieces
{
    public class Rook : Piece
    {
        
        public Rook(bool isWhite) : base(isWhite, 'r') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece?[][] cells, List<Move> moves)
        {
            SearchStraight(cells, moves, piecePosition,new Int2(0,1));
            SearchStraight(cells, moves, piecePosition,new Int2(0,-1));
            SearchStraight(cells, moves, piecePosition,new Int2(1,0));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,0));
        }
    }
}