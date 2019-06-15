using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckersEngine;

namespace CheckersM.Game
{
    public class GameData
    {
        public BitBoard BitBoard { get; set; }
        public PlayerData PlayerData { get; set; }

        public GameData(BitBoard bitBoard, PlayerData playerData)
        {
            BitBoard = bitBoard;
            PlayerData = playerData;
        }
    }
}
