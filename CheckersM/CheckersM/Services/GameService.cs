using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckersM.Models;
using MongoDB.Driver;

namespace CheckersM.Services
{
    public class GameService
    {
        private readonly IMongoCollection<Models.Game> _games;

        public GameService(IGameDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Models.Game>(settings.GameCollectionName);
        }

        public List<Models.Game> Get() =>
            _games.Find(game => true).ToList();

        public Models.Game Get(string id) =>
            _games.Find<Models.Game>(game => game.ConnectionId == id).FirstOrDefault();

        public Models.Game Create(Models.Game game)
        {
            _games.InsertOne(game);
            return game;
        }

        public void Update(string id, Models.Game gameIn) =>
            _games.ReplaceOne(book => book.ConnectionId == id, gameIn);

        public void Remove(Models.Game gameIn) =>
            _games.DeleteOne(game => game.ConnectionId == gameIn.ConnectionId);

        public void Remove(string id) =>
            _games.DeleteOne(game => game.ConnectionId == id);
    }
}
