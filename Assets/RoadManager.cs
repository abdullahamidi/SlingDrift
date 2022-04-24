using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Sprite vertical;
    [SerializeField] Sprite horizontal;
    [SerializeField] Sprite upRight;
    [SerializeField] Sprite upLeft;
    [SerializeField] Sprite downRight;
    [SerializeField] Sprite downLeft;
    Sprite rightTurn;
    Sprite leftTurn;
    Sprite currentSprite;
    Dictionary<Sprite, List<Sprite>> spriteConnectivity;
    List<Transform> road = new List<Transform>();
    float spawnTime = 1.0f;
    bool roadTowardsLeft = false;

    private void Start()
    {
        foreach (Transform roadPart in transform)
        {
            road.Add(roadPart);
        }
        currentSprite = road.Last().GetComponent<SpriteRenderer>().sprite;
        spriteConnectivity = new Dictionary<Sprite, List<Sprite>>()
        {
            { vertical, new List<Sprite>() { vertical, rightTurn,leftTurn }},
            { horizontal, new List<Sprite>() { horizontal, rightTurn,leftTurn }},
            { upRight, new List<Sprite>() { horizontal, upLeft, downLeft}},
            { upLeft, new List<Sprite>() { horizontal, upRight, downRight}},
            { downRight, new List<Sprite>() { horizontal, upLeft, downLeft }},
            { downLeft, new List<Sprite>() { horizontal, upRight, downRight}},
        };
        StartCoroutine(SpawnRoad());
    }

    private IEnumerator SpawnRoad()
    {
        while (true)
        {
            rightTurn = player.velocity.x > 0 ? upRight : downRight;
            leftTurn = player.velocity.x > 0 ? upLeft : downLeft;
            int randomRoadIndex = Random.Range(0, spriteConnectivity[currentSprite].Count);
            Sprite lastSprite = road.Last().GetComponent<SpriteRenderer>().sprite;
            Sprite newSprite = spriteConnectivity[currentSprite][randomRoadIndex];
            if (lastSprite == upLeft || lastSprite == downLeft)
                roadTowardsLeft = true;
            else
                roadTowardsLeft = false;
            var newRoad = road[0];
            road.RemoveAt(0);
            //newRoad.position = road.Last().position + roadPosDiff;
            newRoad.GetComponent<SpriteRenderer>().sprite = newSprite;
            road.Add(newRoad);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
