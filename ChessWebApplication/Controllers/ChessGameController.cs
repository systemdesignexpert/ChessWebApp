using ChessWebApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Chess_System_Design;

namespace ChessWebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ChessGameController : ControllerBase
{
    

    private readonly ILogger<ChessGameController> _logger;
    private CosmosDBClient dbClient;

    public ChessGameController(ILogger<ChessGameController> logger)
    {
        _logger = logger;
        dbClient = new CosmosDBClient();
    }

    [HttpPost]
    [Route("newGame")]
    public async Task<KeyValue> newGame()
    {
        ChessEngine chessEngine = new ChessEngine();

        KeyValue result = await dbClient.put(Guid.NewGuid().ToString(), new List<Move>());
        return result;

    }

    [HttpGet]
    [Route("getGame")]
    public async Task<List<Square>> getGame(string id)
    {
        ChessEngine chessEngine = new ChessEngine();
        KeyValue result = await dbClient.get(id);
        List<Move> moves = result.moves;
        chessEngine.runTillNow(moves);
        return chessEngine.getChessBoard().getAllSquares();

    }

    [HttpGet]
    [Route("getMoveList")]
    public async Task<List<Move>> getMoveList(string id)
    {
        KeyValue result = await dbClient.get(id);
        List<Move> moves = result.moves;
        return moves;

    }

    [HttpPost]
    [Route("move")]
    public async Task<MoveResult> move(string gameId, string startPos, string endPos)
    {
        ChessEngine chessEngine = new ChessEngine();
        KeyValue result = await dbClient.get(gameId);
        List<Move> moves = result.moves;
        int move_number = moves.Count();

        chessEngine.runTillNow(moves);

        Player player = chessEngine.getBlackPlayer();
        if(move_number % 2 == 0)
        {
            player = chessEngine.getWhitePlayer();
        }

        ChessBoard board = chessEngine.getChessBoard();

        string s1 = startPos;
        String s2 = endPos;
        
        bool success = player.move(board.getSquareAtPos(s1), board.getSquareAtPos(s2));
        bool isWhiteWinner = board.isGameOver(chessEngine.getBlackPlayer());
        bool isBlackWinner = board.isGameOver(chessEngine.getWhitePlayer());
        MoveResult response = new MoveResult()
        {
            isSuccess = success,
            isGameOver = isWhiteWinner || isBlackWinner,
            isWhiteWinner = isWhiteWinner,
            listOfSquares = board.getAllSquares()
        };

        if(success)
        {
            move_number++;
            String piece = board.getSquareAtPos(s2).getPiece().getName().ToString();
            moves.Add(new Move()
            {
                startPos = startPos,
                endPos = endPos,
                number = move_number,
                piece = piece

            });

            await dbClient.put(gameId, moves);
        }

        return response;

    }


}

