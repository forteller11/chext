using System;
using System.Text;
using System.Threading.Tasks;
using chess_proj.Math;
using chess_proj.Mechanics;
using chess_proj.Mechanics.Pieces;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace chess_proj.Discord
{
    public class Renderer
    {
        public EmbedBuilder _embedBuilder;
        public ISocketMessageChannel Channel;
        public RestUserMessage EmbedMessage;
        public Board _board;
        private StringBuilder _stringBuilder;

        public void Init(ISocketMessageChannel channel, Board board)
        {
            Console.WriteLine("init");
            Channel = channel;
            _board = board;
            
            _stringBuilder = new StringBuilder(_board.Dimensions * _board.Dimensions * 2);
            _stringBuilder.Append('o', _stringBuilder.MaxCapacity);

            _embedBuilder = new EmbedBuilder();
            _embedBuilder.Color = Color.DarkBlue;
            _embedBuilder.Title = "Chex";
            Console.WriteLine("end init");

        }
        
        public async Task Redraw()
        {
            Console.WriteLine("start redraw");
            if (EmbedMessage == null)
            {
                Console.WriteLine("hasn't started first message");
                EmbedMessage = await Channel.SendMessageAsync(null, false, _embedBuilder.Build());
            }

            Console.WriteLine("start redraw");
            
            
            _stringBuilder.Clear();
            _stringBuilder.Append("```"); //code block
            Console.WriteLine("redraw 2");
            for (int i = 0; i < _board.Dimensions; i++)
            {
                for (int j = 0; j < _board.Dimensions; j++)
                {
                    if (_board.GetCell(new Int2(i, j)) == null)
                    {
                        _stringBuilder.Append('.');
                    }
                    else
                    {
                        _stringBuilder.Append('0');
                    }
                }

                _stringBuilder.Append('\n');
            }
            
            _stringBuilder.Append("```"); //code block end
            
            _embedBuilder.Fields.Clear();
            _embedBuilder.AddField("board state", _stringBuilder.ToString());
            
            Console.WriteLine("before modify async");
            EmbedMessage.ModifyAsync(properties =>
            {
                properties.Embed = _embedBuilder.Build();
            }, null);
            
            Console.WriteLine("after modify async");
        }
        
 
    }
}