using System;
using chext.Math;

namespace chext
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

        public static int ParseCharToInt(char x)
        {
            switch (x)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                default: throw new ArgumentException($"char must be number but was {x}!");
            }
        }
        
        public static Int2 FromLabelToIndexCoordinate(string coordinate, int boardDimensions)
        {
            int x = boardDimensions - ParseCharToInt(coordinate[1]);
            int y = LetterToIndex(coordinate[0]);
            return new Int2(x,y);
        }
        
        public static string IndexToLabelCoordinate(Int2 pos, int boardDimensions)
        {
            var x = Common.IndexToLetter(pos.X);
            var y   = boardDimensions - pos.Y; 
            
            return x + y;
        }
    }
}