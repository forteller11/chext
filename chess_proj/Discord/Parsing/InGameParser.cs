using System;
using System.Collections.Generic;
using chext.Math;
using Discord.WebSocket;

namespace chext.Discord.Parsing
{
    public class InGameParser
    {

        public event Action<Int2> DisplayHandler;
       /// <summary>/// from, to, is mover white?/// </summary>
        public event Action<Int2, Int2, SocketUser> MoveHandler; 
        

        private List<InGameToken> _tokens = new List<InGameToken>(20);

        public void Parse(SocketMessage message, int boardDimensions)
        {
            _tokens.Clear();
            string[] contentWords = message.Content.Split(" ");
            
            #region create tokens
            for (int i = 0; i < contentWords.Length; i++)
            {
                var tokenString = contentWords[i].ToLower();

                if (tokenString.Length == 2)
                {
                    if (ContainsLabelLetter(tokenString[0]) && ContainsLabelNumber(tokenString[1]))
                        _tokens.Add(new InGameToken(tokenString, InGameToken.TokenType.CellCoord));
                }
                
            }
            #endregion
            
            
            #region trigger events depending on grammer
            if (_tokens.Count == 1)
            {
                if (_tokens[0].Type == InGameToken.TokenType.CellCoord)
                    DisplayHandler?.Invoke(Common.FromLabelToIndexCoordinate(_tokens[0].Value, boardDimensions));
            }

            if (_tokens.Count == 2)
            {
                if (_tokens[0].Type == InGameToken.TokenType.CellCoord && _tokens[1].Type == InGameToken.TokenType.CellCoord)
                {
                    var coordFrom = Common.FromLabelToIndexCoordinate(_tokens[0].Value, boardDimensions);
                    var coordTo = Common.FromLabelToIndexCoordinate(_tokens[1].Value, boardDimensions);
                    MoveHandler?.Invoke(coordFrom, coordTo, message.Author);
                }
            }
            
            #endregion
        }

        private bool ContainsLabelLetter(char x)
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
        
        private bool ContainsLabelNumber(char x)
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