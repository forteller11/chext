
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            public SocketUser? BlackSide;
            public SocketUser? WhiteSide;

            public SocketUser? GetSide(bool isWhite) => isWhite ? WhiteSide : BlackSide;
            public void SetSide (bool isWhite, SocketUser user)
            {
                if (isWhite) WhiteSide = user;
                else BlackSide = user;
            }

            public bool BothSidesNotNull() => WhiteSide != null && BlackSide != null;

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
            _parser.JoinSideHandler += OnJoinSide;
            
            _client.MessageReceived += OnMessageReceived;
        }

        public void OnGameProposalProposal(ISocketMessageChannel channel)
        {
            Program.DebugLog($"New Game Proposal at {channel.Name}");
            
            if (!_proposals.ContainsKey(channel.Id))
            {
                _proposals.Add(channel.Id, new GameProposal());
            }
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
                if (message.Channel.Id == game.Channel.Id)
                {
                    game.InChannelNonSelfMessageReceived(message);
                }
            }
            
            return Task.CompletedTask;
        }

        private void OnJoin(SocketUser user, ISocketMessageChannel channel)
        {
            Program.DebugLog("on Join");
            if (!ProposalExists(channel.Id)) //if there is already an active proposal
                return;
            var proposal = _proposals[channel.Id];
            if (proposal.WhiteSide == null)
                OnJoinSide(user, channel, true);
            else if (proposal.BlackSide == null)
                OnJoinSide(user, channel, false);
            else throw new AggregateException("not deleting proposals correctly after game has already started?");
        }
        
        private void OnJoinSide(SocketUser user, ISocketMessageChannel channel, bool isWhite)
        {
            Program.DebugLog("on join side");

            if (!ProposalExists(channel.Id)) //if there is already an active proposal
                return;

            var proposal = _proposals[channel.Id];
            var desiredSide = proposal.GetSide(isWhite);
            
            if (desiredSide == null)
            {
                Program.DebugLog($"{user.Username} joined {(isWhite ? "white" : "black")} side!");
                proposal.SetSide(isWhite, user);
            }

            if (proposal.BothSidesNotNull())
            {
                Program.DebugLog($"new game created!");
                var game = new Game(channel, proposal!.WhiteSide, proposal!.BlackSide);
                game.SetupAndRender();
                _games.Add(channel.Id, game);
            }

        }

        private bool ProposalExists(ulong channelId)
        {
            if (_proposals.ContainsKey(channelId)) //if there is already an active proposal
                return true;
            
            Program.DebugLog("Can't join game when no active games exist in current channel!");
            return false;
        }



        //responsible for creating new games, passing in parser, and joining platers, starting games
    }
}