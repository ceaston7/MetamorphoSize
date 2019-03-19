using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    private Vector3 StartLoc;
    private Vector3 StartSize;

    private void Start()
    {
        StartLoc = transform.position;
        StartSize = GetComponent<Renderer>().bounds.size;
    }

    public Vector3 getStartLocation()
    {
        return StartLoc;
    }

    public Vector3 getStartSize()
    {
        return StartSize;
    }
}
