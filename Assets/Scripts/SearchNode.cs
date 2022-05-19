using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNode : MonoBehaviour
{
    public bool NodeFound => _nodeInRange != null;
    public Node ActiveNode;
    private Node _nodeInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Node>())
        {
            _nodeInRange = collision.GetComponent<Node>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_nodeInRange != null && _nodeInRange.IsPassed)
        {
            _nodeInRange = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Node>())
        {
            if (_nodeInRange != null && _nodeInRange.transform == collision.GetComponent<Node>().transform)
            {
                _nodeInRange = null;
            }
        }
    }

    public Node GetNodeInRange()
    {
        return _nodeInRange;
    }
}
