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

        public Piece(Player owner, char type)
        {
            Owner = owner;
            Type = type.ToString();
        }
        public abstract void RefreshValidMoves(in List<Int2> moves);
    }
}