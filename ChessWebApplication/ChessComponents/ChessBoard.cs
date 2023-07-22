using System;
using System.Numerics;

namespace Chess_System_Design
{
	public class ChessBoard
	{
        private Square[] squares;

		public ChessBoard()
		{
            squares = new Square[64];
            int k = 0;
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    Square sq = new Square(i, j, null);
                    squares[k++] = sq;
                }
            }
		}

        public List<Square> getAllSquares()
        {
            return this.squares.ToList();
        }

        public void displayBoard()
        {

            for(int j=0; j<8; j++)
            {
                string[] arr = new string[8];
                for(int i=0; i<8; i++) {

                    Piece p = this.getSquareAt(i, j).getPiece();
                    if (p != null)
                    {
                        arr[i] = this.getSquareAt(i, j).getPiece().getName().ToString();
                    }
                    else
                    {
                        arr[i] = "";
                    }
                }
                Console.WriteLine(String.Format("|{0,8}|{1,8}|{2,8}|{3,8}|{4,8}|{5,8}|{6,8}|{7,8}|", arr));
            }

        }

        public Square findSquareForPiece(Piece p)
        {
            foreach(var sq in squares)
            {
                if (sq.getPiece() == p) return sq;
            }
            return null;
        }

        public Square getSquareAt(int x, int y)
        {
            foreach(Square sq in squares)
            {
                if(sq.getX() == x && sq.getY() == y)
                {
                    return sq;
                }
            }
            return null;
        }

        public Square getSquareAtPos(string s)
        {
            foreach (Square sq in squares)
            {
                if (sq.getPos() == s)
                {
                    return sq;
                }
            }
            return null;
        }

        public Square getForwardSquare(Square sq)
        {
            
            if (sq == null) return null;
            foreach (Square _sq in squares)
            {
                if((sq.getY() + 1 == _sq.getY()) && sq.getX() == _sq.getX())
                {
                    return _sq;
                }

            }
            return null;
        }

        public List<Square> getForwardDiagonalSquare(Square sq)
        {
            List<Square> listOfSquares = new List<Square>();
            if (sq == null) return listOfSquares;

            foreach (Square _sq in squares)
            {
                if (

                        (sq.getY() + 1 == _sq.getY())
                        &&
                        ((sq.getX() + 1 == _sq.getX()) || (sq.getX() - 1 == _sq.getX()))

                    )
                {
                    listOfSquares.Add(sq);
                }

            }

            return listOfSquares;
        }

        public List<Square> getAllNieghboringSquares(Square sq)
        {
            List<Square> listOfSquares = new List<Square>();
            if (sq == null) return listOfSquares;

            int x = sq.getX();
            int y = sq.getY();

            foreach (Square _sq in squares)
            {
                if (
                    (Math.Abs(_sq.getX() - x) <= 1) &&
                    (Math.Abs(_sq.getY() - y) <= 1)
                  )
                {
                    if(_sq != sq) listOfSquares.Add(_sq);
                }
            }

            return listOfSquares;
        }



        public List<Square> getAllVerticalSquares(Square sq)
        {
            List<Square> listOfSquares = new List<Square>();
            if (sq == null) return listOfSquares;
            int x = sq.getX();

            foreach(Square _sq in squares)
            {
                if(_sq.getX() == x)
                {
                    listOfSquares.Add(_sq);
                }
            }
            return listOfSquares;
        }

        public List<Square> getAllHorizonatalSquares(Square sq)
        {
            List<Square> listOfSquares = new List<Square>();
            if (sq == null) return listOfSquares;

            int y = sq.getY();
            foreach (Square _sq in squares)
            {
                if (_sq.getY() == y)
                {
                    listOfSquares.Add(_sq);
                }
            }
            return listOfSquares;
        }

        public List<Square> getAllDiagonalSquares(Square sq)
        {
            List<Square> listOfSquares = new List<Square>();

            if (sq == null) return listOfSquares;

            int x = sq.getX();
            int y = sq.getY();

            
            foreach (Square _sq in squares)
            {
                if (Math.Abs(_sq.getY() - y) == Math.Abs(_sq.getX() -x))
                {
                    listOfSquares.Add(_sq);
                }
            }
            return listOfSquares;
        }


        public bool isGameOver(Player player)
        {
            bool checkMated = isCheckMated(player);
            bool staleMated = isStaleMated(player);

            return checkMated || staleMated;

        }

        public bool isCheckMated(Player player)
        {
            if (!isInCheck(player)) return false;

            Piece king = player.getPiecesWithName(PieceName.KING)[0];
            Square originalSq = king.getSquare(this);
            bool mated = true;

            List<Square> squares = king.getAllLegalSquares(this, false);
            foreach (var sq in squares)
            {
                Piece captured = sq.getPiece();

                sq.setPiece(king);
                originalSq.setPiece(null);

                if (!isInCheck(player)) {
                    mated = false;
                }

                originalSq.setPiece(king);
                sq.setPiece(captured);

                if (!mated) return false;
            }

            return true;
            
        }

        public bool isInCheck(Player player)
        {
            Piece p = player.getPiecesWithName(PieceName.KING)[0];
            King king = (King)p;
            return king.isUnderAttack(this);
        }

        public bool isStaleMated(Player p)
        {
            return false;
        }

        public List<Square> getAllSquaresInPath(Square sq1, Square sq2)
        {

            List<Square> results = new List<Square>();

            if(sq1.getX() == sq2.getX())
            {
                int low = Math.Min(sq1.getY(), sq2.getY());
                int hi = Math.Max(sq1.getY(), sq2.getY());
                for(int i = low+1; i<hi; i++)
                {
                    results.Add(getSquareAt(sq1.getX(), i));
                }

            } else if(sq1.getY() == sq2.getY())
            {

                int low = Math.Min(sq1.getX(), sq2.getX());
                int hi = Math.Max(sq1.getX(), sq2.getX());
                for (int i = low+1; i < hi; i++)
                {
                    results.Add(getSquareAt(i, sq1.getY()));
                }

            } else
            {
                Square x = sq1;
                int posX = x.getX();
                int posY = x.getY();
                

                while (x != sq2)
                {
                    
                    if(posX > sq2.getX())
                    {
                        posX--;
                    } else
                    {
                        posX++;
                    }

                    if (posY > sq2.getY())
                    {
                        posY--;
                    }
                    else
                    {
                        posY++;
                    }

                    x = this.getSquareAt(posX, posY);




                    if ( x!= sq2) results.Add(x);

                }

            }
            return results;
        }
    }
}

