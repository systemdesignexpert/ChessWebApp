using Chess_System_Design;

namespace Chess_System_Design
{
    public class Rook : Piece
    {
        public override List<Square> getAllValidSquares(ChessBoard board)
        {
            List<Square> result = board.getAllHorizonatalSquares(this.getSquare(board));
            result.AddRange(board.getAllVerticalSquares(this.getSquare(board)));

            return result;
        }

        public override List<string> getInitialPositions()
        {
            if (this.getColor() == Color.WHITE) return new List<String> { "a1", "h1" };
            else return new List<String> { "a8", "h8" };
        }

        public Rook(Color color)
        {
            this.setColor(color);
            this.setName(PieceName.ROOK);
        }
    }
}

