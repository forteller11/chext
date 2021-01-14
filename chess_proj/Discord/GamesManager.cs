
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using chext.Discord.Parsing;
using Discord;
using Discord.WebSocket;
using Game = chext.Mechanics.Game;

#nullable enable

namespace chext.Discord
{
    
    public class GamesManager
    {
        private PreGameParser _parser;
        private DiscordSocketClient _client;
        
        private Dictionary<ulong, Game> _games = new Dictionary<ulong, Game>();

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
        public void OnGameProposalProposal(ISocketMessageChannel channel, SocketUser creator)
        {
            if (_proposals.ContainsKey(channel.Id) )
            {
                Program.WarningLog("Cannot create proposal when one already exists in same channel!");
                return;
            }
        
            if (_games.ContainsKey(channel.Id))
            {
                Program.WarningLog("Cannot create proposal when an active game already exists in same channel!");
                return;
            }
            
            Program.DebugLog($"New Game Proposal at {channel.Name}");
            var newProposal = new GameProposal(creator, channel, new EmbededDrawer(channel));
            _proposals.Add(channel.Id, newProposal);
            GameManagerRenderer.DrawProposal(newProposal);
        }

        #region joins
        private void OnJoin(JoinEvent e)
        {
            Program.DebugLog("on Join");
            if (!ProposalExists(e.PostedChannel.Id)) //if there is already an active proposal
                return;
            
            var proposal = _proposals[e.PostedChannel.Id];
            if (proposal.WhiteSide == null)
                OnJoinSide(e.ToJoinSideEvent(true));
            else if (proposal.BlackSide == null)
                OnJoinSide(e.ToJoinSideEvent(false));
            else throw new AggregateException("not deleting proposals correctly after game has already started?");
        }
        
        private void OnJoinSide(JoinSideEvent e)
        {
            Program.DebugLog("on join side");

            if (!ProposalExists(e.PostedChannel.Id)) //if there is already an active proposal
                return;

            var proposal = _proposals[e.PostedChannel.Id];
            var desiredSide = proposal.GetSide(e.IsWhite);
            
            if (desiredSide == null)
            {
                Program.DebugLog($"{e.Author.Username} joined {(e.IsWhite ? "white" : "black")} side!");
                proposal.SetSide(e.IsWhite, e.Author);
                GameManagerRenderer.DrawProposal(proposal);
            }

            if (proposal.BothSidesNotNull())
            {
                Program.DebugLog($"new game created!");
                var game = new Game(proposal);
                game.SetupAndRender();
                _games.Add(e.PostedChannel.Id, game);
                _proposals.Remove(e.PostedChannel.Id);
            }

        }

        private bool ProposalExists(ulong channelId)
        {
            if (_proposals.ContainsKey(channelId)) //if there is already an active proposal
                return true;
            
            Program.WarningLog("Can't join game when no active games exist in current channel!");
            return false;
        }
        #endregion
        
    }
}