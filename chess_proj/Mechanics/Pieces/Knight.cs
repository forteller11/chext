using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite, 'n') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece[][] cells, List<Move> moves)
        {
            //todo add all directions
            AddMoveIfValid(cells, moves, piecePosition + new Int2(1, 2), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-1, 2), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(1, -2), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-1, -2), piecePosition);
            
            AddMoveIfValid(cells, moves, piecePosition + new Int2(2, 1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-2, 1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(2, -1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-2, -1), piecePosition);
        }
    }
}