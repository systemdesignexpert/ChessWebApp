using System;
using Chess_System_Design;

namespace ChessWebApplication.Database
{
	public class KeyValue
	{
        public string? id { get; set; }
        public List<Move> moves { get; set; }

        public KeyValue(string _id, List<Move> _moves)
		{
			this.id = _id;
			this.moves = _moves;
		}
		
    }
}

