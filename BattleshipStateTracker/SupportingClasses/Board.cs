using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace BattleshipStateTracker.SupportingClasses
{
    public class Board
    {
        public List<Panel> Panels;
        public List<Battleship> Ships;

        public Board()
        {
            Panels = new List<Panel>();
            for (var i = 0; i < 10; i++)
                for (var j = 0; j < 10; j++)
                    Panels.Add(new Panel(i, j));

            Ships = new List<Battleship>();
        }

        public bool AddBattleShipToBoard(Battleship ship, Coordinate startCoordinate, bool isVertical)
        {

            if (ship == null)
                throw new Exception("Please provide a valid battleship");
            if (startCoordinate == null)
                throw new Exception("Please provide a valid starting coordinate");
            int endRow = startCoordinate.Row;
            int endColumn = startCoordinate.Column;

            // Verify if the ship can be fit 
            for (var i = 1; i < ship.Width; i++)
                if (isVertical)
                    endRow++;
                else
                    endColumn++;

            if (endRow >= 10 || endColumn >= 10)
            {
                WriteLine("Battleship is too big to be placed in this coordinate");
                return false;
            }

            //  Verify if any position is already taken or not
            var panelRange = Panels.GetPanels(startCoordinate.Row, startCoordinate.Column, endRow, endColumn);
            if (panelRange.Any(panel => panel.IsOccupied))
            {
                WriteLine("Battleship can not be placed in this position because one or more postion is already occupied by another ship");
                return false;
            }
            // All good place the ship here 
            foreach (var panel in panelRange)
                panel.ShipName = ship.Name;

            Ships.Add(ship);
            WriteLine($"Battleship Successfully placed {(isVertical ? "Vertically" : "Horizontically")} from [{startCoordinate.Row}, {startCoordinate.Column}] to [{endRow}, {endColumn}]");
            return true;
        }

        public ShotResult TakeShot(Coordinate shotCoordinate)
        {
            var targetPanel = Panels.FirstOrDefault(x => x.Coordinate.Equals(shotCoordinate));
            if (targetPanel == null)
                throw new Exception("Invalid coordinate passed for shot");
            if (!targetPanel.IsOccupied)
            {
                WriteLine("Alas it was a miss!!");
                return ShotResult.Miss;
            }
            // Find the battleship on the panel
            var battleshipOnPanel = Ships.FirstOrDefault(ship => ship.Name.Equals(targetPanel.ShipName));

            if(battleshipOnPanel == null || battleshipOnPanel.Hits.Exists(x=> x.Equals(shotCoordinate)))
            {
                WriteLine("You can not target a panel which is already shot");
                return ShotResult.AlreadyHit;
            }
            battleshipOnPanel.Hits.Add(shotCoordinate);
            WriteLine("Hurray It was a hit!!");
            if (battleshipOnPanel.IsSunk)
                WriteLine($"Wow you have successfully sunk the {battleshipOnPanel.Name} battleship");
            return ShotResult.Hit;
        }

        public bool IsGameOver() => Ships.All(x => x.IsSunk);

        public void WatchBoard()
        {
            WriteLine("Your board currently looks like :");
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Write($"{ Panels.At(row, column).Status} ");
                }
                WriteLine(Environment.NewLine);
            }
            WriteLine(Environment.NewLine);
        }
    }
}
