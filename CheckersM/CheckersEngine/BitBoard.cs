using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersEngine
{
    public class BitBoard
    {
        public readonly long WhiteCheckers;
        public readonly long WhiteKings;
        public readonly long BlackCheckers;
        public readonly long BlackKings;

        public BitBoard()
        {
            WhiteKings = 0;
            BlackKings = 0;
            WhiteCheckers = Convert.ToInt64("101010100101010110101010" + new string('0', 40), 2);
            BlackCheckers = Convert.ToInt64("010101011010101001010101", 2);
        }

        public BitBoard(long whiteCheckers, long whiteKings, long blackCheckers, long blackKings)
        {
            WhiteCheckers = whiteCheckers;
            WhiteKings = whiteKings;
            BlackCheckers = blackCheckers;
            BlackKings = blackKings;
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
