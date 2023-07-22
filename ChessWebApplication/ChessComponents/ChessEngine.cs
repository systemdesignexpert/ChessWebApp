using Chess_System_Design;

//Console.WriteLine("Hello, World!");
//ChessEngine ce = new ChessEngine();
//ce.run();

public class ChessEngine
{

    public static Dictionary<Color, Player> playerMap;
    private ChessBoard board;
    private Player whitePlayer;
    private Player blackPlayer;

    public ChessEngine()
    {
        board = new ChessBoard();
        whitePlayer = new Player(Color.WHITE, board);
        blackPlayer = new Player(Color.BLACK, board);
        playerMap = new Dictionary<Color, Player>();

        playerMap.Add(Color.WHITE, whitePlayer);
        playerMap.Add(Color.BLACK, blackPlayer);
    }

    public ChessBoard getChessBoard()
    {
        return this.board;
    }

    public Player getWhitePlayer()
    {
        return this.whitePlayer;
    }

    public Player getBlackPlayer()
    {
        return this.blackPlayer;
    }

    public void run()
    {

        while (true)
        {

            board.displayBoard();
            if (!board.isGameOver(whitePlayer))
            {
                bool success = false;
                while (!success)
                {
                    Console.WriteLine("WHITE PLAYER to MOVE: ");
                    string s1 = Console.ReadLine();
                    String s2 = Console.ReadLine();
                    success = whitePlayer.move(board.getSquareAtPos(s1), board.getSquareAtPos(s2));
                }

            } else
            {
                Console.WriteLine("BLACK WINS");
                return;
            }

            board.displayBoard();
            if (!board.isGameOver(blackPlayer))
            {
                bool success = false;
                while (!success)
                {
                    Console.WriteLine("BLACK PLAYER to MOVE: ");
                    string s1 = Console.ReadLine();
                    String s2 = Console.ReadLine();
                    success = blackPlayer.move(board.getSquareAtPos(s1), board.getSquareAtPos(s2));
                }
            } else
            {
                Console.WriteLine("WHITE WINS");
                return;
            }
        }

        return;

    }

    public void runTillNow(List<Move> moves)
    {
        int moveSize = moves.Count();
        int i = 0;
        while(i < moveSize)
        {
            Player player = this.getBlackPlayer();
            if(i % 2 == 0)
            {
                player = this.getWhitePlayer();
            }
            board.displayBoard();
            if (!board.isGameOver(whitePlayer))
            {
                bool success = false;
                while (!success)
                {
                    Console.WriteLine("WHITE PLAYER to MOVE: ");
                    string s1 = moves[i].startPos;
                    String s2 = moves[i].endPos;
                    success = player.move(board.getSquareAtPos(s1), board.getSquareAtPos(s2));
                }

            }
            else
            {
                Console.WriteLine("BLACK WINS");
                return;
            }

            i++;

        }

        return;

    }











}

