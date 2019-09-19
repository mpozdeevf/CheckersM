using System;
using System.Collections.Generic;
using System.Text;
using CheckersEngine;

namespace CheckersAI
{
    public static class ArtificialIntelligence
    {
        private const int Depth = 2;

        private static readonly int[] WhiteWeights =
        {
            1, 1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 3, 3,
            5, 5, 3, 3, 3, 3, 4, 4,
            6, 6, 4, 4, 4, 4, 7, 7,
            9, 9, 6, 6, 6, 6, 8, 8,
            10, 10, 8, 8, 8, 8, 11, 11,
            13, 13, 10, 10, 10, 10, 12, 12,
            14, 14, 14, 14, 14, 14, 14, 14
        };

        private static readonly int[] BlackWeights =
        {
            14, 14, 14, 14, 14, 14, 14, 14,
            12, 12, 10, 10, 10, 10, 13, 13,
            11, 11, 8, 8, 8, 8, 10, 10,
            8, 8, 6, 6, 6, 6, 9, 9,
            7, 7, 4, 4, 4, 4, 6, 6,
            4, 4, 3, 3, 3, 3, 5, 5,
            2, 2, 2, 2, 2, 2, 3, 3,
            1, 1, 1, 1, 1, 1, 1, 1
        };

        private static readonly int[] KingWeights =
        {
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
            15, 15, 15, 15, 15, 15, 15, 15,
        };

        public static List<string> GetNextMove(List<List<string>> positions)
        {
            if (positions == null || positions.Count == 0) return null;
            var greatPos = new List<List<string>>();
            var maxPos = int.MinValue;
            foreach (var position in positions)
            {
                var max = 0;
                for (var i = 0; i < position[position.Count - 1].Length; i++)
                {
                    if (position[position.Count - 1][i] == 'b') max += BlackWeights[i];
                    if (position[position.Count - 1][i] == 'B') max += KingWeights[i];
                    if (position[position.Count - 1][i] == 'w') max -= WhiteWeights[i];
                    if (position[position.Count - 1][i] == 'W') max -= KingWeights[i];
                }

                max -= WhiteMax(position[position.Count - 1], 0);
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

        private static int BlackMax(string board, int currentDepth)
        {
            var engine = new BlackCheckersEngine(board);
            var positions = engine.GetPossiblePositions();
            if (positions == null || positions.Count == 0) return 0;
            var maxPos = int.MinValue;
            foreach (var position in positions)
            {
                var max = 0;
                for (var i = 0; i < position[position.Count - 1].Length; i++)
                {
                    if (position[position.Count - 1][i] == 'b') max += BlackWeights[i];
                    if (position[position.Count - 1][i] == 'B') max += KingWeights[i];
                    if (position[position.Count - 1][i] == 'w') max -= WhiteWeights[i];
                    if (position[position.Count - 1][i] == 'W') max -= KingWeights[i];
                }

                max -= WhiteMax(position[position.Count - 1], currentDepth);
                if (max > maxPos) maxPos = max;
            }

            return maxPos;
        }

        private static int WhiteMax(string board, int currentDepth)
        {
            var engine = new WhiteCheckersEngine(board);
            var positions = engine.GetPossiblePositions();
            if (positions == null || positions.Count == 0) return 0;
            var maxPos = int.MinValue;
            foreach (var position in positions)
            {
                var max = 0;
                for (var i = 0; i < position[position.Count - 1].Length; i++)
                {
                    if (position[position.Count - 1][i] == 'b') max -= BlackWeights[i];
                    if (position[position.Count - 1][i] == 'B') max -= KingWeights[i];
                    if (position[position.Count - 1][i] == 'w') max += WhiteWeights[i];
                    if (position[position.Count - 1][i] == 'W') max += KingWeights[i];
                }

                if (currentDepth < Depth)
                {
                    max -= BlackMax(position[position.Count - 1], currentDepth + 1);
                }

                if (max > maxPos) maxPos = max;
            }

            return maxPos;
        }
    }
}