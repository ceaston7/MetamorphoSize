using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 linearVelocity = new Vector3(1.0f, 0.0f, 0.0f);
    public float rotationalSpeed = 30.0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += linearVelocity * Time.fixedDeltaTime;
    }
}
