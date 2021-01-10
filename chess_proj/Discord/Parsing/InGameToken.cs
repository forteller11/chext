using System;
using System.Collections.Generic;

namespace chext.Discord.Parsing
{
    public struct InGameToken
    {
        public string Value;
        public TokenType Type;
        public enum TokenType
        {
            //during game
            CellCoord,
            DisplayCellMoves,
            Undo,
        }

        public InGameToken(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
    }
}