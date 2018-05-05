using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform main;
    public Transform openPoint;
    public Transform closedPoint;

    [HideInInspector]
    public bool closed = true;

    public void ChangeState(bool toClosed)
    {
        if (toClosed)
        {
            main.transform.position = closedPoint.position;
            closed = true;
        }
        else
        {
            main.transform.position = openPoint.position;
            closed = false;
        }
    }
}
