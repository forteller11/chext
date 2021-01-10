﻿using System.Collections.Generic;
using chess_proj.Math;

namespace chess_proj.Mechanics.Pieces
{
    public class King : Piece
    {
        public King(Player owner) : base(owner, 'k') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece[][] cells, List<Move> moves)
        {
            AddMoveIfValid(cells, moves, piecePosition + new Int2(0, 1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(1, 0), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(0, -1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-1, 0), piecePosition);
            
            AddMoveIfValid(cells, moves, piecePosition + new Int2(1, 1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-1, -1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(1, -1), piecePosition);
            AddMoveIfValid(cells, moves, piecePosition + new Int2(-1, 1), piecePosition);
            
   
        }
    }
}