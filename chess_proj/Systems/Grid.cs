using System.Collections.Generic;
using chess_proj.Math;
using chess_proj.Mechanics.Pieces;
using Discord;

#nullable enable
namespace chess_proj.Mechanics
{
    public class Board
    {
        public readonly Piece?[][] Cells;
        private readonly List<Int2> ValidMoves;
        public Player White = new Player();
        public Player Black = new Player();
        
        public Board(int cellCount)
        {
            White = new Player();
            Black = new Player();
            
            Cells = new Piece[cellCount][];
            for (int i = 0; i < Cells.Length; i++)
                Cells[i] = new Piece[cellCount];
            
        }

        public void MovePiece(Player actor, Int2 from, Int2 target)
        {
            var piece = GetCell(from);

            if (piece == null)
            {
                Program.DebugLog($"no piece at {from}");
                return;
            }
            
            //make sure piece is owned by actor
            if (piece.Owner != actor)
            {
                Program.DebugLog($"{piece.Name} not owned by actor");
                return;
            }

            //is target in list of valid moves
            ValidMoves.Clear();
            piece.RefreshValidMoves(in ValidMoves);
            if (!ValidMoves.Contains(target))
            {
                Program.DebugLog($"Cannot move {piece.Name} to {target}");
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