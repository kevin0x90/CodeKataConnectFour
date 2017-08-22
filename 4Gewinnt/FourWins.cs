using System;
using System.Collections.Generic;
using System.Text;

namespace _4Gewinnt
{
    public class FourWins
    {
        private const int ROWS = 6;
        private const int COLUMNS = 7;
        private const int NUMBER_OF_STONES_TO_WIN = 4;
        private readonly int MAP_SIZE = ROWS * COLUMNS;

        private List<Position> player1Positions = new List<Position>();
        private List<Position> player2Positions = new List<Position>();

        public FourWins()
        {
            Map = new int[ROWS, COLUMNS];
            Player1 = 1;
            Player2 = 2;
        }

        public int[,] Map { get; private set; }
        public int Player1 { get; private set; }
        public int Player2 { get; private set; }

        public bool MapIsFull()
        {
            return player1Positions.Count + player2Positions.Count == MAP_SIZE;
        }

        public void LoadMap(int[,] newMap)
        {
            if (Map.GetLength(0) != newMap.GetLength(0) || Map.GetLength(1) != newMap.GetLength(1))
            {
                throw new ArgumentException("Invalid map dimensions!");
            }

            player1Positions.Clear();
            player2Positions.Clear();

            IterateMapBottomToTop((row, column) =>
            {
                Map[row, column] = newMap[row, column];

                if (newMap[row, column] == Player1)
                {
                    player1Positions.Add(new Position(row, column));
                }
                else if (newMap[row, column] == Player2)
                {
                    player2Positions.Add(new Position(row, column));
                }
            });
        }

        private void IterateMapBottomToTop(Action<int, int> action)
        {
            for (int row = Map.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = Map.GetLength(1) - 1; column >= 0; column--)
                {
                    action(row, column);
                }
            }
        }

        private void IterateMapTopToBottom(Action<int, int> action)
        {
            for (int row = 0; row < Map.GetLength(0); row++)
            {
                for (int column = 0; column < Map.GetLength(1); column++)
                {
                    action(row, column);
                }
            }
        }

        public string MapToString()
        {
            var stringBuilder = new StringBuilder();

            IterateMapTopToBottom((row, column) =>
            {
                if (column < Map.GetLength(1) - 1)
                {
                    stringBuilder.Append(Map[row, column] + " ");
                }
                else
                {
                    stringBuilder.Append(Map[row, column]);
                    stringBuilder.AppendLine();
                }
            });

            return stringBuilder.ToString();
        }

        private void AddPlayerPosition(int player, int row, int column)
        {
            if (player == Player1)
            {
                player1Positions.Add(new Position(row, column));
            }
            else if (player == Player2)
            {
                player2Positions.Add(new Position(row, column));
            }
        }

        public bool Move(int player, int column)
        {
            var isMovePossible = false;

            for (int row = Map.GetLength(0) - 1; row >= 0; row--)
            {
                if (Map[row, column] == 0)
                {
                    Map[row, column] = player;
                    AddPlayerPosition(player, row, column);
                    isMovePossible = true;
                    break;
                }
            }

            return isMovePossible;
        }

        private bool PlayerHadNotEnoughMovesToWin(IList<Position> positions)
        {
            return positions.Count < NUMBER_OF_STONES_TO_WIN;
        }

        private IList<Position> GetPlayerMoves(int player)
        {
            if (player == Player1)
            {
                return player1Positions;
            }
            else if (player == Player2)
            {
                return player2Positions;
            }

            throw new ArgumentException("Unknown player!");
        }

