using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Chess_System_Design
{
    public abstract class Piece
    {
        private Color color;

        private PieceName name;

        [JsonPropertyName("color")]
        public string ColorName {
            get
            {
                return color.ToString();
            }
        }

        [JsonPropertyName("name")]
        public string NameOfPiece
        {
            get
            {
                return name.ToString();
            }
        }

        public Color getColor()
        {
            return color;
        }

        public void setColor(Color _color)
        {
            this.color = _color;
        }

        public PieceName getName()
        {
            return name;
        }

        public void setName(PieceName _name)
        {
            this.name = _name;
        }

        public int getPosX(ChessBoard board)
        {
            return this.getSquare(board).getX();
        }

        public int getPosY(ChessBoard board)
        {
            return this.getSquare(board).getY();
        }

        public Square getSquare(ChessBoard board)
        {
            return board.findSquareForPiece(this);
        }

        public string getPos(ChessBoard board)
        {
            return this.getSquare(board).getPos();
        }

        public Player getPlayer()
        {
            Color color = this.getColor();
            return ChessEngine.playerMap[color];
        }

        public Player getOpponentPlayer()
        {
            Color color = this.getColor();
            if (color == Color.WHITE) color = Color.BLACK;
            else color = Color.WHITE;

            return ChessEngine.playerMap[color];
        }

        

        public bool move(Square sq, ChessBoard board)
        {
            var validSquares = this.getAllLegalSquares(board, this.getName() == PieceName.KNIGHT);
            if(validSquares.Contains(sq))  {
                if (simulateMove(sq, board))
                {
                    return true;
                }
            }

            return false;
        }

        public bool simulateMove(Square sq, ChessBoard board)
        {

            Player player = this.getPlayer();

            //do move
            Piece captured = sq.getPiece();
            Square old_sq = this.getSquare(board);
            sq.setPiece(this);
            old_sq.setPiece(null);
            Console.WriteLine(old_sq.getPos() + " " + old_sq.getPiece() + 
                " " + sq.getPos() + " " + sq.getPiece().getName().ToString());


            if (board.isInCheck(player))
            {
                //revert move
                sq.setPiece(captured);
                old_sq.setPiece(this);
                return false;

            }  else  {

                // success
                return true;
            }

        }
        
        public List<Square> getAllLegalSquares(ChessBoard board, bool allowJump)
        {
            var squares = this.getAllValidSquares(board);
            var listOfMoves = new List<Square>();
            foreach(Square sq in squares)  {
                if(sq.getPiece() == null || (sq.getPiece().getColor() != this.getColor()))
                {
                    listOfMoves.Add(sq);
                }
            }

            

            if (!allowJump) {

                var result = new List<Square>();
                foreach (Square sq in listOfMoves)
                {
                    if(!this.isPieceInPath(board, sq))
                    {
                        result.Add(sq);
                    }
                }
                return result;
            }

            return listOfMoves;
        }

        public bool isPieceInPath(ChessBoard board, Square sq)
        {
            List<Square> squares = board.getAllSquaresInPath(sq, this.getSquare(board));
            foreach(var _sq in squares)
            {
                if(_sq.getPiece() != null)
                {
                    return true;
                }
            }

            return false;
        }


        abstract public List<Square> getAllValidSquares(ChessBoard board);
        abstract public List<String> getInitialPositions();


    }
}