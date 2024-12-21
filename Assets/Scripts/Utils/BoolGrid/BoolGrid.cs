using System.Collections.Generic;
using System;

[Serializable]
public class BoolGrid
{
    public List<Row> rows = new List<Row>();

    [Serializable]
    public class Row
    {
        public List<bool> columns = new List<bool>();
    }

    public void Resize(int width, int height)
    {
        // Изменяем количество строк
        while (rows.Count < height)
            rows.Add(new Row());
        while (rows.Count > height)
            rows.RemoveAt(rows.Count - 1);

        // Изменяем количество столбцов в каждой строке
        for (int y = 0; y < height; y++)
        {
            Row row = rows[y];
            if (row.columns == null)
                row.columns = new List<bool>();
            while (row.columns.Count < width)
                row.columns.Add(true);
            while (row.columns.Count > width)
                row.columns.RemoveAt(row.columns.Count - 1);
        }
    }

    public bool GetCell(int x, int y)
    {
        return rows[y].columns[x];
    }

    public void SetCell(int x, int y, bool value)
    {
        rows[y].columns[x] = value;
    }

    public int Width
    {
        get { return rows.Count > 0 ? rows[0].columns.Count : 0; }
    }

    public int Height
    {
        get { return rows.Count; }
    }
}