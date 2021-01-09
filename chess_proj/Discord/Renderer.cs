using System;
using System.Text;
using System.Threading.Tasks;
using chess_proj.Math;
using chess_proj.Mechanics;
using chess_proj.Mechanics.Pieces;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

#nullable enable
namespace chess_proj.Discord
{
    public class Renderer
    {
        private EmbedBuilder _embedBuilder;
        public ISocketMessageChannel Channel;
        public RestUserMessage? EmbedMessage;
        public Board _board;
        private StringBuilder _stringBuilder;

        public Renderer(ISocketMessageChannel channel, Board board)
        {
            Channel = channel;
            _board = board;

            _stringBuilder = new StringBuilder(_board.Dimensions * _board.Dimensions * 6);
            _stringBuilder.Append('-', _stringBuilder.Capacity);
            
            _embedBuilder = new EmbedBuilder();
            _embedBuilder.Color = Color.Gold;
            _embedBuilder.Title = "Chex";
        }

        
        public async Task Redraw()
        {
            Console.WriteLine("Redraw");
            //if you haven't drawn first message yet
         
            if (EmbedMessage == null)
            {
                Console.WriteLine("hasn't started first message");
                EmbedMessage = await Channel.SendMessageAsync(null, false, _embedBuilder.Build());
            }

            _stringBuilder.Clear();
            //code block
            Console.WriteLine("redraw 2");
            for (int i = 0; i < _board.Dimensions; i++)
            {
                for (int j = 0; j < _board.Dimensions; j++)
                {
                    if (_board.GetCell(new Int2(i, j)) == null)
                    {
                        _stringBuilder.Append(" .");
                    }
                    else
                    {
                        _stringBuilder.Append(" " + _board.Cells[i][j]!.EmoteName + " ");
                    }
                }

                _stringBuilder.Append(" \n");
            }
            
            
            
            _embedBuilder.Fields.Clear();
            _embedBuilder.AddField("Turn", _stringBuilder.ToString());

            _embedBuilder.Description = _stringBuilder.ToString();
            _embedBuilder.Description = _stringBuilder.ToString();
            
            Console.WriteLine("before modify async");
            EmbedMessage.ModifyAsync(properties =>
            {
                properties.Embed = _embedBuilder.Build();
            }, null);
            
            Console.WriteLine("after modify async");
        }
        
 
    }
}