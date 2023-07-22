using System;
namespace Chess_System_Design
{
	public class MoveResult
	{
		public MoveResult()
		{
			isSuccess = false;
			isGameOver = false;
			isWhiteWinner = false;
			listOfSquares = new List<Square>();
		}

		public bool isSuccess { get; set; }
		public bool isGameOver { get; set; }
		public bool isWhiteWinner { get; set; }
		public List<Square> listOfSquares { get; set; }
	}
}

