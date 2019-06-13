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
    public CapsuleCollider collider;
    public GameObject head;
    public float m_Speed = 1f;
    public float jumpForce = 5f;
    public LayerMask layermask;

    private float angle;
    private bool jumping = false;
    private Vector3 velocity;
    private Vector3 direction;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        if(body == null)
        {
            body = gameObject.GetComponent<Rigidbody>();
        }
        if(collider == null)
        {
            collider = gameObject.GetComponent<CapsuleCollider>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics.SphereCast(head.transform.position, collider.radius - 0.1f, Vector3.up * -1.0f, out hit, collider.height, layermask);
        Debug.DrawRay(head.transform.position, Vector3.up * -1.0f * (collider.height + 0.001f));
        if (hit.collider != null)
        {
            jumping = false;
        }
        else
        {
            jumping = true;
        }

        if (m_Direction.axis != Vector2.zero)
        {
            velocity = new Vector3(head.transform.forward.x, 0, head.transform.forward.z);
            direction = new Vector3(m_Direction.axis.x, 0, m_Direction.axis.y);

            angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            velocity = Quaternion.AngleAxis(angle, Vector3.up) * velocity;
            velocity.Normalize();
            velocity = velocity * m_Speed;
            velocity += new Vector3(0, body.velocity.y, 0);
            body.velocity = velocity;
        }
        if (m_Jump.state)
        {
            if (m_Jump.changed && !jumping)
            {
                Debug.Log("Before jump set: " + jumping);
                body.AddForce(Vector3.up * 1000f);
                jumping = true;
                Debug.Log("After jump set: " + jumping);
            }
        }
    }
}
