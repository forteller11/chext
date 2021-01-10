
using System.Collections.Generic;
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
        
        class GameProposal
        {
            public SocketUser? BlackPlayer;
            public SocketUser? WhitePlayer;
        }
        private Dictionary<ulong, GameProposal> _proposals = new Dictionary<ulong, GameProposal>();

        /// <summary>
        /// assumed to be called after _client is Ready()
        /// </summary>
        /// <param name="client"></param>
        public GamesManager(DiscordSocketClient client)
        {
            Program.DebugLog("Game manger created");
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

        private void OnJoin(SocketUser user, ISocketMessageChannel channel)
        {
            var channelId = channel.Id;

            if (_proposals.ContainsKey(channelId)) //if there is already an active proposal
            {
                var proposal = _proposals[channelId];
                if (proposal.WhitePlayer == null)
                {
                    proposal.WhitePlayer = user;
                    Program.DebugLog($"{user.Username} joined white side!");
                }
                else
                {
                    proposal.BlackPlayer = user;
                    Program.DebugLog($"{user.Username} joined black side!");
                }

                if (proposal.BlackPlayer != null && proposal.BlackPlayer != null)
                {
                    Program.DebugLog($"new game created!");
                    var game = new Game(channel, proposal.WhitePlayer, proposal.BlackPlayer);
                    game.SetupAndRender();
                    _games.Add(channelId, game);
                }
            }
        }



        //responsible for creating new games, passing in parser, and joining platers, starting games
    }
}