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
        
        private static string _binPath= Directory.GetCurrentDirectory();
        private static string _projectPath = @"D:\MOVE\Homework\Concordia_Year_03\Hobby\textual chess\chext\chext\chess_proj\";
        
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.LoggedIn += OnLogin;
            _client.MessageReceived += OnMessageReceived;
            _client.
            
            var token = File.ReadAllText(_projectPath + @"Private\token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
            //Console.WriteLine(token);
        }

        

        Task OnLogin()
        {
            DebugLog("completed task");
            return Task.CompletedTask;
        }
        Task OnMessageReceived(SocketMessage socketMessage)
        {
            DebugLog(socketMessage.Content);
            return Task.CompletedTask;
        }
        
        #region debug
        Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        Task DebugLog(string message, string src="Debug")
        {
            Log(new LogMessage(LogSeverity.Debug, src, message));
            return Task.CompletedTask;
        }
        #endregion
    }
}