using System;
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
        private static string CHEX_CHANNEL_NAME = "__chex__";

        public Game(DiscordSocketClient client)
        {
            _renderer = new Renderer();
            _board = new Board(20);
            
            _client = client;
            _client.Ready += OnReady;
            _client.MessageReceived += OnMessageReceived;
            //_client.ChannelCreated  += OnChannelCreated; //todo make sure no chex name
        }
        
        private async Task OnReady()
        {
            //get __chess channel in all guilds.......

            Console.WriteLine(_client.Guilds.Count);
            foreach (var guild in _client.Guilds)
            {
                #region find server named chex
                bool hasChessChannel = false;
                Console.WriteLine("guild");
                Console.WriteLine(guild.Name);
                foreach (var channel in guild.TextChannels)
                {
                    Console.WriteLine("channel");
                    Console.WriteLine($"{channel.Name} == {CHEX_CHANNEL_NAME}");
                    if (channel.Name == CHEX_CHANNEL_NAME)
                    {
                        hasChessChannel = true;
                        _chessChannel = channel;
                        Console.WriteLine("FOUND CHEX CHANNEL");
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
                    
                    Console.WriteLine("CREATED TEXT CHANNEL");
                    
                    foreach (var channel in guild.TextChannels)
                    {
                        if (channel.Name == CHEX_CHANNEL_NAME)
                        {
                            _chessChannel = channel;
                            Console.WriteLine("FOUND NEWLY CREATED CHEX CHANNEL");
                            break;
                        }

                    }
                    
                }
                #endregion
                
                _renderer.Init(_chessChannel, _board);
                await _renderer.Redraw();
                
            }

        }

        private Task OnMessageReceived(SocketMessage arg)
        {
            return Task.CompletedTask;
        }
    }
}