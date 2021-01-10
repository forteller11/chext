using System;
using System.Collections.Generic;
using chess_proj.Math;

#nullable enable

namespace chess_proj.Mechanics.Pieces
{
    public abstract class Piece
    {
        public readonly Player Owner;
        public string Name => Owner.IsWhite ? $" {Type.ToUpper()} " : $"`{Type.ToLower()} ";  //wasting mem
        public readonly string Type; //wasting mem
        
        //public string EmoteName => "\\:__" + Name + "__:";
       // public string EmoteName => "<:__rook__:797330289474142239>";

       //todo piece shouldn't know owner
       public Piece(Player owner, char type)
        {
            Owner = owner;
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
    }
}