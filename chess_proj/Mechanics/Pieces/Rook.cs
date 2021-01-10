using System.Collections.Generic;
using chess_proj.Math;

#nullable enable
namespace chess_proj.Mechanics.Pieces
{
    public class Rook : Piece
    {
        
        public Rook(Player owner) : base(owner, 'r') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece?[][] cells, List<Move> moves)
        {
            SearchStraight(cells, moves, piecePosition,new Int2(0,1));
            SearchStraight(cells, moves, piecePosition,new Int2(0,-1));
            SearchStraight(cells, moves, piecePosition,new Int2(1,0));
            SearchStraight(cells, moves, piecePosition,new Int2(-1,0));
        }
    }
}