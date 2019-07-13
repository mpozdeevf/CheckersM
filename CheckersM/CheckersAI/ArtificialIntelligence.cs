using System;
using System.Collections.Generic;
using System.Text;
using CheckersEngine;

namespace CheckersAI
{
    public static class ArtificialIntelligence
    {
        private static readonly int[] WhitePoints =
        {
            1, 1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 3, 3,
            6, 6, 4, 4, 4, 4, 5, 5,
            9, 9, 7, 7, 8, 8, 10, 10,
            14, 14, 12, 12, 11, 11, 13, 13,
            16, 16, 15, 15, 15, 15, 17, 17,
            19, 19, 18, 18, 18, 18, 18, 18,
            20, 20, 20, 20, 20, 20, 20, 20
        };

        private static readonly int[] BlackPoints =
        {
            20, 20, 20, 20, 20, 20, 20, 20,
            18, 18, 18, 18, 18, 18, 19, 19,
            17, 17, 15, 15, 15, 15, 16, 16,
            13, 13, 11, 11, 12, 12, 14, 14,
            10, 10, 8, 8, 7, 7, 9, 9,
            5, 5, 4, 4, 4, 4, 6, 6,
            3, 3, 2, 2, 2, 2, 2, 2,
            1, 1, 1, 1, 1, 1, 1, 1
        };

        private static readonly int[] KingPoints =
        {
            21, 21, 21, 21, 21, 21, 21, 21,
            21, 22, 22, 22, 22, 22, 22, 21,
            21, 22, 23, 23, 23, 23, 22, 21,
            21, 22, 23, 24, 24, 23, 22, 21,
            21, 22, 23, 24, 24, 23, 22, 21,
            21, 22, 23, 23, 23, 23, 22, 21,
            21, 22, 22, 22, 22, 22, 22, 21,
            21, 21, 21, 21, 21, 21, 21, 21
        };

        private static readonly int[] TableWeights =
        {
            4, 4, 4, 4, 4, 4, 4, 4,
            3, 3, 3, 3, 3, 3, 4, 4,
            4, 4, 2, 2, 2, 2, 3, 3,
            3, 3, 1, 1, 2, 2, 4, 4,
            4, 4, 2, 2, 1, 1, 3, 3,
            3, 3, 2, 2, 2, 2, 4, 4,
            4, 4, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 4
        };

        public static List<string> GetNextMove(List<List<string>> positions)
        {
            if (positions == null || positions.Count == 0) return null;
            var greatPos = new List<List<string>>();
            var maxPos = int.MinValue;
            foreach (var position in positions)
            {
                var max = int.MinValue;
                for (var i = 0; i < position[position.Count - 1].Length; i++)
                {
                    if (position[position.Count - 1][i] == 'b') max += BlackPoints[i] * 2;
                    if (position[position.Count - 1][i] == 'B') max += KingPoints[i] * 2;
                    if (position[position.Count - 1][i] == 'w') max -= WhitePoints[i];
                    if (position[position.Count - 1][i] == 'W') max -= KingPoints[i];
                }

                max -= WhiteMax(position[position.Count - 1]);
                if (max > maxPos)
                {
                    greatPos = new List<List<string>> {position};
                    maxPos = max;
                }

                if (max == maxPos)
                {
                    greatPos.Add(position);
                }
            }

            var rnd = new Random();
            return greatPos[rnd.Next(0, greatPos.Count - 1)];
        }

        private static int WhiteMax(string board)
        {
            var engine = new WhiteCheckersEngine(board);
            var positions = engine.GetPossiblePositions();
            if (positions == null || positions.Count == 0) return 0;
            var maxPos = int.MinValue;
            foreach (var position in positions)
            {
                var max = int.MinValue;
                for (var i = 0; i < position[position.Count - 1].Length; i++)
                {
                    if (position[position.Count - 1][i] == 'b') max -= BlackPoints[i] * 2;
                    if (position[position.Count - 1][i] == 'B') max -= KingPoints[i] * 2;
                    if (position[position.Count - 1][i] == 'w') max += WhitePoints[i];
                    if (position[position.Count - 1][i] == 'W') max += KingPoints[i];
                }

                if (max > maxPos) maxPos = max;
            }

            return maxPos;
        }
    }
}