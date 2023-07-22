using System;

namespace Chess_System_Design
{
	public class Queen: Piece
	{
		public Queen(Color color)
		{
            this.setColor(color);
            this.setName(PieceName.QUEEN);
        }

        public override List<string> getInitialPositions()
        {
            if (this.getColor() == Color.WHITE) return new List<String> { "d1" };
            else return new List<String> { "d8" };
        }

        public override List<Square> getAllValidSquares(ChessBoard board)
        {
            List<Square> result = board.getAllHorizonatalSquares(this.getSquare(board));
            result.AddRange(board.getAllVerticalSquares(this.getSquare(board)));
            result.AddRange(board.getAllDiagonalSquares(this.getSquare(board)));

            return result;
        }
    }
}

