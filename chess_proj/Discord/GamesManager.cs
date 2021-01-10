
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using chext.Discord.Parsing;
using chext.Mechanics;
using Discord.WebSocket;

#nullable enable

namespace chext.Discord
{
    public class GamesManager
    {
        private PreGameParser _parser;

        private DiscordSocketClient _client;
        private Dictionary<ulong, Game> _games = new Dictionary<ulong, Game>();

        /// <summary>
        /// assumed to be called after _client is Ready()
        /// </summary>
        /// <param name="client"></param>
        public GamesManager(DiscordSocketClient client)
        {
            _client = client;
            
            _parser = new PreGameParser();
            _parser.GameProposalHandler += OnGameProposalProposal;
            _parser.JoinHandler += OnJoin;
            
            _client.MessageReceived += OnMessageReceived;
        }

        public void OnGameProposalProposal()
        {
            //create game, add parser which only listens to channel
        }

        public Task OnMessageReceived(SocketMessage message)
        {
            Program.DebugLog("Game Manager message received");
            if (message.Author.Id == _client.CurrentUser.Id)
                return Task.CompletedTask;

            _parser.Parse(message);
            
            foreach (var gameKV in _games)
            {
                var game = gameKV.Value;
                if (message.Id == game.Channel.Id)
                {
                    game.InChannelNonSelfMessageReceived(message);
                }
            }
            
            return Task.CompletedTask;
        }
        
        private void OnJoin(SocketUser user)
        {
            throw new NotImplementedException();
            //see if can join
            //is there qualifier join 'black'?
            //then join
            //then update embed --> new draw author method
            //if both players filled, create new game
        }

        //responsible for creating new games, passing in parser, and joining platers, starting games
    }
}