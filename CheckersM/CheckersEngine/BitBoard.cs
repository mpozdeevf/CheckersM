using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersEngine
{
    public class BitBoard
    {
        public long WhiteCheckers { get; set; }
        public long WhiteKings { get; set; }
        public long BlackCheckers { get; set; }
        public long BlackKings { get; set; }

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
    }
}
