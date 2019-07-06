using System;
using System.Collections.Generic;
using System.Linq;
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
            if (stringBoard != null)
            {
                ArrayBoard = GetArrayBoard(stringBoard);
            }
            else
            {
                stringBoard = "weweweweewewewewweweweweeeeeeeeeeeeeeeeeebebebebbebebebeebebebeb";
                ArrayBoard = GetArrayBoard(stringBoard);
            }
        }

        public abstract List<List<string>> GetPossiblePositions();
        protected abstract List<List<string>> GetCheckerPositions(List<List<string>> positions, int i, int j);
        protected abstract List<List<string>> GetKingPositions(List<List<string>> positions, int i, int j);

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

        protected List<List<string>> GetKingsPosition(List<List<string>> positions, int i, int j)
        {
            var position = new List<string>();
            var tempBoard = new CheckerType[BoardLength, BoardLength];
            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            while (i + 1 < BoardLength && j + 1 < BoardLength && tempBoard[i + 1, j + 1] == CheckerType.Empty)
            {
                tempBoard[i + 1, j + 1] = tempBoard[i, j];
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
                position = new List<string>();
                i++;
                j++;
            }

            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            while (i + 1 < BoardLength && j - 1 > -1 && tempBoard[i + 1, j - 1] == CheckerType.Empty)
            {
                tempBoard[i + 1, j - 1] = tempBoard[i, j];
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
                position = new List<string>();
                i++;
                j--;
            }

            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            while (i - 1 > -1 && j - 1 > -1 && tempBoard[i - 1, j - 1] == CheckerType.Empty)
            {
                tempBoard[i - 1, j - 1] = tempBoard[i, j];
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
                position = new List<string>();
                i--;
                j--;
            }

            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            while (i - 1 > -1 && j + 1 < BoardLength && tempBoard[i - 1, j + 1] == CheckerType.Empty)
            {
                tempBoard[i - 1, j + 1] = tempBoard[i, j];
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
                position = new List<string>();
                i++;
                j++;
            }

            return positions;
        }

        protected List<List<string>> Attack(List<List<string>> positions, CheckerType[] enemies)
        {
            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    if (enemies.Contains(ArrayBoard[i, j]) || ArrayBoard[i, j] == CheckerType.Empty) continue;
                    if (i + 1 < BoardLength && j + 1 < BoardLength && enemies.Contains(ArrayBoard[i + 1, j + 1])
                        && i + 2 < BoardLength && j + 2 < BoardLength && ArrayBoard[i + 2, j + 2] == CheckerType.Empty
                        || i + 1 < BoardLength && j - 1 > -1 && enemies.Contains(ArrayBoard[i + 1, j - 1])
                        && i + 2 < BoardLength && j - 2 > -1 && ArrayBoard[i + 2, j - 2] == CheckerType.Empty
                        || i - 1 > -1 && j - 1 > -1 && enemies.Contains(ArrayBoard[i - 1, j - 1])
                        && i - 2 > -1 && j - 2 > -1 && ArrayBoard[i - 2, j - 2] == CheckerType.Empty
                        || i - 1 > -1 && j + 1 < BoardLength && enemies.Contains(ArrayBoard[i - 1, j + 1])
                        && i - 2 > -1 && j + 2 < BoardLength && ArrayBoard[i - 2, j + 2] == CheckerType.Empty)
                    {
                        if (ArrayBoard[i, j] != CheckerType.BlackKing && ArrayBoard[i, j] != CheckerType.WhiteKing)
                        {
                            positions = GetCheckerAttackPositions(positions, enemies, i, j, new List<string>(),
                                ArrayBoard);
                        }
                        else
                        {
                            positions = GetKingAttackPositions(positions, enemies, i, j, new List<string>(),
                                ArrayBoard);
                        }
                    }
                }
            }

            return positions;
        }

        private List<List<string>> GetCheckerAttackPositions(List<List<string>> positions, CheckerType[] enemies,
            int i, int j, List<string> position, CheckerType[,] board)
        {
            var tempBoard = new CheckerType[BoardLength, BoardLength];
            var isKingCreated = false;
            Array.Copy(board, tempBoard, BoardSize);
            if (i + 1 < BoardLength && j + 1 < BoardLength && enemies.Contains(board[i + 1, j + 1])
                && i + 2 < BoardLength && j + 2 < BoardLength && board[i + 2, j + 2] == CheckerType.Empty)
            {
                if (tempBoard[i, j] == CheckerType.WhiteChecker && i + 2 == BoardLength - 1)
                {
                    isKingCreated = true;
                    tempBoard[i + 2, j + 2] = CheckerType.WhiteKing;
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i + 1, j + 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetKingAttackPositions(positions, enemies, i + 2, j + 2, position, tempBoard);
                }
                else
                {
                    tempBoard[i + 2, j + 2] = tempBoard[i, j];
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i + 1, j + 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetCheckerAttackPositions(positions, enemies, i + 2, j + 2, position, tempBoard);
                }
            }

            Array.Copy(board, tempBoard, BoardSize);
            if (i + 1 < BoardLength && j - 1 > -1 && enemies.Contains(board[i + 1, j - 1])
                && i + 2 < BoardLength && j - 2 > -1 && board[i + 2, j - 2] == CheckerType.Empty &&
                !isKingCreated)
            {
                if (tempBoard[i, j] == CheckerType.WhiteChecker && i + 2 == BoardLength - 1)
                {
                    isKingCreated = true;
                    tempBoard[i + 2, j - 2] = CheckerType.WhiteKing;
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i + 1, j - 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetKingAttackPositions(positions, enemies, i + 2, j - 2, position, tempBoard);
                }
                else
                {
                    tempBoard[i + 2, j - 2] = tempBoard[i, j];
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i + 1, j - 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetCheckerAttackPositions(positions, enemies, i + 2, j - 2, position, tempBoard);
                }
            }

            Array.Copy(board, tempBoard, BoardSize);
            if (i - 1 > -1 && j - 1 > -1 && enemies.Contains(board[i - 1, j - 1])
                && i - 2 > -1 && j - 2 > -1 && board[i - 2, j - 2] == CheckerType.Empty &&
                !isKingCreated)
            {
                if (tempBoard[i, j] == CheckerType.BlackChecker && i - 2 == 0)
                {
                    isKingCreated = true;
                    tempBoard[i - 2, j - 2] = CheckerType.BlackKing;
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i - 1, j - 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetKingAttackPositions(positions, enemies, i - 2, j - 2, position, tempBoard);
                }
                else
                {
                    tempBoard[i - 2, j - 2] = tempBoard[i, j];
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i - 1, j - 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetCheckerAttackPositions(positions, enemies, i - 2, j - 2, position, tempBoard);
                }
            }

            Array.Copy(board, tempBoard, BoardSize);
            if (i - 1 > -1 && j + 1 < BoardLength && enemies.Contains(board[i - 1, j + 1])
                && i - 2 > -1 && j + 2 < BoardLength && board[i - 2, j + 2] == CheckerType.Empty &&
                !isKingCreated)
            {
                if (tempBoard[i, j] == CheckerType.BlackChecker && i - 2 == 0)
                {
                    tempBoard[i - 2, j + 2] = CheckerType.BlackKing;
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i - 1, j + 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetKingAttackPositions(positions, enemies, i - 2, j + 2, position, tempBoard);
                }
                else
                {
                    tempBoard[i - 2, j + 2] = tempBoard[i, j];
                    tempBoard[i, j] = CheckerType.Empty;
                    tempBoard[i - 1, j + 1] = CheckerType.Empty;
                    position.Add(GetStringBoard(tempBoard));
                    positions = GetCheckerAttackPositions(positions, enemies, i - 2, j + 2, position, tempBoard);
                }
            }

            if (!positions.Contains(position))
            {
                positions.Add(position);
            }

            return positions;
        }

        private List<List<string>> GetKingAttackPositions(List<List<string>> positions, CheckerType[] enemies,
            int i, int j, List<string> position, CheckerType[,] board)
        {
            var i1 = i + 1;
            var j1 = j + 1;
            var tempBoard = new CheckerType[BoardLength, BoardLength];
            Array.Copy(board, tempBoard, BoardSize);
            while (i1 < BoardLength && j1 < BoardLength)
            {
                var variantsAfterFight = new List<Tuple<int, int>>();
                if (enemies.Contains(tempBoard[i1, j1]))
                {
                    var i2 = i1 + 1;
                    var j2 = j1 + 1;
                    while (i2 < 8 && j2 < 8 && tempBoard[i2, j2] == CheckerType.Empty)
                    {
                        variantsAfterFight.Add(Tuple.Create(i2, j2));
                        i2++;
                        j2++;
                    }

                    if (variantsAfterFight.Count > 0)
                    {
                        var current = tempBoard[i, j];
                        var enemy = tempBoard[i1, j1];
                        foreach (var (i3, j3) in variantsAfterFight)
                        {
                            tempBoard[i, j] = CheckerType.Empty;
                            tempBoard[i1, j1] = CheckerType.Empty;
                            tempBoard[i3, j3] = current;
                            position.Add(GetStringBoard(tempBoard));
                            positions = GetKingAttackPositions(positions, enemies, i3, j3, position, tempBoard);
                            tempBoard[i3, j3] = CheckerType.Empty;
                            tempBoard[i, j] = current;
                            tempBoard[i1, j1] = enemy;
                            position.RemoveAt(position.Count - 1);
                        }
                    }

                    break;
                }

                if(!enemies.Contains(tempBoard[i1, j1]) && tempBoard[i1, j1] != CheckerType.Empty)
                    break;

                i1++;
                j1++;
            }

            i1 = i - 1;
            j1 = j + 1;
            Array.Copy(board, tempBoard, BoardSize);
            while (i1 > -1 && j1 < BoardLength)
            {
                var variantsAfterFight = new List<Tuple<int, int>>();
                if (enemies.Contains(tempBoard[i1, j1]))
                {
                    var i2 = i1 - 1;
                    var j2 = j1 + 1;
                    while (i2 > -1 && j2 < 8 && tempBoard[i2, j2] == CheckerType.Empty)
                    {
                        variantsAfterFight.Add(Tuple.Create(i2, j2));
                        i2--;
                        j2++;
                    }

                    if (variantsAfterFight.Count > 0)
                    {
                        var current = tempBoard[i, j];
                        var enemy = tempBoard[i1, j1];
                        foreach (var (i3, j3) in variantsAfterFight)
                        {
                            tempBoard[i, j] = CheckerType.Empty;
                            tempBoard[i1, j1] = CheckerType.Empty;
                            tempBoard[i3, j3] = current;
                            position.Add(GetStringBoard(tempBoard));
                            positions = GetKingAttackPositions(positions, enemies, i, j, position, tempBoard);
                            tempBoard[i3, j3] = CheckerType.Empty;
                            tempBoard[i, j] = current;
                            tempBoard[i1, j1] = enemy;
                            position.RemoveAt(position.Count - 1);
                        }
                    }

                    break;
                }

                if (!enemies.Contains(tempBoard[i1, j1]) && tempBoard[i1, j1] != CheckerType.Empty)
                    break;

                i1--;
                j1++;
            }

            i1 = i - 1;
            j1 = j - 1;
            Array.Copy(board, tempBoard, BoardSize);
            while (i1 > -1 && j1 > -1)
            {
                var variantsAfterFight = new List<Tuple<int, int>>();
                if (enemies.Contains(tempBoard[i1, j1]))
                {
                    var i2 = i1 - 1;
                    var j2 = j1 - 1;
                    while (i2 > -1 && j2 > -1 && tempBoard[i2, j2] == CheckerType.Empty)
                    {
                        variantsAfterFight.Add(Tuple.Create(i2, j2));
                        i2--;
                        j2--;
                    }

                    if (variantsAfterFight.Count > 0)
                    {
                        var current = tempBoard[i, j];
                        var enemy = tempBoard[i1, j1];
                        foreach (var (i3, j3) in variantsAfterFight)
                        {
                            tempBoard[i, j] = CheckerType.Empty;
                            tempBoard[i1, j1] = CheckerType.Empty;
                            tempBoard[i3, j3] = current;
                            position.Add(GetStringBoard(tempBoard));
                            positions = GetKingAttackPositions(positions, enemies, i3, j3, position, tempBoard);
                            tempBoard[i3, j3] = CheckerType.Empty;
                            tempBoard[i, j] = current;
                            tempBoard[i1, j1] = enemy;
                            position.RemoveAt(position.Count - 1);
                        }
                    }

                    break;
                }

                if (!enemies.Contains(tempBoard[i1, j1]) && tempBoard[i1, j1] != CheckerType.Empty)
                    break;

                i1--;
                j1--;
            }

            i1 = i + 1;
            j1 = j - 1;
            Array.Copy(board, tempBoard, BoardSize);
            while (i1 < BoardLength && j1 > -1)
            {
                var variantsAfterFight = new List<Tuple<int, int>>();
                if (enemies.Contains(tempBoard[i1, j1]))
                {
                    var i2 = i1 + 1;
                    var j2 = j1 - 1;
                    while (i2 < 8 && j2 > -1 && tempBoard[i2, j2] == CheckerType.Empty)
                    {
                        variantsAfterFight.Add(Tuple.Create(i2, j2));
                        i2++;
                        j2--;
                    }

                    if (variantsAfterFight.Count > 0)
                    {
                        var current = tempBoard[i, j];
                        var enemy = tempBoard[i1, j1];
                        foreach (var (i3, j3) in variantsAfterFight)
                        {
                            tempBoard[i, j] = CheckerType.Empty;
                            tempBoard[i1, j1] = CheckerType.Empty;
                            tempBoard[i3, j3] = current;
                            position.Add(GetStringBoard(tempBoard));
                            positions = GetKingAttackPositions(positions, enemies, i3, j3, position, tempBoard);
                            tempBoard[i3, j3] = CheckerType.Empty;
                            tempBoard[i, j] = current;
                            tempBoard[i1, j1] = enemy;
                            position.RemoveAt(position.Count - 1);
                        }
                    }

                    break;
                }

                if (!enemies.Contains(tempBoard[i1, j1]) && tempBoard[i1, j1] != CheckerType.Empty)
                    break;

                i1++;
                j1--;
            }


            if (!positions.Contains(position))
            {
                positions.Add(position);
            }
            return positions;
        }
    }
}