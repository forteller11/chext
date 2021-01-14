using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using chext.Math;
using chext.Mechanics;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

#nullable enable
namespace chext.Discord
{
    public class Renderer
    {
        private EmbededDrawer Drawer;
        public Board _board;
        public char[][] Effects; //over top chess board for visual effects

        private StringBuilder _stringBuilder;
        const int BOARD_BORDER_WIDTH = 4; //IN SPACES
        const int CELL_WIDTH = 4; //IN SPACES
        

        public Renderer(Board board, EmbededDrawer drawer)
        {
            _board = board;
            Drawer = drawer;
            
            Effects = new char[_board.Dimensions][];
            ClearEffects();
            
            _stringBuilder = new StringBuilder(_board.Dimensions * _board.Dimensions * 6);
            _stringBuilder.Append('#', _stringBuilder.Capacity);
            
            Drawer.Builder.Color = Color.Gold;
            Drawer.Builder.Title = "Chext";
        }

        public void DisplayMoves(Int2 position)
        {
            ClearEffects();
            Effects[position.X][position.Y] = 'X';
            
            var moves = _board.GetMoves(position);
            for (int i = 0; i < moves.Count; i++)
            {
                var p = moves[i].Pos;
                Effects[p.X][p.Y] = '.';
            }

            Program.DebugLog("Display moves end");
        }

        public void ClearEffects()
        {
            for (int i = 0; i < _board.Dimensions; i++)
            {
                Effects[i] = new char[_board.Dimensions];
                for (int j = 0; j < _board.Dimensions; j++)
                    Effects[i][j] = '\0';
            }
        }
        public async Task DrawBoard()
        {
            Program.DebugLog("Redraw");
            
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
            
            Drawer.Builder.Description = _stringBuilder.ToString();
            
            await Drawer.Draw();

            void LetterRow()
            {
                _stringBuilder.Append(' ', BOARD_BORDER_WIDTH);
                for (int i = 0; i < _board.Dimensions; i++)
                {
                    _stringBuilder.Append($"  {Common.IndexToLetter(i)} ");
                }
            }
        }
        
 
    }
}