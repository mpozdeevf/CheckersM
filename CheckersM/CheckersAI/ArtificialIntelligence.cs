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
            2, 2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 4,
            5, 5, 5, 5, 5, 5, 5, 5,
            6, 6, 6, 6, 6, 6, 6, 6,
            7, 7, 7, 7, 7, 7, 7, 7,
            8, 8, 8, 8, 8, 8, 8, 8
        };

        private static readonly int[] BlackWeights =
        {
            8, 8, 8, 8, 8, 8, 8, 8,
            7, 7, 7, 7, 7, 7, 7, 7,
            6, 6, 6, 6, 6, 6, 6, 6,
            5, 5, 5, 5, 5, 5, 5, 5,
            4, 4, 4, 4, 4, 4, 4, 4,
            3, 3, 3, 3, 3, 3, 3, 3,
            2, 2, 2, 2, 2, 2, 2, 2,
            1, 1, 1, 1, 1, 1, 1, 1
        };

        private static readonly int[] KingWeights =
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