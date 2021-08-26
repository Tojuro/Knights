using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knights.Code
{
    public struct Point
    {
        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class KnightPath
    {
        /// <summary>
        /// Constructor --- for the first point
        /// </summary>
        /// <param name="point"></param>
        public KnightPath(Point point)
        {
            History.Add(1, point);
        }

        /// <summary>
        /// Constructor --- for every other point
        /// </summary>
        /// <param name="point"></param>
        public KnightPath(Point move, KnightPath path)
        {
            ValidMoves = Helpers.CopyMoves(path.ValidMoves);
            History = Helpers.CopyMoves(path.History);
            History.Add(History.Keys.Max() + 1, move);
        }

        public Dictionary<int, Point> History = new Dictionary<int, Point>();

        public Point LastMove => History.OrderBy(_ => _.Key).Last().Value;

        public List<Point> ValidMoves = new List<Point>();
    }

    /// <summary>
    /// Manages the whole thing
    /// </summary>
    public class Processor
    {
        public ConcurrentBag<KnightPath> successPath = new ConcurrentBag<KnightPath>();
        private ConcurrentBag<KnightPath> nextSteps = new ConcurrentBag<KnightPath>();
        private ConcurrentBag<KnightPath> allMoves = new ConcurrentBag<KnightPath>();

        public void Go(Board board, Point startPoint)
        {
            nextSteps.Add(new KnightPath(startPoint));

            while (Cycle(board))
            {
                //Do
            }

            if (successPath.Any())
            {
                foreach (var step in successPath.First().History)
                {
                    board.Squares[step.Value.Y, step.Value.X].Text = step.Key.ToString();
                }    
            }

        }

        public bool Cycle(Board board)
        {
            ConcurrentBag<KnightPath> possibleSteps = new ConcurrentBag<KnightPath>();
            ConcurrentBag<Point> allMoves = new ConcurrentBag<Point>(nextSteps.SelectMany(_ => _.History.Values));

            Parallel.ForEach<KnightPath>(nextSteps.ToList(), k =>
            {
                foreach (var v in Helpers.ValidNextMoves(k, board))
                {
                    // Makes sure moves aren't doubled & a spot wasn't marked on a earlier cycle (going in circles)
                    if (possibleSteps.All(_ => !_.History.Values.Contains(v)) && 
                            !allMoves.Contains(v))
                    {
                        possibleSteps.Add(new KnightPath(v, k)); 
                    }
                }
            });

            nextSteps.Clear();
            nextSteps = possibleSteps;   

            if (nextSteps.Any(_ => board.Squares[_.LastMove.Y, _.LastMove.X].Success))
            {
                successPath.Add(nextSteps.First(_ => board.Squares[_.LastMove.Y, _.LastMove.X].Success));
                nextSteps = new ConcurrentBag<KnightPath>();
            }

            return nextSteps.Any();
        }

    }


    public static class Helpers
    {
        public static Dictionary<int, Point> CopyMoves(Dictionary<int, Point> from)
        {
            var to = new Dictionary<int, Point>();

            foreach (var pt in from)
            {
                to.Add(pt.Key, new Point(pt.Value.X, pt.Value.Y));
            }
            return to;
        }

        public static List<Point> CopyMoves(List<Point> from)
        {
            var to = new List<Point>();

            foreach (var pt in from)
            {
                to.Add(new Point(pt.X, pt.Y));
            }
            return to;
        }

        public static List<Point> ValidNextMoves(KnightPath path, Board board)
        {
            var start = path.LastMove;
            var possibleMoves = new List<Point>();

            possibleMoves.Add(new Point(start.X + 2, start.Y + 1));
            possibleMoves.Add(new Point(start.X + 2, start.Y - 1));

            possibleMoves.Add(new Point(start.X - 2, start.Y + 1));
            possibleMoves.Add(new Point(start.X - 2, start.Y - 1));
            
            possibleMoves.Add(new Point(start.X + 1, start.Y + 2));
            possibleMoves.Add(new Point(start.X - 1, start.Y + 2)); 
            
            possibleMoves.Add(new Point(start.X + 1, start.Y - 2));
            possibleMoves.Add(new Point(start.X - 1, start.Y - 2));

            return FindIntersectMoves(possibleMoves, board);
        }

        public static List<Point> FindIntersectMoves(List<Point> possible, Board board)
        {
            var validMoves = new List<Point>();
            foreach (var p in possible)
            {
                // range checks are redundant
                if (p.X >= 0 && 
                    p.X < 25 && 
                    p.Y >= 0 && 
                    p.Y < 15 && 
                    (board.Squares[p.Y, p.X].ValidMove || board.Squares[p.Y, p.X].Success))
                {
                    validMoves.Add(p);
                }
            }

            return validMoves;
        }

    }
}
