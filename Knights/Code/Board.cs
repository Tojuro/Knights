using System.Collections.Generic;

namespace Knights.Code
{
    public enum SquareColor
    {
        Black,
        White,
        Brown,
        Blue
    }

    public class Square
    {
        public SquareColor Color;
        public string StringColor
        {
            get
            {
                if (Color == SquareColor.Blue)
                    return "#0000ff";
                else if (Color == SquareColor.White)
                    return "#ffffff";
                else if (Color == SquareColor.Brown)
                    return "#ff0000";
                return "#cccccc";
            }
        }

        public string Text { get; set; }

        public bool ValidMove
        {
            get
            {
                return Color == SquareColor.Brown || Color == SquareColor.White;
            }
        }

        public bool Success
        {
            get
            {
                return Color == SquareColor.Blue;
            }
        }
          
    }


    public class Board
    {
        public Square[,] Squares = new Square[15, 25];

        public Board()
        {
            int i = 0;
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    Squares[y, x] = new Square { Color = (SquareColor)BoardData.Data[i] };
                    i++;
                }
            }
        }
    }


    public static class BoardData
    {
        public static int[] Data = new int[]
            { 
                1, 0, 0, 0, 0, 2, 0, 0, 1, 0, 1, 2, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1,
                0, 1, 2, 1, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0, 0,
                1, 0, 0, 0, 1, 0, 0, 2, 0, 2, 1, 0, 0, 2, 0, 2, 0, 0, 1, 0, 0, 2, 0, 2, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0, 0, 1, 2, 0, 0, 0, 0,
                0, 2, 0, 0, 1, 0, 1, 0, 0, 0, 0, 2, 0, 0, 1, 0, 0, 2, 0, 0, 0, 2, 1, 0, 1,
                0, 1, 0, 0, 0, 0, 0, 1, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2, 1, 0,
                0, 0, 1, 2, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0,
                3, 0, 0, 0, 2, 0, 0, 1, 0, 0, 2, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 3,
                0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 1, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1, 2,
                0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 1, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0,
                2, 0, 0, 1, 0, 0, 2, 0, 2, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 1, 2,
                0, 0, 1, 2, 1, 0, 0, 0, 1, 0, 1, 0, 1, 2, 0, 0, 1, 2, 1, 2, 1, 0, 0, 2, 0,
                2, 1, 2, 1, 0, 1, 0, 0, 0, 0, 0, 1, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2,
                0, 0, 1, 2, 0, 2, 0, 2, 0, 2, 1, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0, 1, 1, 0, 0,
            };
    }

}
