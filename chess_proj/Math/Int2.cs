﻿
 namespace chess_proj.Math
{
    public struct Int2
    {
        public int X;
        public int Y;

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Int2 a, Int2 b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Int2 a, Int2 b) => a.X != b.X || a.Y != b.Y;
        public override string ToString() => $"Int2 X: {X}, Y: {Y};";
        
    }
}