using System;
using System.IO;
using System.Threading.Tasks;
using chext.Discord;
using Discord;
using Discord.WebSocket;

namespace chext
{
    class Program
    {
        private DiscordSocketClient _client;
        private GamesManager _gamesManager;
        
        private static string _binPath = Directory.GetCurrentDirectory();
        private static string _projectPath = @"D:\MOVE\Homework\Concordia_Year_03\Hobby\textual chess\chext\chext\chess_proj\";
        
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += () =>
            {
                _gamesManager = new GamesManager(_client);
                return Task.CompletedTask;
            };

            var token = File.ReadAllText(_projectPath + @"Private\token.txt");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }
        
        
        #region debug
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public static Task DebugLog(object message, string src="Debug")
        {
            Log(new LogMessage(LogSeverity.Debug, src, message.ToString()));
            return Task.CompletedTask;
        }
        #endregion
    }
}