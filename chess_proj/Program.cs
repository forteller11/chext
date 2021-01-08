using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace chess_proj
{
    class Program
    {
        private DiscordSocketClient _client;
        private Mechanics.Game _game;
        
        private static string _binPath = Directory.GetCurrentDirectory();
        private static string _projectPath = @"D:\MOVE\Homework\Concordia_Year_03\Hobby\textual chess\chext\chext\chess_proj\";
        
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _game = new Mechanics.Game(_client);

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
        public static Task DebugLog(string message, string src="Debug")
        {
            Log(new LogMessage(LogSeverity.Debug, src, message));
            return Task.CompletedTask;
        }
        #endregion
    }
}