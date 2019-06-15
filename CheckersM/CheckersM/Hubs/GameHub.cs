using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CheckersAI;
using CheckersEngine;
using CheckersM.Game;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CheckersM.Hubs
{
    public class GameHub : Hub
    {
        private readonly Dictionary<string, PlayerData> _playersData = new Dictionary<string, PlayerData>();

        public async Task SendMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", Context.ConnectionId, message);
        }

        public async Task StartGame()
        {
            if (!_playersData.ContainsKey(Context.ConnectionId))
            {
                List<List<BitBoard>> positions;
                BitBoard bitBoard;
                var rnd = new Random();
                var playerType = (PlayerType) rnd.Next(0, 1);
                if (playerType == PlayerType.White)
                {
                    var board = new Board();
                    positions = board.GetAllPossiblePositions(playerType);
                    bitBoard = board.GetBitBoardFromBoard();
                }
                else
                {
                    var aiMove = ArtificialIntelligence.GetNextMove(new Board().GetAllPossiblePositions(PlayerType.White));
                    var board = new Board(aiMove[aiMove.Count - 1]);
                    positions = board.GetAllPossiblePositions(playerType);
                    bitBoard = board.GetBitBoardFromBoard();
                }
                _playersData[Context.ConnectionId] = new PlayerData(playerType, positions);
                var gameData = new GameData(bitBoard, _playersData[Context.ConnectionId]);
                await Clients.Caller.SendAsync("Start",JsonConvert.SerializeObject(gameData));
            }
        }
    }
}
