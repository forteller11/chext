using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite, 'q') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece[][] cells, List<Move> moves)
        {
            SearchStraight(cells, moves, piecePosition,new Int2(0,1));
            SearchStraight(cells, moves, piecePosition,new Int2(0,-1));
            SearchStraight(cells, moves, piecePosition,new Int2(1,0));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,0));
            
            SearchStraight(cells, moves, piecePosition,new Int2(1,1));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,-1));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,1));
            SearchStraight(cells, moves, piecePosition,new Int2(1,-1));
        }
    }
}