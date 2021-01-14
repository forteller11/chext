using Discord;
using Discord.WebSocket;
#nullable enable

namespace chext.Discord
{
    public class GameProposal
    {
        public ISocketMessageChannel Channel;
        public SocketUser Creator;
        public SocketUser? BlackSide;
        public SocketUser? WhiteSide;
        public EmbededDrawer Drawer; //todo, remove this if unused

        public GameProposal(SocketUser creator, ISocketMessageChannel channel, EmbededDrawer drawer)
        {
            Creator = creator;
            Channel = channel;
            Drawer = drawer;
        }
        
        public SocketUser? GetSide(bool isWhite) => isWhite ? WhiteSide : BlackSide;
        public void SetSide (bool isWhite, SocketUser user)
        {
            if (isWhite) WhiteSide = user;
            else BlackSide = user;
        }

        public bool BothSidesNotNull() => WhiteSide != null && BlackSide != null;

    }
}