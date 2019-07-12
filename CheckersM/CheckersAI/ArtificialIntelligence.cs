using System;
using System.Collections.Generic;
using System.Text;
using CheckersEngine;

namespace CheckersAI
{
    public static class ArtificialIntelligence
    {
        public static List<string> GetNextMove(List<List<string>> positions)
        {
            if (positions == null || positions.Count == 0) return null;
            var greatPos = new List<List<string>>();
            var maxPos = int.MinValue;
            foreach (var position in positions)
            {
                var max = int.MinValue;
                foreach (var checker in position[position.Count - 1])
                {
                    if (checker == 'b') max++;
                    if (checker == 'B') max += 2;
                    if (checker == 'w') max--;
                    if (checker == 'W') max -= 2;
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
                foreach (var checker in position[position.Count - 1])
                {
                    if (checker == 'b') max--;
                    if (checker == 'B') max -= 2;
                    if (checker == 'w') max++;
                    if (checker == 'W') max += 2;
                }

                if (max > maxPos) maxPos = max;
            }

            return maxPos;
        }
    }
}