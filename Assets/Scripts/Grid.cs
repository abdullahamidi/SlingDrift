using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public List<Cell> cells = new List<Cell>();

    public void AddCell(Cell cell)
    {
        cells.Add(cell);
    }
}
