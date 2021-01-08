using System.Diagnostics;
using System.Threading.Tasks;
using chess_proj.Discord;
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
        private static string CHEX_CHANNEL_NAME = "__Chex Match__";

        public Game(DiscordSocketClient client)
        {
            _renderer = new Renderer();
            _board = new Board(20);
            
            _client = client;
            _client.Connected += OnConnected;
            _client.MessageReceived += OnMessageReceived;
        }

        private async Task OnConnected()
        {
            //get __chess channel in all guilds.......
    
            foreach (var guild in _client.Guilds)
            {
                #region find server named chex
                bool hasChessChannel = false;
                Program.DebugLog(guild.Name);
                foreach (var channel in guild.TextChannels)
                {
                    Program.DebugLog(channel.Name);
                    if (channel.Name == CHEX_CHANNEL_NAME)
                    {
                        hasChessChannel = true;
                        _chessChannel = channel;
                        Program.DebugLog("FOUND CHEX CHANNEL");
                    }

                }
                #endregion
                #region create new if no chex server found
                if (hasChessChannel == false) //IF not chex channel found in server, create it and then find it agfain
                {
                    Program.DebugLog("CREATED TEXT CHANNEL");
                    var newChannel = await guild.CreateTextChannelAsync(CHEX_CHANNEL_NAME, properties =>
                    {
                        properties.Topic = "CHEX";
                        properties.Position = 0;
                    });
                    
                    foreach (var channel in guild.TextChannels)
                    {
                        if (channel.Name == CHEX_CHANNEL_NAME)
                        {
                            _chessChannel = channel;
                            Program.DebugLog("FOUND NEWLY CREATED CHEX CHANNEL");
                        }

                    }
                }
                #endregion
                
                //INIT RENDERER
                _renderer.Init(_chessChannel);
                _renderer.Redraw();
     
            }


           
        }

        private Task OnMessageReceived(SocketMessage arg)
        {
            return Task.CompletedTask;
        }
    }
}