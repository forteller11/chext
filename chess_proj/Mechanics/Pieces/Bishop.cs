using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite, 'b') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece[][] cells, List<Move> moves)
        {
            SearchStraight(cells, moves, piecePosition,new Int2(1,1));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,-1));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,1));
            SearchStraight(cells, moves, piecePosition,new Int2(1,-1));
        }
    }
}