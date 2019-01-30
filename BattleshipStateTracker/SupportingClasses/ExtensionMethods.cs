using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker.SupportingClasses
{
    public static class PanelExtensions
    {
        public static List<Panel> GetPanels(this List<Panel> panels, int startRow, int startColumn, int endRow, int endColumn) => panels.Where(x => x.Coordinate.Row >= startRow
                                                                                                                                                                        && x.Coordinate.Column >= startColumn
                                                                                                                                                                        && x.Coordinate.Row <= endRow
                                                                                                                                                                        && x.Coordinate.Column <= endColumn).ToList();
        public static Panel At(this List<Panel> panels, int row, int column) => panels.First(x => x.Coordinate.Column == column && x.Coordinate.Row == row);
    }
}
