using System;
namespace BattleshipStateTracker.SupportingClasses
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override bool Equals(object obj) => obj is Coordinate coordinate && this.Row == coordinate.Row && this.Column == coordinate.Column;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
