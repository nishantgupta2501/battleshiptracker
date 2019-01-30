using System;
namespace BattleshipStateTracker.SupportingClasses
{
    public class Panel
    {
        public string ShipName { get; set; }
        public Coordinate Coordinate { get; set; }

        public Panel(int row, int column)
        {
            Coordinate = new Coordinate(row, column);
            ShipName = String.Empty;
        }

        public char Status => !string.IsNullOrEmpty(ShipName) ? ShipName[0] : '0';

        public bool IsOccupied => !string.IsNullOrEmpty(ShipName);
    }
}
