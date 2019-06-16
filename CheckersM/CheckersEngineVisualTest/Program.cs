using System;
using CheckersEngine;

namespace CheckersEngineVisualTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //DrawBoard();
            //DisplayBitBoardFromBoard();

            //var arr1 = new int[,]
            //{
            //    {1, 2},
            //    {3, 4}
            //};
            //int[,] arr2 = new int[4, 4];
            //Array.Copy(arr1, arr2, 4);
            //for (var i = 0; i < arr1.GetLength(0); i++)
            //{
            //    for (var j = 0; j < arr1.GetLength(1); j++)
            //    {
            //        arr2[i, j] = 0;
            //        Console.Write(arr1[i,j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //for (var i = 0; i < arr1.GetLength(0); i++)
            //{
            //    for (var j = 0; j < arr1.GetLength(1); j++)
            //    {
            //        Console.Write(arr2[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            WatchPositions();
            //var n = -6172840797264674816;
            //var str = Convert.ToString(n, 2);
            //long r = 0;
            //for (var i = 0; i < str.Length; i++)
            //{
            //    if (str[i] == '1')
            //    {
            //        r += (long)Math.Pow(2, 64 - i - 1);
            //    }
            //}
            //Console.WriteLine(str);


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
            //var board = new Board();
            //Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().WhiteCheckers, 2));
            //Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().WhiteCheckers, 2).Length);
            //Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().BlackCheckers, 2));
            //Console.WriteLine(Convert.ToString(board.GetBitBoardFromBoard().BlackCheckers, 2).Length);
            //Console.WriteLine(board.GetBitBoardFromBoard().WhiteCheckers);
            //Console.WriteLine(board.GetBitBoardFromBoard().BlackCheckers);
            //Console.WriteLine(board.GetBitBoardFromBoard().WhiteKings);
            //Console.WriteLine(board.GetBitBoardFromBoard().BlackKings);
        }

        private static void WatchPositions()
        {
            var board = new Board(new BitBoard(0, Convert.ToInt64(new string('0', 9) + "1" + new string('0', 54), 2), 0,
                Convert.ToInt64(new string('0', 18) + "1" + new string('0', 15) + "101" + new string('0', 27), 2)));

            //var board = new Board(new BitBoard(0, 0, 0, 
            //    Convert.ToInt64(new string('0', 9) + "1" + new string('0', 54), 2)));

            //var board = new Board(new BitBoard(Convert.ToInt64(new string('0', 27) + "1" + new string('0', 36), 2), 0,
            //    Convert.ToInt64(new string('0', 36) + "101" + new string('0', 25), 2), 0));

            //var board = new Board();

            //var bitBoard = board.GetBitBoardFromBoard();
            //var str = Convert.ToString(bitBoard.WhiteCheckers, 2);
            //for (var i = 0; i < str.Length; i++)
            //{
            //    if (str[i] == '1')
            //    {

            //    }
            //}

            var boardState = board.GetBoardState();
            for (var i = boardState.GetLength(0) - 1; i >= 0; i--)
            {
                for (var j = 0; j < boardState.GetLength(1); j++)
                {
                    Console.Write((int)boardState[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            var positions = board.GetAllPossiblePositions(PlayerType.White);
            //var k = 0;
            //var rnd = new Random();
            //while (positions.Count > 0)
            //{
            //    var num = rnd.Next(0, positions.Count - 1);
            //    board = new Board(positions[num][positions[num].Count - 1]);
            //    boardState = board.GetBoardState();
            //    Console.WriteLine(k % 2 == 0 ? "W" : "B");
            //    for (var i = boardState.GetLength(0) - 1; i >= 0; i--)
            //    {
            //        for (var j = 0; j < boardState.GetLength(1); j++)
            //        {
            //            Console.Write((int)boardState[i, j] + " ");
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //    k++;
            //    positions = board.GetAllPossiblePositions(k % 2 == 0 ? PlayerType.White : PlayerType.Black);
            //}

            foreach (var position in positions)
            {
                board = new Board(position[position.Count - 1]);
                boardState = board.GetBoardState();
                for (var i = boardState.GetLength(0) - 1; i >= 0; i--)
                {
                    for (var j = 0; j < boardState.GetLength(1); j++)
                    {
                        Console.Write((int)boardState[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
