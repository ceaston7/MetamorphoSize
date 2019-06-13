using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using OurGame;

public class VRPlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 m_Direction = null;
    public SteamVR_Action_Boolean m_Jump = null;
    public SteamVR_Action_Boolean m_MovePressShrink = null;
    public SteamVR_Action_Boolean m_MovePressToggle = null;
    public GameObject head;
    public Rigidbody body;
    public CapsuleCollider collider;
    public LayerMask spherecastMask;
    public LayerMask layerMask;
    public float m_Speed = 1f;
    public float jumpForce = 5f;
    public Transform gun;
    public Color shrinkColor;
    public Color growColor;
    public SteamVR_LaserPointer laser;

    private float angle;
    private bool jumping = false;
    private Vector3 velocity;
    private Vector3 direction;
    private RaycastHit hit;
    private bool shrink = true;
    private bool allowInput = true;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("[ViveInputUtility]")) Destroy(GameObject.Find("[ViveInputUtility]"));
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
        Physics.SphereCast(head.transform.position, collider.radius - 0.1f, Vector3.up * -1.0f, out hit, collider.height * transform.localScale.x + 0.001f, spherecastMask);
        Debug.DrawRay(head.transform.position, Vector3.up * -1.0f * (collider.height * transform.localScale.x + 0.001f));
        if (hit.collider != null)
        {
            jumping = false;
        }
        else
        {
            jumping = true;
        }

        if (m_MovePressToggle.state && allowInput)
        {
            allowInput = false;
            shrink = !shrink;
            if (shrink)
            {
                laser.setColor(shrinkColor);
            }
            else
            {
                laser.setColor(growColor);
            }
            StartCoroutine(SwitchInput());
        }

        //Move player
        if (m_Direction.axis != Vector2.zero)
        {
            print(m_Direction.axis.ToString("F4"));
            velocity = new Vector3(head.transform.forward.x, 0, head.transform.forward.z);
            direction = new Vector3(m_Direction.axis.x, 0, m_Direction.axis.y);

            angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            velocity = Quaternion.AngleAxis(angle, Vector3.up) * velocity;
            velocity.Normalize();
            velocity = velocity * m_Speed * transform.localScale.x;
            velocity += new Vector3(0, body.velocity.y, 0);
            body.velocity = velocity;
        }

        //Jump
        if (m_Jump.state)
        {
            if (m_Jump.changed && !jumping)
            {
                Debug.Log("Before jump set: " + jumping);
                body.AddForce(Vector3.up * 1000f * transform.localScale.x);
                jumping = true;
                Debug.Log("After jump set: " + jumping);
            }
        }

        //Shoot
        if (m_MovePressShrink.state)
        {
            RaycastHit hit;
            Physics.Raycast(gun.position, gun.forward, out hit, 10000f, layerMask.value);
            Debug.DrawRay(gun.position, gun.forward * 200, Color.red);

            if (hit.collider != null)
            {
                if(hit.collider.gameObject.name == "HeadCollider")
                {
                    Debug.Log("HEAD");
                    gameObject.GetComponent<Scaler>().scale(shrink ? -1 : 1);
                }

                try
                {
                    hit.collider.gameObject.GetComponent<Scaler>().scale(shrink ? -1 : 1);
                }
                catch { }
            }
        }

        IEnumerator SwitchInput()
        {
            yield return new WaitForSeconds(.2f);
            allowInput = true;
        }
    }
}
