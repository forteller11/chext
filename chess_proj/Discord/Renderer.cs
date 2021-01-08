using chess_proj.Systems;
using chess_proj.Systems.Pieces;
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

        public Renderer(ISocketMessageChannel channel)
        {
            Channel = channel;
            _embedBuilder = new EmbedBuilder();
        }

        public void Init(ISocketMessageChannel channel)
        {
            Channel = channel;
            _embedBuilder= new EmbedBuilder();
            EmbedMessage = Channel.SendMessageAsync(null, false, _embedBuilder.Build()).Result;
        }
        
        
        
        public void InitRender(Piece[][] cells)
        {
            var embed = new EmbedBuilder();
            embed.Color = Color.DarkBlue;

            embed.Title = "Chex";
            embed.AddField("cells", "``` x o x o x o``` "+
                                    "\nx o x o x o x o`");

            var msg = socketMessage.Channel as IMessageChannel;
            msg.SendMessageAsync(null, false, embed.Build());
        }

        void ModifyEmbed()
        {
            _embedBuilder.Color = Color.DarkBlue;

            _embedBuilder.Title = "Chex";
            _embedBuilder.AddField("cells", "``` x o x o x o``` "+
                                            "\nx o x o x o x o`");
            EmbedMessage.ModifyAsync((testfunc), null);
        }

        void Redraw(MessageProperties messageProperties)
        {
            for (int i = 0; i < Cells.length; i++)
            {
                //todo draw cells
            }
            messageProperties.Embed = _embedBuilder.Build();
        }
        public void Render(Board board)
        {
            //todo embed emoji
        }
    }
}