using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckersM.Models
{
    public class GameDatabaseSettings : IGameDatabaseSettings
    {
        public string GameCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IGameDatabaseSettings
    {
        string GameCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
