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
    private Grid roadGrid = new Grid();
    private Cell currentCell = new Cell(Vector2.zero);
    private float timer = 0f;
    private List<Node> nodes = new List<Node>();
    private int count = 0;
    private int deadEndCount = 0;
    private int removeCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        currentRoad = Instantiate(startRoad);
        roadGrid.AddCell(currentCell);
        roadGrid.AddCell(new Cell(currentCell.position + Vector2.down));
        roadGrid.AddCell(new Cell(currentCell.position + 2 * Vector2.down));
        roads.Push(currentRoad);
        CreateRoads(100);
        UpdateNodes();
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
            roadGrid.cells.RemoveAt(roadGrid.cells.Count - 1);

        }
        currentRoad = roads.Peek();
        roads.Pop();
        var prevRoad = roads.Peek();
        if (prevRoad.GetComponentInChildren<Node>() == null && currentRoad.GetComponentInChildren<Node>(true))
        {
            currentRoad.GetComponentInChildren<Node>(true).gameObject.SetActive(true);
        }
        roads.Push(currentRoad);
        currentCell = roadGrid.cells.Last();
    }

    public bool CellAvailable(Vector2 direction)
    {
        Cell cell2Check = new Cell(currentCell.position + direction);
        bool isAvailable = !roadGrid.cells.Exists(cell => cell.position == cell2Check.position);
        return isAvailable;
    }

    private void CreateRoads(int roadCount)
    {
        while (roads.Count < roadCount)
        {
            Vector2 roadDirection = currentRoad.GetRoadDirection();
            if (CellAvailable(roadDirection))
            {
                RoadTile previousRoad = currentRoad;
                Node prevNode;
                Node currentNode;
                currentCell = new Cell(currentCell.position + roadDirection);
                currentRoad = currentRoad.GenerateRoad();
                roadGrid.AddCell(currentCell);
                roads.Push(currentRoad);
                prevNode = previousRoad.GetComponentInChildren<Node>();
                currentNode = currentRoad.GetComponentInChildren<Node>();
                if (prevNode != null && currentNode != null)
                    prevNode.gameObject.SetActive(false);
            }
            else
            {
                DeleteLastRoads(removeCount);
            }
        }
    }

    private void UpdateNodes()
    {
        nodes.Clear();
        foreach (var road in roads)
        {
            if (road.GetComponentInChildren<Node>() != null)
            {
                nodes.Add(road.GetComponentInChildren<Node>());
            }
        }
    }

    public List<Node> GetNodes()
    {
        return nodes;
    }
}
