﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CheckersEngine
{
    public class Board
    {
        private const int BoardLength = 8;
        private const int BoardSize = BoardLength * BoardLength;

        private readonly CellType[,] _boardState = new CellType[BoardLength, BoardLength];

        public Board()
        {
            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    _boardState[i, j] = CellType.Empty;
                }
            }
            ConvertBitBoardToBoard(new BitBoard());
        }

        public Board(BitBoard bitBoard)
        {
            ConvertBitBoardToBoard(bitBoard);
        }

        public CellType[,] GetBoardState() => _boardState; //for test

        public List<List<BitBoard>> GetAllPossiblePositions(PlayerType playerType)
        {
            var positions = new List<List<BitBoard>>();
            if (playerType == PlayerType.White)
            {
                for (var i = 0; i < BoardLength; i++)
                {
                    for (var j = 0; j < BoardLength; j++)
                    {
                        if (_boardState[i, j] == CellType.WhiteChecker)
                        {
                            positions = GetPositionsAfterFigths(i, j, playerType,
                                new[] { CellType.BlackChecker, CellType.BlackKing },
                                positions, new List<BitBoard>(), _boardState);
                        }
                    }
                }

                if (positions.Count == 0)
                {
                    for (var i = 0; i < BoardLength; i++)
                    {
                        for (var j = 0; j < BoardLength; j++)
                        {
                            if (_boardState[i, j] == CellType.WhiteChecker)
                            {
                                var tempBoardState = new CellType[BoardLength, BoardLength];
                                Array.Copy(_boardState, tempBoardState, BoardSize);
                                if (i + 1 < 8 && j + 1 < 8 && tempBoardState[i + 1, j + 1] == CellType.Empty)
                                {
                                    var positionsInTurn = new List<BitBoard>();
                                    tempBoardState[i, j] = CellType.Empty;
                                    if (i + 1 == BoardLength - 1)
                                        tempBoardState[i + 1, j + 1] = CellType.WhiteKing;
                                    else
                                        tempBoardState[i + 1, j + 1] = CellType.WhiteChecker;
                                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                                    positions.Add(positionsInTurn);
                                }
                                Array.Copy(_boardState, tempBoardState, BoardSize);
                                if (i + 1 < 8 && j - 1 > -1 && tempBoardState[i + 1, j - 1] == CellType.Empty)
                                {
                                    var positionsInTurn = new List<BitBoard>();
                                    tempBoardState[i, j] = CellType.Empty;
                                    if (i + 1 == BoardLength - 1)
                                        tempBoardState[i + 1, j - 1] = CellType.WhiteKing;
                                    else
                                        tempBoardState[i + 1, j - 1] = CellType.WhiteChecker;

                                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                                    positions.Add(positionsInTurn);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < BoardLength; i++)
                {
                    for (var j = 0; j < BoardLength; j++)
                    {
                        if (_boardState[i, j] == CellType.BlackChecker)
                        {
                            positions = GetPositionsAfterFigths(i, j, playerType,
                                new[] { CellType.WhiteChecker, CellType.WhiteKing },
                                positions, new List<BitBoard>(), _boardState);
                        }
                    }
                }

                if (positions.Count == 0)
                {
                    for (var i = 0; i < BoardLength; i++)
                    {
                        for (var j = 0; j < BoardLength; j++)
                        {
                            if (_boardState[i, j] == CellType.BlackChecker)
                            {
                                var tempBoardState = new CellType[BoardLength, BoardLength];
                                Array.Copy(_boardState, tempBoardState, BoardSize);
                                if (i - 1 > -1 && j + 1 < 8 && tempBoardState[i - 1, j + 1] == CellType.Empty)
                                {
                                    var positionsInTurn = new List<BitBoard>();
                                    tempBoardState[i, j] = CellType.Empty;
                                    if (i - 1 == 0)
                                        tempBoardState[i - 1, j + 1] = CellType.BlackKing;
                                    else
                                        tempBoardState[i - 1, j + 1] = CellType.BlackChecker;
                                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                                    positions.Add(positionsInTurn);
                                }
                                Array.Copy(_boardState, tempBoardState, BoardSize);
                                if (i - 1 > -1 && j - 1 > -1 && tempBoardState[i - 1, j - 1] == CellType.Empty)
                                {
                                    var positionsInTurn = new List<BitBoard>();
                                    tempBoardState[i, j] = CellType.Empty;
                                    if (i - 1 == 0)
                                        tempBoardState[i - 1, j - 1] = CellType.BlackKing;
                                    else
                                        tempBoardState[i - 1, j - 1] = CellType.BlackChecker;
                                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                                    positions.Add(positionsInTurn);
                                }
                            }
                        }
                    }
                }
            }

            return positions;
        }

        private List<List<BitBoard>> GetPositionsAfterFigths(int i, int j, PlayerType playerType, CellType[] enemies,
            List<List<BitBoard>> positions, List<BitBoard> positionsInTurn, CellType[,] boardState)
        {
            var tempBoardState = new CellType[BoardLength, BoardLength];
            Array.Copy(boardState, tempBoardState, BoardSize);
            if (j + 1 < 8 && i + 1 < 8 && enemies.Contains(tempBoardState[i + 1, j + 1]))
            {
                if (j + 2 < 8 && i + 2 < 8 && tempBoardState[i + 2, j + 2] == CellType.Empty)
                {
                    tempBoardState[i, j] = CellType.Empty;
                    tempBoardState[i + 1, j + 1] = CellType.Empty;
                    if (playerType == PlayerType.White)
                    {
                        if (i + 2 == BoardLength - 1)
                            tempBoardState[i + 2, j + 2] = CellType.WhiteKing;
                        else
                            tempBoardState[i + 2, j + 2] = CellType.WhiteChecker;
                    }
                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                    positions = GetPositionsAfterFigths(i + 2, j + 2, playerType, enemies, positions,
                        positionsInTurn, tempBoardState);
                }
            }
            Array.Copy(boardState, tempBoardState, BoardSize);
            if (j + 1 < 8 && i - 1 > -1 && enemies.Contains(tempBoardState[i - 1, j + 1]))
            {
                if (j + 2 < 8 && i - 2 > -1 && tempBoardState[i - 2, j + 2] == CellType.Empty)
                {
                    tempBoardState[i, j] = CellType.Empty;
                    tempBoardState[i - 1, j + 1] = CellType.Empty;
                    if (playerType == PlayerType.Black)
                    {
                        if (i - 2 == 0)
                            tempBoardState[i - 2, j + 2] = CellType.BlackKing;
                        else
                            tempBoardState[i - 2, j + 2] = CellType.BlackChecker;
                    }
                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                    positions = GetPositionsAfterFigths(i - 2, j + 2, playerType, enemies, positions,
                        positionsInTurn, tempBoardState);
                }
            }
            Array.Copy(boardState, tempBoardState, BoardSize);
            if (j - 1 > -1 && i - 1 > -1 && enemies.Contains(tempBoardState[i - 1, j - 1]))
            {
                if (j - 2 > -1 && i - 2 > -1 && tempBoardState[i - 2, j - 2] == CellType.Empty)
                {
                    tempBoardState[i, j] = CellType.Empty;
                    tempBoardState[i - 1, j - 1] = CellType.Empty;
                    if (playerType == PlayerType.Black)
                    {
                        if (i - 2 == 0)
                            tempBoardState[i - 2, j - 2] = CellType.BlackKing;
                        else
                            tempBoardState[i - 2, j - 2] = CellType.BlackChecker;
                    }
                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                    positions = GetPositionsAfterFigths(i - 2, j - 2, playerType, enemies, positions,
                        positionsInTurn, tempBoardState);
                }
            }
            Array.Copy(boardState, tempBoardState, BoardSize);
            if (j - 1 > -1 && i + 1 < 8 && enemies.Contains(tempBoardState[i + 1, j - 1]))
            {
                if (j - 2 > -1 && i + 2 < 8 && tempBoardState[i + 2, j - 2] == CellType.Empty)
                {
                    tempBoardState[i, j] = CellType.Empty;
                    tempBoardState[i + 1, j - 1] = CellType.Empty;
                    if (playerType == PlayerType.White)
                    {
                        if (i + 2 == BoardLength - 1)
                            tempBoardState[i + 2, j - 2] = CellType.WhiteKing;
                        else
                            tempBoardState[i + 2, j - 2] = CellType.WhiteChecker;
                    }
                    positionsInTurn.Add(GetBitBoardFromBoard(tempBoardState));
                    positions = GetPositionsAfterFigths(i + 2, j - 2, playerType, enemies, positions,
                        positionsInTurn, tempBoardState);
                }
            }

            if (positionsInTurn.Count > 0)
            {
                var positionsInTurnCopy = new BitBoard[positionsInTurn.Count];
                Array.Copy(positionsInTurn.ToArray(), positionsInTurnCopy, positionsInTurn.Count);
                if (positions.Count > 0)
                {
                    if (!positions[positions.Count - 1][positions[positions.Count - 1].Count - 1]
                        .Equals(positionsInTurn[positionsInTurn.Count - 1]))
                        positions.Add(positionsInTurnCopy.ToList());
                }
                else
                {
                    positions.Add(positionsInTurnCopy.ToList());
                }
            }
            return positions;
        }

        public BitBoard GetBitBoardFromBoard(CellType[,] boardState)
        {
            var whiteCheckers = new StringBuilder();
            var blackCheckers = new StringBuilder();
            var whiteKings = new StringBuilder();
            var blackKings = new StringBuilder();

            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    whiteCheckers.Append(boardState[i, j] == CellType.WhiteChecker ? '1' : '0');

                    blackCheckers.Append(boardState[i, j] == CellType.BlackChecker ? '1' : '0');

                    whiteKings.Append(boardState[i, j] == CellType.WhiteKing ? '1' : '0');

                    blackKings.Append(boardState[i, j] == CellType.BlackKing ? '1' : '0');
                }
            }

            return new BitBoard(Convert.ToInt64(whiteCheckers.ToString(), 2),
                Convert.ToInt64(whiteKings.ToString(), 2),
                Convert.ToInt64(blackCheckers.ToString(), 2),
                Convert.ToInt64(blackKings.ToString(), 2));
        }

        private void ConvertBitBoardToBoard(BitBoard bitBoard)
        {
            var whiteCheckers = Convert.ToString(bitBoard.WhiteCheckers, 2);
            var blackCheckers = Convert.ToString(bitBoard.BlackCheckers, 2);
            var whiteKings = Convert.ToString(bitBoard.WhiteKings, 2);
            var blackKings = Convert.ToString(bitBoard.BlackKings, 2);

            whiteCheckers = new string('0', BoardSize - whiteCheckers.Length) + whiteCheckers;
            blackCheckers = new string('0', BoardSize - blackCheckers.Length) + blackCheckers;
            whiteKings = new string('0', BoardSize - whiteKings.Length) + whiteKings;
            blackKings = new string('0', BoardSize - blackKings.Length) + blackKings;

            for (var i = 0; i < BoardLength; i++)
            {
                for (var j = 0; j < BoardLength; j++)
                {
                    if (whiteCheckers[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.WhiteChecker;
                    if (blackCheckers[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.BlackChecker;
                    if (whiteKings[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.WhiteKing;
                    if (blackKings[i * BoardLength + j].Equals('1'))
                        _boardState[i, j] = CellType.BlackKing;
                }
            }
        }
    }
}