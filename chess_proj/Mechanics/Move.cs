using System;
using chext.Math;

namespace chext.Mechanics
{
    public struct Move : IEquatable<Move>
    {
        public Int2 Pos;
        public Int2 From;
        public bool ValidLanding;

        public Move(Int2 pos, Int2 from, bool validLanding=true)
        {
            Pos = pos;
            From = from;
            ValidLanding = validLanding;
        }


        public bool Equals(Move other)
        {
            return Pos.Equals(other.Pos) && From.Equals(other.From) && ValidLanding == other.ValidLanding;
        }

        public override bool Equals(object obj)
        {
            return obj is Move other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pos, From, ValidLanding);
        }
    }
}