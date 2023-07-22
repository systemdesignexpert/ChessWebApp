using System;

namespace Chess_System_Design
{
	public class King: Piece
	{
		public King(Color color)
		{
            this.setColor(color);
            this.setName(PieceName.KING);
		}

        public override List<Square> getAllValidSquares(ChessBoard board)
        {
            return board.getAllNieghboringSquares(this.getSquare(board));
        }

        public override List<string> getInitialPositions()
        {
            if (this.getColor() == Color.WHITE) return new List<String> { "e1" };
            else return new List<String> { "e8" };
        }

        public bool isUnderAttack(ChessBoard board)
        {
            Player player = this.getOpponentPlayer();
            List<Piece> pieces = player.getAllPieces();
            List<Square> squares = new List<Square>();

            foreach (Piece p in pieces)
            {
                
                if (p.getName() == PieceName.KNIGHT)
                {
                    squares.AddRange(p.getAllLegalSquares(board, true));
                } else
                {
                    squares.AddRange(p.getAllLegalSquares(board, false));
                }


            }

            foreach(Square sq in squares)
            {
                if (sq.getPiece() == this) return true;
            }

            return false;


        }
    }
}

