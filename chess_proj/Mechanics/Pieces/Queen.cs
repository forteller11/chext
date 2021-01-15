using System.Collections.Generic;
using chext.Math;

namespace chext.Mechanics.Pieces
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite, 'q') { }

        public override void RefreshValidMoves(Board board, Int2 pos, List<Move> moves)
        {
            SearchStraight(board, moves, pos,new Int2(0,1));
            SearchStraight(board, moves, pos,new Int2(0,-1));
            SearchStraight(board, moves, pos,new Int2(1,0));
            SearchStraight(board, moves, pos,new Int2(-1,0));
            
            SearchStraight(board, moves, pos,new Int2(1,1));
            SearchStraight(board, moves, pos,new Int2(-1,-1));
            SearchStraight(board, moves, pos,new Int2(-1,1));
            SearchStraight(board, moves, pos,new Int2(1,-1));
        }
    }
}