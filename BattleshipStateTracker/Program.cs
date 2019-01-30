using System;
using BattleshipStateTracker.SupportingClasses;
using static System.Console;

namespace BattleshipStateTracker
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            WriteLine("Welcome to Battleship Tracker");
            WriteLine("Press 1 to Setup Game-Board");
            WriteLine("Press 2 to Exit");
            if (ReadKey().KeyChar == '2')
            {
                return;
            }

            try
            {
                StartGame();
            }
            catch(Exception ex)
            {
                WriteLine($"Game stopped Abrupptly with following error {ex.Message}");
            }

        }

        public static void StartGame()
        {
            var board = new Board();
            WriteLine("Your board is ready to place ships now. Your board looks like:");
            board.WatchBoard();

            var gameOn = true;
            // Keep presenting options and taking input from User untill User wants to quit
            do
            {
                DisplayOptions();
                var enteredKey = ReadKey();
                switch (enteredKey.KeyChar)
                {
                    case '1':
                        Write("\nEnter BattleShip Name ");
                        var shipName = ReadLine();
                        WriteLine("Enter BattleShip Length");
                        var shipLength = Convert.ToInt16(ReadLine());
                        if (shipLength > 10)
                            break;
                        WriteLine("Do You want to place it vertically? Enter Y/N ");
                        var isVertical = ReadKey().KeyChar.Equals('Y');
                        var battleShip = new Battleship(shipName, shipLength);
                        var shipPlacedSuccesfully = false;
                        //If Battleship could not be placed correctly keep asking for new coordinate
                        do
                        {
                            WriteLine("\nEnter Start X Postion between 0 - 9");
                            var xCoordinate = Convert.ToInt16(ReadLine());

                            WriteLine("Enter Start Y Postion between 0 - 9");
                            var yCoordinate = Convert.ToInt16(ReadLine());
                            shipPlacedSuccesfully = board.AddBattleShipToBoard(battleShip, new Coordinate(xCoordinate, yCoordinate), isVertical);
                            if (shipPlacedSuccesfully)
                            {
                                board.WatchBoard();
                            }
                        } while (!shipPlacedSuccesfully);
                        break;

                    case '2':
                        WriteLine("\nEnter Start X Postion between 0 - 9");
                        var rowAt = Convert.ToInt16(ReadLine());
                        WriteLine("Enter Start Y Postion between 0 - 9");
                        var columnAt = Convert.ToInt16(ReadLine());
                        board.TakeShot(new Coordinate(rowAt, columnAt));
                        break;

                    case '3':
                        WriteLine("\n" + (board.IsGameOver() ? "All your ships are sunk and Game over now" : "Keep playing you still have a chance to win"));
                        gameOn &= !board.IsGameOver();
                        break;

                    case '4':
                        board.WatchBoard();
                        break;
                    case '5':
                        gameOn = false;
                        break;

                    default:
                        WriteLine("\nSorry Couldn't understand your entered character. Please enter from below option");
                        break;

                }
            } while (gameOn);
        }

        public static void DisplayOptions()
        {
            WriteLine("Press 1 to Place a ship");
            WriteLine("Press 2 to Take a shot");
            WriteLine("Press 3 to Track your game");
            WriteLine("Press 4 to See your board");
            WriteLine("Press 5 to exit the game");
        }
    }
}
