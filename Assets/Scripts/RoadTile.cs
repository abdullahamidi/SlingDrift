using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadType
{
    UpLeftCorner,
    UpRightCorner,
    Vertical
}
public class RoadTile : MonoBehaviour
{
    [SerializeField] private Transform enterPoint;
    [SerializeField] private Transform exitPoint;

    [SerializeField] private List<RoadTile> possibleRoads;

    public RoadType roadType;
    public RoadTile GenerateRoad()
    {
        int possibleRoadTypeIndex = Random.Range(0, possibleRoads.Count);
        RoadTile newRoadTile = Instantiate(possibleRoads[possibleRoadTypeIndex], exitPoint.position, exitPoint.rotation);
        newRoadTile.enterPoint.parent = null;
        newRoadTile.transform.parent = newRoadTile.enterPoint;
        newRoadTile.enterPoint.position = exitPoint.position;
        newRoadTile.transform.parent = null;
        newRoadTile.enterPoint.parent = newRoadTile.transform;
        return newRoadTile;
    }

    public Vector2 GetRoadDirection()
    {
        Vector2 direction = new Vector2(exitPoint.up.x, exitPoint.up.y);

        //if (exitPoint.position.x != 0)
        //{
        //    if (exitPoint.position.x > 0)
        //        direction = Vector2.right;
        //    else
        //        direction = Vector2.left;
        //}
        //else
        //{
        //    if (exitPoint.position.y > 0)
        //        direction = Vector2.up;
        //    else
        //        direction = Vector2.down;
        //}
        return direction;
    }

    public Vector3 GetRoadRotation()
    {
        return exitPoint.eulerAngles;
    }

    //public RoadTile GenerateRoad(RoadType exceptedRoadType)
    //{
    //    int possibleRoadTypeIndex = Random.Range(0, possibleRoads.Count);
    //    while (possibleRoads[possibleRoadTypeIndex].roadType == exceptedRoadType)
    //    {
    //        possibleRoadTypeIndex = Random.Range(0, possibleRoads.Count);
    //    }
    //    RoadTile newRoadTile = Instantiate(possibleRoads[possibleRoadTypeIndex], exitPoint.position, exitPoint.rotation);
    //    newRoadTile.enterPoint.parent = null;
    //    newRoadTile.transform.parent = newRoadTile.enterPoint;
    //    newRoadTile.enterPoint.position = exitPoint.position;
    //    newRoadTile.transform.parent = null;
    //    newRoadTile.enterPoint.parent = newRoadTile.transform;
    //    return newRoadTile;
    //}
}
