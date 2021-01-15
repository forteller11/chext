using System;
using System.Collections.Generic;
using chext.Math;

#nullable enable

namespace chext.Mechanics.Pieces
{
    public abstract class Piece
    {
        public string Name => IsWhite ? $" {Type.ToUpper()} " : $"`{Type.ToLower()} ";  //wasting mem
        public readonly string Type; //wasting mem
        public readonly bool IsWhite;
        
        //public string EmoteName => "\\:__" + Name + "__:";
       // public string EmoteName => "<:__rook__:797330289474142239>";

       public Piece(bool isWhite, char type)
        {
            IsWhite = isWhite;
            Type = type.ToString();
        }
        public abstract void RefreshValidMoves(Board board, Int2 pos, List<Move> moves);
        public virtual void OnPostMove(){}

        protected void SearchStraight(Board board, List<Move> moves, Int2 initalPosition, Int2 increment)
        {
            Int2 index = initalPosition;
            while (true)
            {
                Program.DebugLog(index);
                Program.DebugLog(increment);
                index += increment;
                if (!AddMoveIfValid(board, moves, index, index - increment))
                    break;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// has hit piece or is out of bounds == false, if hit empty square == true
        /// </returns>
        protected bool AddMoveIfValid(Board board, List<Move> moves, Int2 target, Int2 initialPosition)
        {
            if (!board.IsWithinBounds(target))
                return false;

            var cell = board.GetCell(target);
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