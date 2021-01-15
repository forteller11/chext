using System.Collections.Generic;
using chext.Math;

namespace chext.Mechanics.Pieces
{
    public class Pawn : Piece
    {
        private bool _isFirstMove = true;
        public Pawn(bool isWhite) : base(isWhite, 'p') { }

        public override void RefreshValidMoves(Board board, Int2 pos, List<Move> moves)
        {
            int vertDir = IsWhite ? 1 : -1; //directions are reversed, so vert corresponds to x index of Int2()

            AddMoveIfValid(board, moves, new Int2(vertDir, 0) + pos, pos);
            
            if (_isFirstMove)
                AddMoveIfValid(board, moves, new Int2(vertDir * 2, 0) + pos, pos);


            var  leftTake = new Int2(pos.X + vertDir, pos.Y - 1);
            var rightTake = new Int2(pos.X + vertDir, pos.Y + 1);
            
            if (board.GetCell(leftTake) != null)
                moves.Add(new Move(leftTake, pos));
            if (board.GetCell(rightTake) != null)
                moves.Add(new Move(rightTake, pos));
  
            
        }

        public override void OnPostMove()
        {
            _isFirstMove = false;
        }
    }
}