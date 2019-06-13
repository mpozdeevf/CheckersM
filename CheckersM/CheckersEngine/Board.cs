using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CheckersEngine
{
    public class Board
    {
        private const int BoardLength = 8;
        private const int BoardSize = BoardLength * BoardLength;

        private readonly CellType[,] _boardState = new CellType[BoardLength, BoardLength];

        public Board()
        {
            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    _boardState[i, j] = CellType.Empty;
                }
            }
            ConvertBitBoardToBoard(new BitBoard());
        }

        public Board(BitBoard bitBoard)
        {
            ConvertBitBoardToBoard(bitBoard);
        }

        //public CellType[,] GetBoardState() => _boardState; //for test

        public BitBoard GetBitBoardFromBoard()
        {
            var whiteCheckers = new StringBuilder();
            var blackCheckers = new StringBuilder();
            var whiteKings = new StringBuilder();
            var blackKings = new StringBuilder();

            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    whiteCheckers.Append(_boardState[i, j] == CellType.WhiteChecker ? '1' : '0');

                    blackCheckers.Append(_boardState[i, j] == CellType.BlackChecker ? '1' : '0');

                    whiteKings.Append(_boardState[i, j] == CellType.WhiteKing ? '1' : '0');

                    blackKings.Append(_boardState[i, j] == CellType.BlackKing ? '1' : '0');
                }
            }

            return new BitBoard(Convert.ToInt64(whiteCheckers.ToString(), 2), 
                Convert.ToInt64(whiteKings.ToString(), 2),
                Convert.ToInt64(blackCheckers.ToString(), 2), 
                Convert.ToInt64(blackKings.ToString(), 2));
        }

        private void ConvertBitBoardToBoard(BitBoard bitBoard)
        {
            var whiteCheckers = Convert.ToString(bitBoard.WhiteCheckers, 2);
            var blackCheckers = Convert.ToString(bitBoard.BlackCheckers, 2);
            var whiteKings = Convert.ToString(bitBoard.WhiteKings, 2);
            var blackKings = Convert.ToString(bitBoard.BlackKings, 2);

            whiteCheckers = new string('0', BoardSize - whiteCheckers.Length) + whiteCheckers;
            blackCheckers = new string('0', BoardSize - blackCheckers.Length) + blackCheckers;
            whiteKings = new string('0', BoardSize - whiteKings.Length) + whiteKings;
            blackKings = new string('0', BoardSize - blackKings.Length) + blackKings;

            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    if (whiteCheckers[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.WhiteChecker;
                    if (blackCheckers[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.BlackChecker;
                    if (whiteKings[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.WhiteKing;
                    if (blackKings[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.BlackKing;
                }
            }
        }
    }
}
