using System;
using System.Collections.Generic;

namespace CheckersEngine
{
    public class BlackCheckersEngine : Engine
    {
        private readonly CheckerType[] _enemies = {CheckerType.WhiteChecker, CheckerType.WhiteKing};

        public BlackCheckersEngine(string stringBoard) : base(stringBoard)
        {
        }

        public override List<List<string>> GetPossiblePositions()
        {
            var positions = new List<List<string>>();
            positions = Attack(positions, _enemies);
            if (positions.Count > 0) return positions;
            
            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    if (ArrayBoard[i, j] == CheckerType.BlackChecker)
                    {
                        positions = GetCheckerPositions(positions, i, j);
                    }

                    if (ArrayBoard[i, j] == CheckerType.BlackKing)
                    {
                        positions = GetKingPositions(positions, i, j);
                    }
                }
            }

            return positions;
        }

        protected override List<List<string>> GetCheckerPositions(List<List<string>> positions, int i, int j)
        {
            var position = new List<string>();
            var tempBoard = new CheckerType[BoardLength, BoardLength];
            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            if (i - 1 > -1 && j - 1 > -1 && tempBoard[i - 1, j - 1] == CheckerType.Empty)
            {
                if (i - 1 == 0)
                {
                    tempBoard[i - 1, j - 1] = CheckerType.BlackKing;
                }
                else
                {
                    tempBoard[i - 1, j - 1] = tempBoard[i, j];
                }
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
            }

            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            position = new List<string>();
            if (i - 1 > -1 && j + 1 < BoardLength && tempBoard[i - 1, j + 1] == CheckerType.Empty)
            {
                if (i - 1 == 0)
                {
                    tempBoard[i - 1, j + 1] = CheckerType.BlackKing;
                }
                else
                {
                    tempBoard[i - 1, j + 1] = tempBoard[i, j];
                }
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
            }

            return positions;
        }

        protected override List<List<string>> GetKingPositions(List<List<string>> positions, int i, int j)
        {
            positions = GetKingsPosition(positions, i, j);

            return positions;
        }
    }
}