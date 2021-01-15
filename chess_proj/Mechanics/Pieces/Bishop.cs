using System.Collections.Generic;
using chext.Math;

namespace chext.Mechanics.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite, 'b') { }

        public override void RefreshValidMoves(Board board, Int2 pos, List<Move> moves)
        {
            SearchStraight(board, moves, pos,new Int2(1,1));
            SearchStraight(board, moves, pos,new Int2(-1,-1));
            SearchStraight(board, moves, pos,new Int2(-1,1));
            SearchStraight(board, moves, pos,new Int2(1,-1));
        }
    }
}