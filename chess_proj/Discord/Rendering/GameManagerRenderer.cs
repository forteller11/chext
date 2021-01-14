using Discord;
using Discord.WebSocket;

#nullable enable
namespace chext.Discord
{
    public static class GameManagerRenderer
    {
        public static void DrawProposal(GameProposal proposal)
        {
            var builder = proposal.Drawer.Builder;
            
            builder.Fields.Clear();
            builder.Color = Color.Magenta;
            builder.Title = $"{proposal.Creator.Username} started a Chext Proposal";
            builder.AddField($"***{NameOrEmpty(proposal.WhiteSide)}*** vs ***{NameOrEmpty(proposal.BlackSide)}***", "White vs Black");
            builder.Footer = new EmbedFooterBuilder{Text = "type \"join\" or \"join white/black\" to participate in game"};

            proposal.Drawer.Draw();

            string NameOrEmpty(SocketUser? user) => (user == null) ? "(empty)" : user.Username;
        }
    }
}