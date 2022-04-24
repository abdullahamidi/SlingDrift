using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField]
    private RoadTile startRoad;
    private RoadTile currentRoad;
    private Stack<RoadTile> roads = new Stack<RoadTile>();
    private Grid roadTiles = new Grid();
    private Cell currentCell = new Cell(Vector2.zero);
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentRoad = Instantiate(startRoad);
        roadTiles.AddCell(currentCell);
        roads.Push(currentRoad);
        CreateRoads(45);
    }


    private void Update()
    {
        if (timer >= 1.0f)
        {
            timer = 0f;
            CreateRoads(roads.Count + 1);
        }
        timer += Time.deltaTime;
    }

    private void DeleteLastRoads(int roadCount)
    {
        for (int i = 0; i < roadCount; i++)
        {
            Destroy(roads.Peek().gameObject);
            roads.Pop();
            roadTiles.cells.RemoveAt(roadTiles.cells.Count - 1);
        }
        currentRoad = roads.Peek();
        currentCell = roadTiles.cells.Last();
    }

    public bool CellAvailable(Vector2 direction)
    {
        Cell cell2Check = new Cell(currentCell.position + direction);
        Debug.Log(cell2Check.position);
        bool isAvailable = !roadTiles.cells.Exists(cell => cell.position == cell2Check.position);
        return isAvailable;
    }

    private void CreateRoads(int roadCount)
    {
        while (roads.Count < roadCount)
        {
            Vector2 roadDirection = currentRoad.GetRoadDirection();
            if (CellAvailable(roadDirection))
            {
                currentCell = new Cell(currentCell.position + roadDirection);
                currentRoad = currentRoad.GenerateRoad();
                roadTiles.AddCell(currentCell);
                roads.Push(currentRoad);
            }
            else
            {
                DeleteLastRoads(3);
            }
        }
    }
}
