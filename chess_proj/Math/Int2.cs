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

        public override string ToString() => $"Int2 X: {X}, Y: {Y};";
        
    }
}