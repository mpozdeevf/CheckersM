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
            var rnd = new Random();
            return positions.Count > 0 ? positions[rnd.Next(0, positions.Count - 1)] : null;
        }
    }
}
