using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckersEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CheckersM.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public BitBoard BitBoard { get; set; }
        public PlayerType PlayerType { get; set; }
        public List<List<BitBoard>> PossiblePositions { get; set; }
    }
}
