using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayerInDriftArea : MonoBehaviour
{
    public static Action OnPlayerEnterDriftArea;
    public static Action OnPlayerExitDriftArea;
    //private Node _activeNode;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && OnPlayerEnterDriftArea != null)
        {
            //_activeNode = GetComponentInParent<Node>();
            OnPlayerEnterDriftArea();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Node currentNode = GetComponentInParent<Node>();
        if (collision.GetComponent<Player>() && OnPlayerExitDriftArea != null)
        {
            //aktif node bu node a eþit ise eventi çalýþtýr.
            //if (_activeNode.transform.parent.position == currentNode.transform.parent.position)
            //{
            //    OnPlayerExitDriftArea();
            //}
            GetComponentInParent<Node>().IsPassed = true;
        }
    }
}
