namespace chext.Discord.Parsing
{
    public struct PreGameToken
    {
        public string Value;
        public TokenType Type;
        public enum TokenType
        {
            Join,
            Create,
            Chext,
        }
        
        public PreGameToken(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
    }
}