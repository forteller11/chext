using System;
using System.Collections.Generic;
using chess_proj.Math;

#nullable enable

namespace chess_proj.Mechanics.Pieces
{
    public abstract class Piece
    {
        public string Name => IsWhite ? $" {Type.ToUpper()} " : $"`{Type.ToLower()} ";  //wasting mem
        public readonly string Type; //wasting mem
        public readonly bool IsWhite;
        
        //public string EmoteName => "\\:__" + Name + "__:";
       // public string EmoteName => "<:__rook__:797330289474142239>";

       //todo piece shouldn't know owner
       public Piece(bool isWhite, char type)
        {
            IsWhite = isWhite;
            Type = type.ToString();
        }
        public abstract void RefreshValidMoves(Int2 piecePosition, Piece?[][] cells, List<Move> moves);

        protected bool IsWithinBounds(Int2 position, Piece?[][] cells) => IsWithinBounds(position.X, position.Y, cells);
        protected bool IsWithinBounds(int x, int y, Piece?[][] cells)
        {
            if (x >= cells.Length) return false;
            if (x <  0)            return false;
            if (y >= cells[x].Length) return false;
            if (y < 0)                return false;
            return true;
        }
        
        protected void SearchStraight(Piece?[][] cells, List<Move> moves, Int2 initalPosition, Int2 increment)
        {
            Int2 index = initalPosition;
            while (true)
            {
                index += increment;
                if (!AddMoveIfValid(cells, moves, index, index - increment))
                    break;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// has hit piece or is out of bounds == false, if hit empty square == true
        /// </returns>
        protected bool AddMoveIfValid(Piece?[][] cells, List<Move> moves, Int2 target, Int2 initialPosition)
        {
            if (!IsWithinBounds(target, cells))
                return false;

            var cell = cells[target.X][target.Y];
            if (cell != null)
            {
                if (IsWhite != cell.IsWhite) //if piece on other side
                    moves.Add(new Move(target, initialPosition));
                return false;
            }

            moves.Add(new Move(target, initialPosition));
            return true;
        }
        
        
    }
}