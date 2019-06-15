using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckersEngine;

namespace CheckersM.Game
{
    public class PlayerData
    {
        public PlayerType CheckersColor { get; set; }
        public List<List<BitBoard>> PossiblePositions { get; set; }

        public PlayerData(PlayerType playerType, List<List<BitBoard>> positions)
        {
            CheckersColor = playerType;
            PossiblePositions = positions;
        }
    }
}
