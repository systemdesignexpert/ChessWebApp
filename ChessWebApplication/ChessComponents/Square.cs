using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Chess_System_Design
{
    public class Square
    {
        private int x;

        private int y;

        private Piece p;

        [JsonPropertyName("x")]
        public int X {
            get { return x; }
            set { x = value ; }
        }

        [JsonPropertyName("y")]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        [JsonPropertyName("piece")]
        public Piece Piece
        {
            get { return p; }
            set { p = value; }
        }




        public Square(int _x, int _y, Piece _p = null)
        {
            x = _x;
            y = _y;
            p = _p;
        }
        public int getX()
        {
            return x;

        }

        public int getY()
        {
            return y;

        }

        public Piece getPiece()
        {
            return p;
        }

        public void setPiece(Piece? _p)
        {
            p = _p;
        }

        public string getPos()
        {
            char first = (char)((int)'a' + x);
            char second = (char)((int)'1' + y);
            String s = "";
            s += first;
            s += second;
            return s;
        }


    }
}