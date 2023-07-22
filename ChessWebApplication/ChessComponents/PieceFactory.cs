using System;
namespace Chess_System_Design
{
	public class PieceFactory
	{
		public PieceFactory()
		{
            
		}

        public Piece createPiece(PieceName name, Color color)
        {
            if(name == PieceName.ROOK)
            {
                return new Rook(color);
            }
            else if(name == PieceName.KING)
            {
                return new King(color);
            }
            else if(name == PieceName.QUEEN)
            {
                return new Queen(color);
            }
            else if(name == PieceName.KNIGHT)
            {
                return new Knight(color);
            }
            else if(name == PieceName.BISHOP)
            {
                return new Bishop(color);
            }
            else if(name == PieceName.PAWN)
            {
                return new Pawn(color);
            }
            
            return null;
        }
    }
}

