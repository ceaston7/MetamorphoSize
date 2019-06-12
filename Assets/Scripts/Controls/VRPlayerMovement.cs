using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using OurGame;

public class VRPlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 m_Direction = null;
    public SteamVR_Action_Boolean m_Jump = null;
    public Rigidbody body;
    public GameObject head;
    public float m_Speed = 1f;
    private Vector3 force;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        force = head.transform.forward;
        direction = new Vector3(m_Direction.axis.x, 0, m_Direction.axis.y);
        force.Scale(direction);
        force.Normalize();
        body.AddForce(force*m_Speed);
    }
}
