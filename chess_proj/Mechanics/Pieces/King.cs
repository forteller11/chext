using System.Collections.Generic;
using chext.Math;

namespace chext.Mechanics.Pieces
{
    public class King : Piece
    {
        public King(bool isWhite) : base(isWhite, 'k') { }

        public override void RefreshValidMoves(Board board, Int2 pos, List<Move> moves)
        {
            AddMoveIfValid(board, moves, pos + new Int2(0, 1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(1, 0), pos);
            AddMoveIfValid(board, moves, pos + new Int2(0, -1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-1, 0), pos);
            
            AddMoveIfValid(board, moves, pos + new Int2(1, 1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-1, -1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(1, -1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-1, 1), pos);
            
   
        }
    }
}