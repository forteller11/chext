using System;
using chess_proj.Math;

namespace chess_proj.Mechanics.Controller
{
    public class Parser
    {
        public event Action<Int2, Int2> MoveAttempt;
        public event Action<Int2> DisplayMove;
        
        
    }
}