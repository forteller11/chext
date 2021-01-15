using System.Collections.Generic;
using chext.Math;

namespace chext.Mechanics.Pieces
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite, 'n') { }

        public override void RefreshValidMoves(Board board, Int2 pos, List<Move> moves)
        {
            //todo add all directions
            AddMoveIfValid(board, moves, pos + new Int2(1, 2), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-1, 2), pos);
            AddMoveIfValid(board, moves, pos + new Int2(1, -2), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-1, -2), pos);
            
            AddMoveIfValid(board, moves, pos + new Int2(2, 1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-2, 1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(2, -1), pos);
            AddMoveIfValid(board, moves, pos + new Int2(-2, -1), pos);
        }
    }
}