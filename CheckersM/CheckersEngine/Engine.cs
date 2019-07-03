using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersEngine
{
    public abstract class Engine
    {
        private const char WhiteChecker = 'w';
        private const char WhiteKing = 'W';
        private const char BlackChecker = 'b';
        private const char BlackKing = 'B';
        private const char Empty = 'e';

        protected CheckerType[,] ArrayBoard { get; set; }
        
        protected const int BoardLength = 8;
        protected const int BoardSize = BoardLength * BoardLength;

        protected Engine(string stringBoard)
        {
            ArrayBoard = GetArrayBoard(stringBoard);
        }

        protected Engine()
        {
            const string stringBoard = "weweweweewewewewweweweweeeeeeeeeeeeeeeeeebebebebbebebebeebebebeb";
            ArrayBoard = GetArrayBoard(stringBoard);
        }

        public abstract List<List<string>> GetPossiblePositions();

        public string GetCurrentPosition() => GetStringBoard(ArrayBoard);

        private static CheckerType[,] GetArrayBoard(string stringBoard)
        {
            var arrayBoard = new CheckerType[BoardLength, BoardLength];
            for (var i = 0; i < BoardSize; i++)
            {
                switch (stringBoard[i])
                {
                    case WhiteChecker:
                        arrayBoard[i / 8, i % 8] = CheckerType.WhiteChecker;
                        break;
                    case WhiteKing:
                        arrayBoard[i / 8, i % 8] = CheckerType.WhiteKing;
                        break;
                    case BlackChecker:
                        arrayBoard[i / 8, i % 8] = CheckerType.BlackChecker;
                        break;
                    case BlackKing:
                        arrayBoard[i / 8, i % 8] = CheckerType.BlackKing;
                        break;
                    case Empty:
                        arrayBoard[i / 8, i % 8] = CheckerType.Empty;
                        break;
                    default:
                        throw new Exception("Wrong board");
                }
            }

            return arrayBoard;
        }

        protected string GetStringBoard(CheckerType[,] arrayBoard)
        {
            var stringBoard = new StringBuilder(BoardSize);
            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    switch (arrayBoard[i, j])
                    {
                        case CheckerType.WhiteChecker:
                            stringBoard.Append(WhiteChecker);
                            break;
                        case CheckerType.WhiteKing:
                            stringBoard.Append(WhiteKing);
                            break;
                        case CheckerType.BlackChecker:
                            stringBoard.Append(BlackChecker);
                            break;
                        case CheckerType.BlackKing:
                            stringBoard.Append(BlackKing);
                            break;
                        case CheckerType.Empty:
                            stringBoard.Append(Empty);
                            break;
                        default:
                            throw new Exception("Wrong board");
                    }
                }
            }

            return stringBoard.ToString();
        }
    }
}