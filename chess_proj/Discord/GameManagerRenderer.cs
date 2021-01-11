using Discord;
using Discord.WebSocket;

#nullable enable
namespace chext.Discord
{
    public static class GameManagerRenderer
    {
        public static void DrawProposal(GamesManager.GameProposal proposal, ISocketMessageChannel channel)
        {
            proposal.EmbedBuilder = new EmbedBuilder();
            proposal.EmbedBuilder.Color = Color.Magenta;
            proposal.EmbedBuilder.Title = $"{proposal.Creator.Username} started a Chext Proposal";
            proposal.EmbedBuilder.AddField($"***{NameOrEmpty(proposal.WhiteSide)}*** vs ***{NameOrEmpty(proposal.BlackSide)}***", "White vs Black");
            var footer = new EmbedFooterBuilder();
            footer.Text = "type \"join\" or \"join white/black\" to participate in game";
            proposal.EmbedBuilder.Footer = footer;
            channel.SendMessageAsync(null, false, proposal.EmbedBuilder.Build());

            string NameOrEmpty(SocketUser? user) => (user == null) ? "(empty)" : user.Username;
            
        }
    }
}