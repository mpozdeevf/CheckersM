using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CheckersEngine
{
    public class BitBoard
    {
        public readonly long WhiteCheckers;
        public readonly long WhiteKings;
        public readonly long BlackCheckers;
        public readonly long BlackKings;

        public readonly string WhiteCheckersStr;
        public readonly string WhiteKingsStr;
        public readonly string BlackCheckersStr;
        public readonly string BlackKingsStr;

        public BitBoard()
        {
            WhiteKings = 0;
            BlackKings = 0;
            WhiteCheckers = Convert.ToInt64("101010100101010110101010" + new string('0', 40), 2);
            BlackCheckers = Convert.ToInt64("010101011010101001010101", 2);

            WhiteCheckersStr = Convert.ToString(WhiteCheckers, 2);
            WhiteCheckersStr = new string('0', 64 - WhiteCheckersStr.Length) + WhiteCheckersStr;

            WhiteKingsStr = Convert.ToString(WhiteKings, 2);
            WhiteKingsStr = new string('0', 64 - WhiteKingsStr.Length) + WhiteKingsStr;

            BlackCheckersStr = Convert.ToString(BlackCheckers, 2);
            BlackCheckersStr = new string('0', 64 - BlackCheckersStr.Length) + BlackCheckersStr;

            BlackKingsStr = Convert.ToString(BlackKings, 2);
            BlackKingsStr = new string('0', 64 - BlackKingsStr.Length) + BlackKingsStr;
        }

        public BitBoard(long whiteCheckers, long whiteKings, long blackCheckers, long blackKings)
        {
            WhiteCheckers = whiteCheckers;
            WhiteKings = whiteKings;
            BlackCheckers = blackCheckers;
            BlackKings = blackKings;

            WhiteCheckersStr = Convert.ToString(WhiteCheckers, 2);
            WhiteCheckersStr = new string('0', 64 - WhiteCheckersStr.Length) + WhiteCheckersStr;

            WhiteKingsStr = Convert.ToString(WhiteKings, 2);
            WhiteKingsStr = new string('0', 64 - WhiteKingsStr.Length) + WhiteKingsStr;

            BlackCheckersStr = Convert.ToString(BlackCheckers, 2);
            BlackCheckersStr = new string('0', 64 - BlackCheckersStr.Length) + BlackCheckersStr;

            BlackKingsStr = Convert.ToString(BlackKings, 2);
            BlackKingsStr = new string('0', 64 - BlackKingsStr.Length) + BlackKingsStr;
        }

        [JsonConstructor]
        public BitBoard(long whiteCheckers, long whiteKings, long blackCheckers, long blackKings, 
            string whiteCheckersStr, string whiteKingsStr, string blackCheckersStr, string blackKingsStr)
        {
            WhiteCheckers = whiteCheckers;
            WhiteKings = whiteKings;
            BlackCheckers = blackCheckers;
            BlackKings = blackKings;
            WhiteCheckersStr = whiteCheckersStr;
            WhiteKingsStr = whiteKingsStr;
            BlackCheckersStr = blackCheckersStr;
            BlackKingsStr = blackKingsStr;
        }

        public override bool Equals(object obj)
        {
            return obj is BitBoard bitBoard &&
                   (bitBoard.BlackCheckers == BlackCheckers && bitBoard.BlackKings == BlackKings
                                                            && bitBoard.WhiteCheckers == WhiteCheckers &&
                                                            bitBoard.WhiteKings == WhiteKings);
        }

        public override int GetHashCode()
        {
            return (int)unchecked(2 * WhiteCheckers + 3 * WhiteKings + 5 * BlackCheckers + 111 * BlackKings);
        }
    }
}
