using System;
using System.Collections.Generic;
using System.Linq;

namespace MM_WPF;

public static class Utils
{
    public static IEnumerable<int> GetValues(this IEnumerable<string> List)
    {
        /*var columnsCount = MainDataGrid.Columns.Count;
        var rowsCount = MainDataGrid.Items.Count;
        var array = new int[columnsCount, rowsCount];

        for (var i = 1; i < columnsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                if (MainDataGrid.Items[j] is TableItem tableItem)
                    array[i, j] = tableItem.Columns.ElementAtOrDefault(i);
            }
        }*/
        return List
            .ToList()
            .Select(i => Convert.ToInt32(
                string.IsNullOrEmpty(i)
                    ? "0"
                    : i))
            .ToArray();
    }
}