using System;
using System.Collections.Generic;

namespace chess_proj.Controller
{
    public struct Token
    {
        public string Value;
        public TokenType Type;
        public enum TokenType
        {
            CellCoord,
            Undo,
            ChexKeyword,
        }

        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
    }
}