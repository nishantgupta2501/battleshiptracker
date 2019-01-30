using System;
using System.Collections.Generic;

namespace BattleshipStateTracker.SupportingClasses
{
    public class Battleship
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public List<Coordinate> Hits { get; set; }
        public bool IsSunk => Hits.Count == Width;

        public Battleship(string name, int width)
        {
            Name = name;
            Width = width;
            Hits = new List<Coordinate>();
        }
    }
}
