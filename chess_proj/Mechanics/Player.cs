using System.Collections.Generic;
using chext.Mechanics.Pieces;
using Discord.WebSocket;

namespace chext.Mechanics
{
    public class Player
    {
        public List<Piece> Captured = new List<Piece>();
        public readonly bool IsWhite;
        public readonly ulong Id;
        public readonly string Username;

        //dont just keep ref to user because if socketuser logs out this class is lost, it's the id that's persistent
        public Player(SocketUser user, bool isWhite)
        {
            Id = user.Id;
            Username = user.Username;
            IsWhite = isWhite;
        }
    }
}