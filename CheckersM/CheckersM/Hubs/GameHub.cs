using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckersAI;
using CheckersEngine;
using CheckersM.Game;
using CheckersM.Models;
using CheckersM.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CheckersM.Hubs
{
    public class GameHub : Hub
    {
        //private readonly Dictionary<string, PlayerData> _playersData = new Dictionary<string, PlayerData>();
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", Context.ConnectionId, message);
        }

        public async Task StartGame()
        {
            List<List<BitBoard>> positions;
            BitBoard bitBoard;
            //var rnd = new Random();
            //var playerType = (PlayerType) rnd.Next(0, 1);
            var playerType = PlayerType.White;

            var board = new Board();
            positions = board.GetAllPossiblePositions(playerType);
            bitBoard = board.GetBitBoardFromBoard();

            //if (playerType == PlayerType.White)
            //{
            //    var board = new Board();
            //    positions = board.GetAllPossiblePositions(playerType);
            //    bitBoard = board.GetBitBoardFromBoard();
            //}
            //else
            //{
            //    var aiMove = ArtificialIntelligence.GetNextMove(new Board().GetAllPossiblePositions(PlayerType.White));
            //    var board = new Board(aiMove[aiMove.Count - 1]);
            //    positions = board.GetAllPossiblePositions(playerType);
            //    bitBoard = board.GetBitBoardFromBoard();
            //}
            var game = new Models.Game
            {
                ConnectionId = Context.ConnectionId,
                BitBoard = bitBoard,
                PlayerType = playerType,
                PossiblePositions = positions
            };
            _gameService.Create(game);
            await Clients.Caller.SendAsync("Start", JsonConvert.SerializeObject(game));
        }

        public async Task PlayGame(string jsonBitboard)
        {
            var bitBoard = JsonConvert.DeserializeObject<BitBoard>(jsonBitboard);
            var board = new Board(bitBoard);
            var position = ArtificialIntelligence
                .GetNextMove(board.GetAllPossiblePositions(PlayerType.Black));
            bitBoard = position[position.Count - 1];
            board = new Board(bitBoard);
            var newGame = _gameService.Get(Context.ConnectionId);
            newGame.BitBoard = bitBoard;
            newGame.PossiblePositions = board.GetAllPossiblePositions(newGame.PlayerType);
            _gameService.Update(Context.ConnectionId, newGame);
            Thread.Sleep(4000);
            await Clients.Caller.SendAsync("Start", JsonConvert.SerializeObject(newGame));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _gameService.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
