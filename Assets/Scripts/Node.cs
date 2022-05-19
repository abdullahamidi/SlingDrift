using UnityEngine;

public class Node : MonoBehaviour
{
    private bool _isPassed = false;

    public bool IsPassed
    {
        get { return _isPassed; }
        set { _isPassed = value; }
    }

}
