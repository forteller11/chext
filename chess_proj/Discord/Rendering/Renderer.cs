using System.Text;
using System.Threading.Tasks;
using chext.Math;
using chext.Mechanics;
using Discord;

#nullable enable
namespace chext.Discord
{
    public class Renderer
    {
        private EmbededDrawer Drawer;
        public char[][] Effects; //over top chess board for visual effects

        private StringBuilder _stringBuilder;
        const int BOARD_BORDER_WIDTH = 4; //IN SPACES
        const int CELL_WIDTH = 4; //IN SPACES
        

        public Renderer(Board board, EmbededDrawer drawer)
        {
            Drawer = drawer;
            
            Effects = new char[board.Dimensions][];
            ClearEffects(board);
            
            _stringBuilder = new StringBuilder(board.Dimensions * board.Dimensions * 6);
            _stringBuilder.Append('#', _stringBuilder.Capacity);
            
            Drawer.Builder.Color = Color.Gold;
            Drawer.Builder.Title = "Chext";
        }

        public void DisplayMoves(Board board, Int2 position)
        {
            ClearEffects(board);
            Effects[position.X][position.Y] = '0';
            
            var moves = board.GetMoves(position);
            for (int i = 0; i < moves.Count; i++)
            {
                var p = moves[i].Pos;
                Effects[p.X][p.Y] = 'x';
            }

            Program.DebugLog("Display moves end");
        }

        public void ClearEffects(Board board)
        {
            for (int i = 0; i < board.Dimensions; i++)
            {
                Effects[i] = new char[board.Dimensions];
                for (int j = 0; j < board.Dimensions; j++)
                    Effects[i][j] = '\0';
            }
        }
        public async Task DrawBoard(Board board, Player enfrachaisedPlayer)
        {
            Program.DebugLog("Redraw");
            
            #region board
            _stringBuilder.Clear();
            _stringBuilder.Append("```"); //code block start

            LetterRow();
            _stringBuilder.Append("\n");
            
            _stringBuilder.Append(new string(' ', BOARD_BORDER_WIDTH));
            _stringBuilder.Append(new string('-', board.Dimensions*CELL_WIDTH+1));
            _stringBuilder.Append("\n");
            
            
            for (int i = 0; i < board.Dimensions; i++)
            {
                #region piece row
                
                int rowLabel = board.Dimensions - i;
                _stringBuilder.Append("  " + (rowLabel).ToString().PadRight(BOARD_BORDER_WIDTH-2, ' '));
                
                for (int j = 0; j < board.Dimensions; j++)
                {
                    var cell = board.GetCell(i, j);
                    
                    var effect = Effects[i][j];
                    char right = effect == '\0' ? ' ' : effect;
                    
                    if (cell == null)
                        _stringBuilder.Append("|  " + right);
                    else
                    {

                        char left  = cell.IsWhite ? ' ' : '`';
                        char middle = board.Cells[i][j].Type;

                        _stringBuilder.Append("|" + left + middle + right);
                    }
                }
                _stringBuilder.Append("|");
                _stringBuilder.Append((rowLabel).ToString().PadLeft(BOARD_BORDER_WIDTH-2, ' ') + " ");
                #endregion

                #region spacer row
                _stringBuilder.Append(" \n");
                if (i != board.Dimensions - 1)
                {
                    _stringBuilder.Append(new string(' ', BOARD_BORDER_WIDTH));
                    
                    for (int j = 0; j < board.Dimensions; j++)
                        _stringBuilder.Append("|---");
                    
                    _stringBuilder.Append("|");
                    _stringBuilder.Append(" \n");
                   
                }
                else //bottom row
                {
                    _stringBuilder.Append(new string(' ', BOARD_BORDER_WIDTH));
                    _stringBuilder.Append(new string('-', board.Dimensions*CELL_WIDTH+1));
                }
                #endregion
            }

           
            _stringBuilder.Append("\n");
            LetterRow();

            _stringBuilder.Append("```"); //code block end
            
            Drawer.Builder.Description = _stringBuilder.ToString();
            #endregion

            Drawer.Builder.Title = $"{Turn()}'s ({enfrachaisedPlayer.Username}) turn.";
            
            ClearEffects(board);
            
            await Drawer.Draw();

            #region nested functions
            void LetterRow()
            {
                _stringBuilder.Append(' ', BOARD_BORDER_WIDTH);
                for (int i = 0; i < board.Dimensions; i++)
                {
                    _stringBuilder.Append($"  {Common.IndexToLetter(i)} ");
                }
            }
            
            string Turn () => board.IsWhitesTurn ? "White" : "Black";
            #endregion
        }
        
 
    }
}