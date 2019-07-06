using System;
using System.Linq;
using CheckersEngine;

namespace CheckersEngineVisualTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //var engine = new WhiteCheckersEngine();
/*            var engine = new BlackCheckersEngine();
            var stringBoard = engine.GetCurrentPosition();
            WriteBoard(stringBoard);

            var positions = engine.GetPossiblePositions();
            foreach (var position in positions)
            {
                Console.WriteLine();
                WriteBoard(position.Last());
            }*/
            Fight();
        }

        private static void TestKings()
        {
            
        }
        
        private static void Fight()
        {
            string board = null;
            while (true)
            {
                board = WhiteTurn(board);
                if (board == null) break;
                Console.WriteLine("White turn");
                WriteBoard(board);
                Console.WriteLine();
                
                board = BlackTurn(board);
                if (board == null) break;
                Console.WriteLine("Black turn");
                WriteBoard(board);
                Console.WriteLine();
            }
        }

        private static string WhiteTurn(string board)
        {
            var engine = new WhiteCheckersEngine(board);
            var positions = engine.GetPossiblePositions();
            var rnd = new Random();
            return positions.Count > 0 ? positions[rnd.Next(0, positions.Count - 1)].Last() : null;
        }

        private static string BlackTurn(string board = null)
        {
            var engine = new BlackCheckersEngine(board);
            var positions = engine.GetPossiblePositions();
            var rnd = new Random();
            return positions.Count > 0 ? positions[rnd.Next(0, positions.Count - 1)].Last() : null;
        }

        private static void WriteBoard(string stringBoard)
        {
            for (var i = 56; i >= 0; i -= 8)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (stringBoard[i + j] == 'e')
                    {
                        Console.Write("*" + " ");
                    }
                    else
                    {
                        Console.Write(stringBoard[i + j] + " ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}