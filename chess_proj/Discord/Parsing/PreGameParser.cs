using System;
using System.Collections.Generic;
using Discord.WebSocket;

namespace chext.Discord.Parsing
{
    public class PreGameParser
    {
        public event Action<ISocketMessageChannel> GameProposalHandler; 
        public event Action<SocketUser, ISocketMessageChannel> JoinHandler; 
        
        private List<PreGameToken> _tokens = new List<PreGameToken>(20);
        
        public void Parse(SocketMessage message)
        {
        
        
            _tokens.Clear();
            string[] contentWords = message.Content.Split(" ");
            
            #region create tokens
            for (int i = 0; i < contentWords.Length; i++)
            {
                var tokenString = contentWords[i].ToLower();

               
                if (tokenString == "join") _tokens.Add(new PreGameToken(tokenString, PreGameToken.TokenType.Join));
                if (tokenString == "create") _tokens.Add(new PreGameToken(tokenString, PreGameToken.TokenType.Create));
                if (tokenString == "chext") _tokens.Add(new PreGameToken(tokenString, PreGameToken.TokenType.Chext));
               
            }
            #endregion
            
            #region trigger events depending on grammer

            if (_tokens.Count == 1)
            {
                if (_tokens[0].Type == PreGameToken.TokenType.Join)
                    JoinHandler?.Invoke(message.Author, message.Channel);
            }

            if (_tokens.Count == 2)
            {

                if (_tokens[0].Type == PreGameToken.TokenType.Create && _tokens[1].Type == PreGameToken.TokenType.Chext)
                    GameProposalHandler?.Invoke(message.Channel);
                    
            }
            #endregion
        }
    }
}