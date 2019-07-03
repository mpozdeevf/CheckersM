using System;
using System.Collections.Generic;

namespace CheckersEngine
{
    public class WhiteCheckersEngine : Engine
    {
        private CheckerType[] _enemies = {CheckerType.BlackChecker, CheckerType.BlackKing};

        public WhiteCheckersEngine(string stringBoard) : base(stringBoard)
        {
        }

        public WhiteCheckersEngine()
        {
        }

        public override List<List<string>> GetPossiblePositions()
        {
            var positions = new List<List<string>>();
            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    if (ArrayBoard[i, j] == CheckerType.WhiteChecker)
                    {
                        positions = GetCheckerPositions(positions, i, j);
                    }
                }
            }

            return positions;
        }

        private List<List<string>> GetCheckerPositions(List<List<string>> positions, int i, int j)
        {
            var position = new List<string>();
            var tempBoard = new CheckerType[BoardLength, BoardLength];
            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            if (i + 1 < BoardLength && j + 1 < BoardLength && tempBoard[i, j] == CheckerType.Empty)
            {
                tempBoard[i + 1, j + 1] = tempBoard[i, j];
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
            }

            Array.Copy(ArrayBoard, tempBoard, BoardSize);
            if (i + 1 < BoardLength && j - 1 > -1 && tempBoard[i, j] == CheckerType.Empty)
            {
                tempBoard[i + 1, j + 1] = tempBoard[i, j];
                tempBoard[i, j] = CheckerType.Empty;
                position.Add(GetStringBoard(tempBoard));
                positions.Add(position);
            }
            
            return positions;
        }
    }
}