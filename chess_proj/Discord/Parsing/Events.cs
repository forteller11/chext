using Discord;
using Discord.WebSocket;

#nullable enable

namespace chext.Discord.Parsing
{
    public struct JoinEvent
    {
        public SocketUser Author;
        public ISocketMessageChannel PostedChannel;

        public JoinEvent(SocketUser author, ISocketMessageChannel postedChannel)
        {
            Author = author;
            PostedChannel = postedChannel;
        }

        public JoinSideEvent ToJoinSideEvent(bool isWhite) => new JoinSideEvent(Author, PostedChannel, isWhite);
        
        
    }
    
    public struct JoinSideEvent
    {
        public SocketUser Author;
        public ISocketMessageChannel PostedChannel;
        public bool IsWhite;

        public JoinSideEvent(SocketUser author, ISocketMessageChannel postedChannel, bool isWhite)
        {
            Author = author;
            PostedChannel = postedChannel;
            IsWhite = isWhite;
        }
        
        public JoinEvent ToJoinEvent() => new JoinEvent(Author, PostedChannel);
        
        
    }
    

}