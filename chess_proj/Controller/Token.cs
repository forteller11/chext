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
            //during game
            CellCoord,
            DisplayCellMoves,
            Undo,
            
            //pregame
            Join,
            CreateGame,
            ChexKeyword
            //todo, token vs phrase/grammer?,
        }

        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
    }
}