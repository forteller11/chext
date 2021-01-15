using System;
using System.Collections.Generic;
using chext.Math;
using chext.Mechanics.Pieces;

#nullable enable
namespace chext.Mechanics
{
    public class Board
    {
        public readonly int Dimensions = 8;
        public readonly Piece?[][] Cells;
        private readonly List<Move> _validMoves;
        public bool IsWhitesTurn = true;

        public List<Piece> CapturedWhite = new List<Piece>();
        public List<Piece> CapturedBlack = new List<Piece>();


        public Board()
        {
            _validMoves = new List<Move>();
            Cells = new Piece[Dimensions][];
            for (int i = 0; i < Cells.Length; i++)
                Cells[i] = new Piece[Dimensions];
            
        }

        public List<Move> GetMoves(Int2 position)
        {
            _validMoves.Clear();

            Program.DebugLog(position);
            var piece = GetCell(position);
            if (piece == null)
                return _validMoves;
            
            piece.RefreshValidMoves(position, Cells, _validMoves);
            return _validMoves;
        }
        
        public void MovePiece(Player actor, Int2 from, Int2 target)
        {
  
            if (actor.IsWhite != IsWhitesTurn)
            {
                Program.WarningLog($"Is not {actor.Username}'s turn to move!");
                return;
            }

            var piece = GetCell(from);
            if (piece == null)
            {
                Program.WarningLog($"no piece exists at {Common.IndexToLabelCoordinate(from, Dimensions)}");
                return;
            }
            
            //make sure piece is owned by actor
            if (piece.IsWhite != actor.IsWhite)
            {
                Program.WarningLog($"{piece.Name} not owned by actor");
                return;
            }

            
            //is target in list of valid moves
            _validMoves.Clear();
            piece.RefreshValidMoves(from, Cells, _validMoves);
            bool validMovesContainsTarget = false;
            for (int i = 0; i < _validMoves.Count; i++)
            {
                if (_validMoves[i].Pos == target)
                {
                    validMovesContainsTarget = true;
                    break;
                }
            }
            if (!validMovesContainsTarget)
            {
                Program.WarningLog($"Cannot move {piece.Name} to {target}");
                return;
            }

            //remove any existing pieces
            if (GetCell(target) != null)
            {
                var takenPiece = GetCell(target);
                if (IsWhitesTurn)
                    CapturedBlack.Add(takenPiece!);
                else 
                    CapturedWhite.Add(takenPiece!);
                
                Cells[target.X][target.Y] = null;
            }
            
            //move piece
            Cells[from.X][from.Y] = null;
            Cells[target.X][target.Y] = piece;
            
            //next turn
           IsWhitesTurn = !IsWhitesTurn;
        }

        public Piece? GetCell(Int2 position) => Cells[position.X][position.Y];
        public Piece? GetCell(int x, int y) => Cells[x][y];
        public void SetCell(Int2 position, Piece piece) => Cells[position.X][position.Y] = piece;
        public void SetCell(int x, int y, Piece piece) => Cells[x][y] = piece;
    }
}