namespace Chess_System_Design
{
    internal class Pawn : Piece
    {
        public Pawn(Color color)
        {
            this.setColor(color);
            this.setName(PieceName.PAWN);
        }

        public override List<Square> getAllValidSquares(ChessBoard board)
        {
            List<Square> results = new List<Square>();

            Square curr = this.getSquare(board);
            if (curr == null) return results;

            int y = (this.getColor() == Color.WHITE) ? curr.getY() + 1 : curr.getY() - 1;
            Square sq = board.getSquareAt(curr.getX(), y);

            if (sq != null && sq.getPiece() == null)
            {
                results.Add(sq);
            }

            Square lt_diag = board.getSquareAt(curr.getX() - 1, y);
            Square rt_diag = board.getSquareAt(curr.getX() + 1, y);

            if(lt_diag != null && lt_diag.getPiece() != null && lt_diag.getPiece().getColor() != this.getColor())
            {
                results.Add(lt_diag);
            }

            if (rt_diag != null && rt_diag.getPiece() != null && rt_diag.getPiece().getColor() != this.getColor())
            {
                results.Add(rt_diag);
            }

            return results;

        }

        public override List<string> getInitialPositions()
        {
            if (this.getColor() == Color.WHITE) return new List<String> { "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2" };
            else return new List<String> { "a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7" }; ;
        }
    }
}