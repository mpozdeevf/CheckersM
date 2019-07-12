using System;
using System.Threading;
using System.Threading.Tasks;
using CheckersAI;
using CheckersEngine;
using CheckersM.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CheckersM.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task StartGame()
        {
            var playerType = PlayerType.White;
            
            var engine = WhiteTurn(null);
            var positions = engine.GetPossiblePositions();
            var board = engine.GetCurrentPosition();
            
            var game = new Models.Game
            {
                ConnectionId = Context.ConnectionId,
                Board = board,
                PlayerType = playerType,
                PossiblePositions = positions
            };
            _gameService.Create(game);
            await Clients.Caller.SendAsync("Start", JsonConvert.SerializeObject(game));
        }

        public async void PlayGame(string stringBoard)
        {
            Thread.Sleep(4000);
            var engine = BlackTurn(stringBoard);
            var positions = engine.GetPossiblePositions();
            if (positions == null || positions.Count == 0)
            {
                await Clients.Caller.SendAsync("End", "White won");
                return;
            }
            var position = ArtificialIntelligence
                .GetNextMove(positions);
            var board = position[position.Count - 1];
            var newGame = _gameService.Get(Context.ConnectionId);
            newGame.Board = board;
            var newPositions = WhiteTurn(board).GetPossiblePositions();
            newGame.PossiblePositions = newPositions;
            _gameService.Update(Context.ConnectionId, newGame);
            await Clients.Caller.SendAsync("Start", JsonConvert.SerializeObject(newGame));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _gameService.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        
        private static Engine WhiteTurn(string board)
        {
            var engine = new WhiteCheckersEngine(board);
            return engine;
        }
        
        private static Engine BlackTurn(string board)
        {
            var engine = new BlackCheckersEngine(board);
            return engine;
        }
    }
}
