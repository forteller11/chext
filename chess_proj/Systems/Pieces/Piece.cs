using System.Collections.Generic;
using chess_proj.Math;

#nullable enable

namespace chess_proj.Mechanics.Pieces
{
    public abstract class Piece
    {
        public readonly Player Owner;
        public readonly string Name; //wasting mem
        //public string EmoteName => "\\:__" + Name + "__:";
        public string EmoteName => "<:__rook__:797330289474142239>";

        public Piece(Player owner, string name)
        {
            Owner = owner;
            Name = name;
        }
        public abstract void RefreshValidMoves(in List<Int2> moves);
    }
}