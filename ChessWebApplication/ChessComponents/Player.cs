using Chess_System_Design;

public class Player
{
    private Color color;
    private List<Piece> pieces;
    private ChessBoard board;

    public Player(Color color, ChessBoard _board)
    {
        this.color = color;
        this.board = _board;
        this.createPieces(color);
        this.initializePieces(board);
    }

    public bool move(Square sq_org, Square sq_final)
    {
        if (sq_org.getPiece() == null) return false;

        if(sq_org.getPiece().getColor() == this.color)
        {

            return sq_org.getPiece().move(sq_final, board);

        } else
        {
            return false;
        }
    }

    public List<Piece> getAllPieces()
    {
        return pieces;
    }

    public List<Piece> getPiecesWithName(PieceName name)
    {
        List<Piece> result = new List<Piece>();
        foreach(Piece p in pieces)
        {
            if(p.getName() == name)
            {
                result.Add(p);
            }
        }
        return result;
    }

    public Piece getPieceAt(string pos)
    {
        Square sq = board.getSquareAtPos(pos);
        return sq.getPiece();
    }



    private void createPieces(Color color)
    {
        
        this.pieces = new List<Piece>();

        PieceFactory pieceFactory = new PieceFactory();
        pieces.Add(pieceFactory.createPiece(PieceName.ROOK, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.ROOK, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.KING, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.QUEEN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.KNIGHT, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.KNIGHT, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.BISHOP, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.BISHOP, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));
        pieces.Add(pieceFactory.createPiece(PieceName.PAWN, this.color));

    }


    private void initializePieces(ChessBoard board)
    {
        
        foreach(var piece in this.pieces)
        {
            foreach (var pos in piece.getInitialPositions())
            {
                if(board.getSquareAtPos(pos).getPiece() == null)
                {
                    board.getSquareAtPos(pos).setPiece(piece);
                    break;
                }
            }
        }
    }

}