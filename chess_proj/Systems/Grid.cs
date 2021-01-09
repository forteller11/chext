using System;
using System.Collections.Generic;
using chess_proj.Math;
using chess_proj.Mechanics.Pieces;

#nullable enable
namespace chess_proj.Mechanics
{
    public class Board
    {
        public readonly int Dimensions;
        public readonly Piece?[][] Cells;
        private readonly List<Int2> ValidMoves;
        public Player White;
        public Player Black;
        
        public Board(int cellCount)
        {
            White = new Player(true);
            Black = new Player(false);

            Dimensions = cellCount;
            Cells = new Piece[Dimensions][];
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Piece[Dimensions];
                for (int j = 0; j < Cells.Length; j++)
                {
                    if (j > Dimensions / 2)
                    {
                        Cells[i][j] = new Rook(White);
                    }
                }
            }


        }

        public void MovePiece(Player actor, Int2 from, Int2 target)
        {
            var piece = GetCell(from);

            if (piece == null)
            {
                Console.WriteLine($"no piece at {from}");
                return;
            }
            
            //make sure piece is owned by actor
            if (piece.Owner != actor)
            {
                Console.WriteLine($"{piece.Name} not owned by actor");
                return;
            }

            //is target in list of valid moves
            ValidMoves.Clear();
            piece.RefreshValidMoves(in ValidMoves);
            if (!ValidMoves.Contains(target))
            {
                Console.WriteLine($"Cannot move {piece.Name} to {target}");
                return;
            }

            //remove any existing pieces
            if (GetCell(target) != null)
            {
                var takenPiece = GetCell(target);
                takenPiece!.Owner.Captured.Add(takenPiece);
                Cells[target.X][target.Y] = null;
            }
            
            //move piece
            Cells[from.X][from.Y] = null;
            Cells[target.X][target.Y] = piece;
        }

        public Piece? GetCell(Int2 position) => Cells[position.X][position.Y];
    }
}