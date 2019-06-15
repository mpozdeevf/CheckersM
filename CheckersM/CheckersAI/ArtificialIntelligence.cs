using System;
using System.Collections.Generic;
using System.Text;
using CheckersEngine;

namespace CheckersAI
{
    public static class ArtificialIntelligence
    {
        public static List<BitBoard> GetNextMove(List<List<BitBoard>> positions)
        {
            var rnd = new Random();
            return positions[rnd.Next(0, positions.Count - 1)];
        }
    }
}
