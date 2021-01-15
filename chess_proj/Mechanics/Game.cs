using System;
using System.Threading.Tasks;
using chext.Discord.Parsing;
using chext.Discord;
using chext.Math;
using chext.Mechanics.Pieces;
using Discord.WebSocket;

#nullable enable

namespace chext.Mechanics
{
    public class Game
    {
        public readonly ISocketMessageChannel Channel;
        
        
        public Player White;
        public Player Black;
       
        private Renderer _renderer;
        private Board _board;
        private InGameParser _inGameParser;
        
        private static string CHEX_CHANNEL_NAME = "__chex__";

        public Game(GameProposal proposal)
        {
            Channel = proposal.Channel;
            Black = new Player(proposal!.BlackSide, false);
            White = new Player(proposal!.WhiteSide, true);
            
            _inGameParser = new InGameParser();
            _inGameParser.DisplayHandler += OnDisplayMoves;
            _inGameParser.MoveHandler    += OnMove;

            _board = new Board();
            _renderer = new Renderer(_board, new EmbededDrawer(proposal.Channel));
        }

        public void SetupAndRender()
        {
            _board.SetupPiecesStandard();
            _renderer.DrawBoard(_board, GetEnfrachaisedPlayer());
        }
        
        
        /// <summary>/// assumed to be channel in own channel, and message not from own chext bot client/// </summary>
        public void InChannelNonSelfMessageReceived(SocketMessage message)
        {
            Program.DebugLog("msg received game");
            _inGameParser.Parse(message, _board.Dimensions);
        }

        private void OnDisplayMoves(Int2 position)
        {
            Program.DebugLog("On display moves");
            _renderer.DisplayMoves(_board, position);
            _renderer.DrawBoard(_board, GetEnfrachaisedPlayer());
        }
        
        private void OnMove(Int2 from, Int2 to, SocketUser author)
        {
            Program.DebugLog("on move");
            if (GetPlayerFromIdPreferEnfrachaisedPlayer(author, _board.IsWhitesTurn, out var player))
            {
                _board.MovePiece(player!, from, to);
            }
            else 
            {
                Program.WarningLog($"{author.Username} is trying to move {Common.IndexToLabelCoordinate(from, _board.Dimensions)} but cannot as they are not the in-turn player in current game!");
            }

            _renderer.DrawBoard(_board, GetEnfrachaisedPlayer());
            
        }
        
        //gets black or white player if they match socketuser id, if both black and white are same player, prefers returning the player who's turn it currently is
        bool GetPlayerFromIdPreferEnfrachaisedPlayer(SocketUser user, bool isWhitesTurn, out Player? player)
        {
            if (user.Id == White.Id && user.Id == Black.Id)
            {
                player = isWhitesTurn ? White : Black;
                return true;
            }
            
            if (user.Id == White.Id)
            {
                player = White;
                return true;
            }

            if (user.Id == Black.Id)
            {
                player = Black;
                return true;
            }

            player = null;
            return false;
        }

        Player GetEnfrachaisedPlayer() => _board.IsWhitesTurn ? White : Black;
        
    }
}