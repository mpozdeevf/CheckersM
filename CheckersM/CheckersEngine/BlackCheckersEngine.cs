using System.Collections.Generic;

namespace CheckersEngine
{
    public class BlackCheckersEngine : Engine
    {
        private char[] _enemies = {'w', 'W'};

        public BlackCheckersEngine(string stringBoard) : base(stringBoard)
        {
        }

        public BlackCheckersEngine()
        {
        }

        public override List<List<string>> GetPossiblePositions()
        {
            throw new System.NotImplementedException();
        }
    }
}