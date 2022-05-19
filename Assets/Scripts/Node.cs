using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private float connectableCircleRadius = 3f;
    [SerializeField]
    private float moveableCircleRadius = 2f;

    private bool _isPassed = false;

    public bool IsPassed
    {
        get { return _isPassed; }
        set { _isPassed = value; }
    }


    private void Update()
    {
    }
}
