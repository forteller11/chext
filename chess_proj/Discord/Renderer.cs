using System;
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

        public void Init(ISocketMessageChannel channel)
        {
            Channel = channel;
            _embedBuilder= new EmbedBuilder();
            EmbedMessage = Channel.SendMessageAsync(null, false, _embedBuilder.Build()).Result;
        }
        
        public void Redraw()
        {
            _embedBuilder.Color = Color.DarkBlue;

            _embedBuilder.Title = "Chex";
            
            EmbedMessage.ModifyAsync((RefreshEmbedBuilder), null);
        }

        void RefreshEmbedBuilder(MessageProperties messageProperties)
        {
            _embedBuilder.AddField(DateTime.Now.ToLongTimeString(), "``` x o x o x o``` "+
                                            "\nx o x o x o x o`");
            // for (int i = 0; i < Cells.length; i++)
            // {
            //     //todo draw cells
            // }
            messageProperties.Embed = _embedBuilder.Build();
        }
 
    }
}