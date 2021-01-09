using System;
using System.Text;
using System.Threading.Tasks;
using chess_proj.Math;
using chess_proj.Mechanics;
using chess_proj.Mechanics.Pieces;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

#nullable enable
namespace chess_proj.Discord
{
    public class Renderer
    {
        private EmbedBuilder _embedBuilder;
        public ISocketMessageChannel Channel;
        public RestUserMessage? EmbedMessage;
        public Board _board;
        public char[][] Effects; //over top chess board for visual effects
        //todo seperate visual from systems
        
        private StringBuilder _stringBuilder;
        const int BOARD_BORDER_WIDTH = 4; //IN SPACES
        const int CELL_WIDTH = 4; //IN SPACES
        

        public Renderer(ISocketMessageChannel channel, Board board)
        {
            Channel = channel;
            _board = board;

            Effects = new char[_board.Dimensions][];
            for (int i = 0; i < _board.Dimensions; i++)
            {
                Effects[i] = new char[_board.Dimensions];
                for (int j = 0; j < _board.Dimensions; j++)
                    Effects[i][j] = '\0';
                
            }
            _stringBuilder = new StringBuilder(_board.Dimensions * _board.Dimensions * 6);
            _stringBuilder.Append('-', _stringBuilder.Capacity);
            
            _embedBuilder = new EmbedBuilder();
            _embedBuilder.Color = Color.Gold;
            _embedBuilder.Title = "Chex";
        }

        
        public async Task Redraw()
        {
            Console.WriteLine("Redraw");
            //if you haven't drawn first message yet
         
            if (EmbedMessage == null)
            {
                Console.WriteLine("hasn't started first message");
                EmbedMessage = await Channel.SendMessageAsync(null, false, _embedBuilder.Build());
            }

            _stringBuilder.Clear();
            _stringBuilder.Append("```"); //code block start

            
            LetterRow();
            _stringBuilder.Append("\n");
            
            _stringBuilder.Append(new string(' ', BOARD_BORDER_WIDTH));
            _stringBuilder.Append(new string('-', _board.Dimensions*CELL_WIDTH+1));
            _stringBuilder.Append("\n");
            
            
            for (int i = 0; i < _board.Dimensions; i++)
            {
                #region piece row
                
                int rowLabel = _board.Dimensions - i;
                _stringBuilder.Append("  " + (rowLabel).ToString().PadRight(BOARD_BORDER_WIDTH-2, ' '));
                
                for (int j = 0; j < _board.Dimensions; j++)
                {
                    if (Effects[i][j] != '\0')
                    {
                        _stringBuilder.Append($"| {Effects[i][j]} ");
                    }
                    else if (_board.GetCell(i, j) == null)
                        _stringBuilder.Append("|   ");
                    else
                        _stringBuilder.Append("|" + _board.Cells[i][j]!.Name);
                }
                _stringBuilder.Append("|");
                _stringBuilder.Append((rowLabel).ToString().PadLeft(BOARD_BORDER_WIDTH-2, ' ') + " ");
                #endregion

                #region spacer row
                _stringBuilder.Append(" \n");
                if (i != _board.Dimensions - 1)
                {
                    _stringBuilder.Append(new string(' ', BOARD_BORDER_WIDTH));
                    
                    for (int j = 0; j < _board.Dimensions; j++)
                        _stringBuilder.Append("|---");
                    
                    _stringBuilder.Append("|");
                    _stringBuilder.Append(" \n");
                   
                }
                else //bottom row
                {
                    _stringBuilder.Append(new string(' ', BOARD_BORDER_WIDTH));
                    _stringBuilder.Append(new string('-', _board.Dimensions*CELL_WIDTH+1));
                }
                #endregion
            }

           
            _stringBuilder.Append("\n");
            LetterRow();

            _stringBuilder.Append("```"); //code block end
            
            
            
            _embedBuilder.Fields.Clear();
            _embedBuilder.AddField("Turn", _stringBuilder.ToString(), false);

            _embedBuilder.Description = _stringBuilder.ToString();
            _embedBuilder.Description = _stringBuilder.ToString();
            
            Console.WriteLine("before modify async");
            EmbedMessage.ModifyAsync(properties =>
            {
                properties.Embed = _embedBuilder.Build();
            }, null);
            
            Console.WriteLine("after modify async");

            void SpaceRow(StringBuilder builder)
            {
                builder.Append(" \n");
                builder.Append(new string(' ', BOARD_BORDER_WIDTH));
                
                for (int j = 0; j < _board.Dimensions; j++)
                    builder.Append("|---");
                
                builder.Append("|");
                builder.Append(" \n");
            }

            void LetterRow()
            {
                _stringBuilder.Append(' ', BOARD_BORDER_WIDTH);
                for (int i = 0; i < _board.Dimensions; i++)
                {
                    _stringBuilder.Append($"  {IndexToLetter(i)} ");
                }
            }

            string IndexToLetter(int i)
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
        }
        
 
    }
}