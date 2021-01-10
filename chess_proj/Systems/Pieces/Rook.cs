using System.Collections.Generic;
using chess_proj.Math;

#nullable enable
namespace chess_proj.Mechanics.Pieces
{
    public class Rook : Piece
    {
        
        public Rook(Player owner) : base(owner, 'r') { }

        public override void RefreshValidMoves(Int2 piecePosition, Piece?[][] cells, List<Move> moves)
        {

            SearchStraight(new Int2(0,1));
            SearchStraight(new Int2(0,-1));
            SearchStraight(new Int2(1,0));
            SearchStraight(new Int2(-1,0));

            void SearchStraight(Int2 increment)
            {
                var index = piecePosition;
                while (true)
                {
                    index += increment;
                    if (!IsWithinBounds(index, cells))
                        break;
                    if (cells[index.X][index.Y] != null)
                    {
                        moves.Add(new Move(index, index - increment));
                        break;
                    }
                    
                    moves.Add(new Move(index, index - increment));
                }
            }
        }
    }
}