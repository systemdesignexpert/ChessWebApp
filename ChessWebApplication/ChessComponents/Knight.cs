using System;

namespace Chess_System_Design
{
    public class Knight: Piece
    {
        public Knight(Color color)
        {
            this.setColor(color);
            this.setName(PieceName.KNIGHT);
        }

        public override List<string> getInitialPositions()
        {
            if (this.getColor() == Color.WHITE) return new List<String> { "b1", "g1" };
            else return new List<String> { "b8", "g8" };
        }

        public override List<Square> getAllValidSquares(ChessBoard board)
        {
            List<Square> results = new List<Square>();
            Square curr = this.getSquare(board);
            if (curr == null) return results;

            for(int i=0;i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if(Math.Abs(curr.getX() - i) <=2 &&
                        Math.Abs(curr.getY() - j) <=2 &&
                        Math.Abs(curr.getX() - i) + Math.Abs(curr.getY() - j) == 3)
                    {
                        results.Add(board.getSquareAt(i, j));
                    }
                }
            }

            return results;

        }
    }
}