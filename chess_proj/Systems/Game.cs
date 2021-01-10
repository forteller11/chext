using System;
using System.Threading.Tasks;
using chess_proj.Controller;
using chess_proj.Discord;
using chess_proj.Math;
using chess_proj.Mechanics.Pieces;
using Discord.Rest;
using Discord.WebSocket;

#nullable enable

namespace chess_proj.Mechanics
{
    public class Game
    {
        private DiscordSocketClient _client;
        private SocketTextChannel? _chessChannel;
        private Renderer _renderer;
        private Board _board;
        private Parser _parser;
        public Player White;
        public Player Black;
        private static string CHEX_CHANNEL_NAME = "__chex__";

        public Game(DiscordSocketClient client)
        {
            White = new Player(true);
            Black = new Player(false);
            
            _parser = new Parser();
            _parser.DisplayMoves += OnDisplayMoves;
            _parser.MoveAttempt += OnMoveAttempt;
            
            _client = client;
            _client.Ready += OnReady;
            _client.MessageReceived += OnMessageReceived;
            //_client.ChannelCreated  += OnChannelCreated; //todo make sure no chex name
        }

        private async Task OnReady()
        {
            Program.DebugLog(_client.Guilds.Count);
            foreach (var guild in _client.Guilds)
            {
                #region find or create _chessChannel in guilds
                #region find server named chex
                bool hasChessChannel = false;
                Program.DebugLog("guild");
                Program.DebugLog(guild.Name);
                foreach (var channel in guild.TextChannels)
                {
                    Program.DebugLog("channel");
                    Program.DebugLog($"{channel.Name} == {CHEX_CHANNEL_NAME}");
                    if (channel.Name == CHEX_CHANNEL_NAME)
                    {
                        hasChessChannel = true;
                        _chessChannel = channel;
                        Program.DebugLog("FOUND CHEX CHANNEL");
                        break;
                    }

                }
                #endregion
                #region create new if no chex server found
                if (hasChessChannel == false) //IF not chex channel found in server, create it and then find it agfain
                {
                    //todo CREATE NEW CHANNEL WHEN THIS RETURNS
                    //LIKE AWAIT WITHOUT MAKING METHOD ASYNC
                    await guild.CreateTextChannelAsync(CHEX_CHANNEL_NAME, properties =>
                    {
                        properties.Topic = "CHEX";
                        properties.Position = 0;
                    });
                    
                    Program.DebugLog("CREATED TEXT CHANNEL");
                    
                    foreach (var channel in guild.TextChannels)
                    {
                        if (channel.Name == CHEX_CHANNEL_NAME)
                        {
                            _chessChannel = channel;
                            Program.DebugLog("FOUND NEWLY CREATED CHEX CHANNEL");
                            break;
                        }

                    }
                    
                }
                #endregion
                
                #endregion
                
                _board = new Board();
                SetupStandard(_board);
                
                _renderer = new Renderer(_chessChannel!, _board);
                await _renderer.Redraw();
                
            }

        }

        void SetupStandard(Board board) //todo should be boards care
        {
            //SetupPawnRow(1, White);
            //SetupPawnRow(6, Black);
            SetupRowNonPawns(0, White);
            SetupRowNonPawns(7, Black);
            
            board.SetCell(4, 4, new Rook(Black));
            
            void SetupPawnRow(int rowIndex, Player player)
            {
                for (int i = 0; i < _board.Dimensions; i++)
                    board.SetCell(rowIndex, i, new Pawn(player));
            }
            void SetupRowNonPawns(int rowIndex, Player player)
            {
                board.SetCell(rowIndex, 0, new Rook(player));
                board.SetCell(rowIndex, 7, new Rook(player));
                board.SetCell(rowIndex, 1, new Knight(player));
                board.SetCell(rowIndex, 6, new Knight(player));
                board.SetCell(rowIndex, 2, new Bishop(player));
                board.SetCell(rowIndex, 5, new Bishop(player));
                int queenIndex = player.IsWhite ? 4 : 3;
                int kingIndex  = player.IsWhite ? 3 : 4;
                board.SetCell(rowIndex, queenIndex, new King(player));
                board.SetCell(rowIndex, kingIndex, new Queen(player));
    
            }
        }

        private Task OnMessageReceived(SocketMessage arg)
        {
            Program.DebugLog("msg received");
            if (arg.Author.Id == _client.CurrentUser.Id)
            {
                Program.DebugLog("chex sent/received msg");
                return Task.CompletedTask;
            }
            
            _parser.Parse(arg.Content, _board.Dimensions);
            return Task.CompletedTask;
        }

        private void OnDisplayMoves(Int2 position)
        {
            _renderer.DisplayMoves(position);
            _renderer.Redraw();
        }
        
        private void OnMoveAttempt(Int2 from, Int2 to)
        {
            
        }
    }
}