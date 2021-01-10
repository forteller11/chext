using System;
using System.Collections.Generic;
using System.Diagnostics;
using chess_proj.Math;

namespace chess_proj.Controller
{
    public class Parser
    {
        public event Action<Int2, Int2> MoveAttempt;
        public event Action<Int2> DisplayMoves;

        private List<Token> _tokens = new List<Token>(20);

        public void Parse(string input)
        {
            _tokens.Clear();
            string[] tokenStrings = input.Split(" ");
            
            #region create tokens
            for (int i = 0; i < tokenStrings.Length; i++)
            {
                var tokenString = tokenStrings[i];
                if (tokenString.Length == 2)
                {
                    if (ContainsLabeLetter(tokenString[0]) && ContainsLabeNumber(tokenString[1]))
                    {
                        _tokens.Add(new Token(tokenString, Token.TokenType.CellCoord));
                        Console.WriteLine($"Token Created: {tokenString}");
                        continue;
                    }
                    
                }
            }
            #endregion
            
            #region trigger events depending on grammer

            if (_tokens.Count == 1)
            {
                if (_tokens[0].Type == Token.TokenType.CellCoord)
                {
                    Console.WriteLine("displaymoves");
                    DisplayMoves?.Invoke(Int2.FromChessCoordinate(_tokens[0].Value));
                }
            }

            if (_tokens.Count == 2)
            {
                if (_tokens[0].Type == Token.TokenType.CellCoord && _tokens[1].Type == Token.TokenType.CellCoord)
                {
                    MoveAttempt?.Invoke(Int2.FromChessCoordinate(_tokens[0].Value), Int2.FromChessCoordinate(_tokens[1].Value));
                }
            }
            #endregion
        }

        private bool ContainsLabeLetter(char x)
        {
            x = Char.ToLower(x);
            if (x == 'a') return true;
            if (x == 'b') return true;
            if (x == 'c') return true;
            if (x == 'd') return true;
            if (x == 'e') return true;
            if (x == 'f') return true;
            if (x == 'g') return true;
            if (x == 'h') return true;
            return false;
        }
        
        private bool ContainsLabeNumber(char x)
        {
            if (x == '1') return true;
            if (x == '2') return true;
            if (x == '3') return true;
            if (x == '4') return true;
            if (x == '5') return true;
            if (x == '6') return true;
            if (x == '7') return true;
            if (x == '8') return true;
            return false;
        }
    }
}