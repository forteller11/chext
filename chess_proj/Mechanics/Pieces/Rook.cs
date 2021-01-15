using System.Collections.Generic;
using chext.Math;

#nullable enable
namespace chext.Mechanics.Pieces
{
    public class Rook : Piece
    {
        
        public Rook(bool isWhite) : base(isWhite, 'r') { }

        public override void RefreshValidMoves(Board board, Int2 pos, List<Move> moves)
        {
            SearchStraight(board, moves, pos,new Int2(0,1));
            SearchStraight(board, moves, pos,new Int2(0,-1));
            SearchStraight(board, moves, pos,new Int2(1,0));
            SearchStraight(board, moves, pos,new Int2(-1,0));
        }
    }
}