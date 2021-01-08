using chess_proj.Math;
using chess_proj.Systems.Pieces;

#nullable enable
namespace chess_proj.Systems
{
    public class Board
    {
        public readonly Piece[]?[]? Cells;
        public Player White = new Player();
        public Player Black = new Player();
        
        public Board(int cellCount)
        {
            White = new Player();
            Black = new Player();
            
            Cells = new Piece[cellCount][];
            for (int i = 0; i < Cells.Length; i++)
                Cells[i] = new Piece[cellCount];
            
        }

        public void MovePiece(Int2 target)
        {
            if (GetCell(target) != null)
            {
                
            }
        }

        public Piece? GetCell(Int2 position) => Cells?[position.X][position.Y];
    }
}