namespace Chess_System_Design
{
    internal class Bishop : Piece
    {
        public Bishop(Color color)
        {
            this.setColor(color);
            this.setName(PieceName.BISHOP);
        }

        public override List<Square> getAllValidSquares(ChessBoard board)
        {
            return board.getAllDiagonalSquares(this.getSquare(board));
        }

        public override List<string> getInitialPositions()
        {
            if (this.getColor() == Color.WHITE) return new List<String> { "c1", "f1" };
            else return new List<String> { "c8", "f8" };
        }
    }
}