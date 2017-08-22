using System;

namespace ConnectFourGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new ConnectFour();

            Console.Write(game.MapToString());

            var mapIsFull = false;

            do
            {
                Console.Write("Player 1: ");
                int playerMoveColumn = int.Parse(Console.ReadLine());
                game.Move(game.Player1, playerMoveColumn - 1);
                Console.Write(game.MapToString());

                if (game.HasWon(game.Player1))
                {
                    Console.WriteLine("Player 1 has won!");
                    break;
                }

                Console.Write("Player 2: ");
                playerMoveColumn = int.Parse(Console.ReadLine());
                game.Move(game.Player2, playerMoveColumn - 1);
                Console.Write(game.MapToString());

                if (game.HasWon(game.Player2))
                {
                    Console.WriteLine("Player 2 has won!");
                    break;
                }

                mapIsFull = game.MapIsFull();

                if (mapIsFull)
                {
                    Console.WriteLine("no winner situation!");
                    break;
                }

            } while (!mapIsFull);

            Console.ReadLine();
        }
    }
}
