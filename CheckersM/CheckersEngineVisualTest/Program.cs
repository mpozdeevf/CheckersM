using System;
using CheckersEngine;

namespace CheckersEngineVisualTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DrawBoard();
            DisplayBitBoardFromBoard();
        }

        private static void DrawBoard()
        {
            //var board = new Board();
            //var boardState = board.GetBoardState();
            //for (var i = boardState.GetLength(0) - 1; i >= 0; i--)
            //{
            //    for (var j = 0; j < boardState.GetLength(1); j++)
            //    {
            //        Console.Write((int)boardState[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }

        private static void DisplayBitBoardFromBoard()
        {
            var board = new Board();
            Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().WhiteCheckers, 2));
            Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().WhiteCheckers, 2).Length);
            Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().BlackCheckers, 2));
            Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().BlackCheckers, 2).Length);
            Console.WriteLine(board.GetBitBoardFromBoard().WhiteCheckers);
            Console.WriteLine(board.GetBitBoardFromBoard().BlackCheckers);
            Console.WriteLine(board.GetBitBoardFromBoard().WhiteKings);
            Console.WriteLine(board.GetBitBoardFromBoard().BlackKings);
        }
    }
}
