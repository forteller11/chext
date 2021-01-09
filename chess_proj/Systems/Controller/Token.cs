using System;
using System.Collections.Generic;

namespace chess_proj.Mechanics.Controller
{
    public struct Token
    {
        public Memory<string> Value;
        public enum Type
        {
            Coordinate,
            Undo,
            ChexKeyword,
        }
    }
}