using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNode : MonoBehaviour
{
    public bool NodeFound => _nodeInRange != null;
    private Node _nodeInRange;

    //public static Action<Node> OnNodeFound;
    //public static Action<Node> OnNodeExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Node>())
        {
            _nodeInRange = collision.GetComponent<Node>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Node>())
        {
            if (_nodeInRange.transform == collision.GetComponent<Node>().transform)
            {
                _nodeInRange = null;
            }
        }
    }

    public Node GetNode()
    {
        return _nodeInRange;
    }
}
