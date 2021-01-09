using System;

namespace chess_proj
{
    public static class Common
    {
        public static string IndexToLetter(int i)
        {
            switch (i)
            {
                case 0: return "a";
                case 1: return "b";
                case 2: return "c";
                case 3: return "d";
                case 4: return "e";
                case 5: return "f";
                case 6: return "g";
                case 7: return "h";
                default: throw new ArgumentException($"index was {i} but must be between 0-7!");
            }
        }
        
        public static int LetterToIndex(char x)
        {
            switch (x)
            {
                case 'a': return 0;
                case 'b': return 1;
                case 'c': return 2;
                case 'd': return 3;
                case 'e': return 4;
                case 'f': return 5;
                case 'g': return 6;
                case 'h': return 7;
                default: throw new ArgumentException($"index was {x} but must be between a-h!");
            }
        }
    }
}