        public bool HasWon(int player)
        {
            var hasWon = false;
            IList<Position> moves = moves = GetPlayerMoves(player);

            if (PlayerHadNotEnoughMovesToWin(moves))
            {
                return false;
            }

            foreach (var move in moves)
            {
                if (CanCheckLineToTheLeft(move) &&
                    PlayerHasFourToTheLeft(player, move))
                {
                    return true;
                }

                if (CanCheckLineToTheRight(move) &&
                    PlayerHasFourToTheRight(player, move))
                {
                    return true;
                }

                if (CanCheckLineToTheTop(move) &&
                    PlayerHasFourToTheTop(player, move))
                {
                    return true;
                }

                if (CanCheckLineToTheBottom(move) &&
                    PlayerHasFourToTheBottom(player, move))
                {
                    return true;
                }

                if (CanCheckDiagonalLineFromTopLeftToBottomRight(move) &&
                    PlayerHasFourDiagonalFromTopLeftToBottomRight(player, move))
                {
                    return true;
                }

                if (CanCheckDiagonalLineFromTopRightToBottomLeft(move) &&
                    PlayerHasFourDiagonalFromTopRightToBottomLeft(player, move))
                {
                    return true;
                }

                if (CanCheckDiagonalLineFromBottomLeftToTopRight(move) &&
                    PlayerHasFourDiagonalFromBottomLeftToTopRight(player, move))
                {
                    return true;
                }

                if (CanCheckDiagonalLineFromBottomRightToTopLeft(move) &&
                    PlayerHasFourDiagonalFromBottomRightToTopLeft(player, move))
                {
                    return true;
                }
            }

            return hasWon;
        }

        private bool CanCheckLineToTheLeft(Position position)
        {
            return position.Column - NUMBER_OF_STONES_TO_WIN >= 0;
        }

        private bool PlayerHasFourToTheLeft(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row, position.Column - 1] == player &&
                   Map[position.Row, position.Column - 2] == player &&
                   Map[position.Row, position.Column - 3] == player;
        }

        private bool CanCheckLineToTheRight(Position position)
        {
            return position.Column + NUMBER_OF_STONES_TO_WIN < COLUMNS;
        }

        private bool PlayerHasFourToTheRight(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row, position.Column + 1] == player &&
                   Map[position.Row, position.Column + 2] == player &&
                   Map[position.Row, position.Column + 3] == player;
        }

        private bool CanCheckLineToTheTop(Position position)
        {
            return position.Row - NUMBER_OF_STONES_TO_WIN >= 0;
        }

        private bool PlayerHasFourToTheTop(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row - 1, position.Column] == player &&
                   Map[position.Row - 2, position.Column] == player &&
                   Map[position.Row - 3, position.Column] == player;
        }

        private bool CanCheckLineToTheBottom(Position position)
        {
            return position.Row + NUMBER_OF_STONES_TO_WIN < ROWS;
        }

        private bool PlayerHasFourToTheBottom(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row + 1, position.Column + 1] == player &&
                   Map[position.Row + 2, position.Column + 2] == player &&
                   Map[position.Row + 3, position.Column + 3] == player;
        }

        private bool CanCheckDiagonalLineFromTopLeftToBottomRight(Position position)
        {
            return CanCheckLineToTheRight(position) && CanCheckLineToTheBottom(position);
        }

        private bool PlayerHasFourDiagonalFromTopLeftToBottomRight(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row + 1, position.Column + 1] == player &&
                   Map[position.Row + 2, position.Column + 2] == player &&
                   Map[position.Row + 3, position.Column + 3] == player;
        }

        private bool CanCheckDiagonalLineFromTopRightToBottomLeft(Position position)
        {
            return CanCheckLineToTheLeft(position) && CanCheckLineToTheBottom(position);
        }

        private bool PlayerHasFourDiagonalFromTopRightToBottomLeft(int player, Position position)
        {
            return Map[position.Column, position.Row] == player &&
                   Map[position.Row + 1, position.Column - 1] == player &&
                   Map[position.Row + 2, position.Column - 2] == player &&
                   Map[position.Row + 3, position.Column - 3] == player;
        }

        private bool CanCheckDiagonalLineFromBottomLeftToTopRight(Position position)
        {
            return CanCheckLineToTheRight(position) && CanCheckLineToTheTop(position);
        }

        private bool PlayerHasFourDiagonalFromBottomLeftToTopRight(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row - 1, position.Column + 1] == player &&
                   Map[position.Row - 2, position.Column + 2] == player &&
                   Map[position.Row - 3, position.Column + 3] == player;
        }

        private bool CanCheckDiagonalLineFromBottomRightToTopLeft(Position position)
        {
            return CanCheckLineToTheLeft(position) && CanCheckLineToTheTop(position);
        }

        private bool PlayerHasFourDiagonalFromBottomRightToTopLeft(int player, Position position)
        {
            return Map[position.Row, position.Column] == player &&
                   Map[position.Row - 1, position.Column - 1] == player &&
                   Map[position.Row - 2, position.Column - 2] == player &&
                   Map[position.Row - 3, position.Column - 3] == player;
        }
    }
}
