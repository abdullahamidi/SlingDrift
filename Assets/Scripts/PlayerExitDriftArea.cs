using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitDriftArea : MonoBehaviour
{
    public static Action OnPlayerExitDriftArea;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            OnPlayerExitDriftArea?.Invoke();
            if (transform.parent.parent.GetComponentInChildren<Node>()) transform.parent.parent.GetComponentInChildren<Node>().IsPassed = true;
        }
    }
}
