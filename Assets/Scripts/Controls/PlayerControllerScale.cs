using System.Collections;
using System;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
using OurGame;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;

namespace OurGame
{

    public class PlayerControllerScale : MonoBehaviour
    {
        public Transform gun;
        private float maxScale;
        private float minScale;
        private PlayerState playerState;
        public float defaultRadius;
        public float defaultGroundCheck;
        public RigidbodyFirstPersonController m_RigidBody;
        public CapsuleCollider m_Capsule;
        private bool inAction = false;
        private bool pausable = false;
        private bool scaling = false;
        public Canvas canvas;
        private bool paused = false;

        public Color shrinkColor;
        public Color growColor;
        public SteamVR_LaserPointer laser;
        private Transform gunTip;
        public GameObject RayBlue;
        public GameObject RayOrange;
        private GameObject beam;
        public LayerMask layerMask;

        public float defaultHeight;

        private float maxRadius;
        private float maxHeight;
        void Start()
        {

            maxScale = 1.31F;
            minScale = .69F;

            playerState = GetComponent<PlayerState>();
            playerState.haveTool[0] = true;
            playerState.haveTool[1] = true;
            m_RigidBody = GetComponent<RigidbodyFirstPersonController>();
            m_Capsule = GetComponent<CapsuleCollider>();
            defaultRadius = m_Capsule.radius;
            defaultHeight = m_Capsule.height;
            defaultGroundCheck = m_RigidBody.advancedSettings.groundCheckDistance;
            maxRadius = defaultRadius + (maxScale - 1);
            maxHeight = defaultHeight + (maxScale - 1);
            canvas = Canvas.FindObjectOfType<Canvas>();
        }

        void Update()
        {
            if (Input.GetButton("Pause"))
            {
                if (!pausable)
                {
                    paused = !paused;
                }

                pausable = true;
                StartCoroutine(Action());
                if (paused)
                {
                    Time.timeScale = 0;
                    canvas.GetComponentInChildren<Text>().enabled = true;
                }
                else
                {
                    Time.timeScale = 1;
                    canvas.GetComponentInChildren<Text>().enabled = false;
                }
            }
            else if (CrossPlatformInputManager.GetButton("Cancel"))
            {
                Application.Quit();
            }
            else if (!Input.GetButton("Pause"))
            {
                pausable = false;
            }
        }

        public SteamVR_Action_Boolean m_MovePressShrink = null;
        public SteamVR_Action_Boolean m_MovePressToggle = null;
        private bool shrink = true;
        private bool allowInput = true;

        void FixedUpdate()
        {
            if (!paused)
            {
                if (m_MovePressToggle.state && allowInput)
                {
                    allowInput = false;
                    shrink = !shrink;
                    if (shrink)
                    {
                        //laser.setColor(shrinkColor);
                    }
                    else
                    {
                        //laser.setColor(growColor);
                    }
                    StartCoroutine(SwitchInput());
                }
                if (!(CrossPlatformInputManager.GetAxis("Fire1") != 0 || CrossPlatformInputManager.GetAxis("Fire2") != 0) && scaling)
                {
                    scaling = false;
                }

                if (m_MovePressShrink.state && shrink)
                {
                    RaycastHit hit;
                    Physics.Raycast(gun.position, gun.forward, out hit, 10000f, layerMask.value);
                    Debug.DrawRay(gun.position, gun.forward * 200, Color.red);
                    if (hit.collider != null) Debug.Log(hit.collider.gameObject);
                    if (!scaling)
                    {
                        scaling = true;
                    }
                    else
                    {

                    }

                    if (hit.collider != null)
                    {
                        try
                        {
                            hit.collider.gameObject.GetComponent<Scaler>().scale(-1);
                        }
                        catch { }
                    }
                }
                else if (m_MovePressShrink.state && !shrink)
                {
                    RaycastHit hit;
                    Physics.Raycast(gun.position, gun.forward, out hit, 10000f, layerMask.value);

                    if (!scaling)
                    {
                        scaling = true;
                    }
                    else
                    {

                    }

                    if (hit.collider != null)
                    {
                        try
                        {
                            hit.collider.gameObject.GetComponent<Scaler>().scale(1);
                        }
                        catch { }
                    }
                }
                else if (CrossPlatformInputManager.GetAxis("Fire3") != 0 && !inAction)
                {
                    RaycastHit hit;
                    Physics.Raycast(gun.position, gun.forward, out hit, 3.0f);

                    if (gun.Find("Hand").gameObject.transform.childCount > 0)
                    {
                        inAction = true;
                        GameObject obj = gun.transform.Find("Hand").GetChild(0).gameObject;
                        obj.GetComponent<PickUp>().Move(new GameObject());
                        //StartCoroutine(Action());
                    }
                    else if (hit.collider != null)
                    {
                        try
                        {
                            inAction = true;
                            hit.collider.GetComponent<PickUp>().Move(new GameObject());
                            //StartCoroutine(Action());
                        }
                        catch { }
                    }
                }
                else if (CrossPlatformInputManager.GetAxis("Scale0") != 0 && playerState.haveTool[(int)Tool.SizeSelf] == true)
                {
                    Vector3 newScale = transform.localScale + new Vector3(.1F, .1F, .1F);

                    if (SanityChecker(newScale))
                    {
                        transform.localScale = newScale;
                        // Handle Ground Check
                        m_RigidBody.advancedSettings.groundCheckDistance += .1F;
                        // Handle Radius
                        if (m_Capsule.radius + .1 > maxRadius)
                        {
                            m_Capsule.radius = maxRadius;
                        }
                        else
                        {
                            m_Capsule.radius += .1F;
                        }
                        // Handle Height breaks groundCheck if it doesn't grow
                        if (m_Capsule.height + .1F > maxHeight)
                        {
                            m_Capsule.height = maxHeight;
                        }
                        else
                        {
                            m_Capsule.height += .1F;
                        }
                    }
                    else
                    {
                        Debug.Log(transform.localScale);
                    }
                }
                else if (CrossPlatformInputManager.GetAxis("Scale1") != 0 && playerState.haveTool[(int)Tool.SizeSelf] == true)
                {
                    Vector3 newScale = transform.localScale - new Vector3(.1F, .1F, .1F);
                    if (SanityChecker(newScale))
                    {
                        transform.localScale = newScale;
                        m_RigidBody.advancedSettings.groundCheckDistance += -.1F;
                        if ((m_Capsule.radius - .1F) <= .1f)
                        {
                            m_Capsule.radius = .1f;
                        }
                        else
                        {
                            m_Capsule.radius += -.1f;
                        }
                        // Height shouldn't change if we shrink (Breaks movement physics)
                        if (m_Capsule.height != defaultHeight)
                        {
                            m_Capsule.height = defaultHeight;
                        }
                    }
                    else
                    {
                        Debug.Log(newScale);
                    }
                }
                else if (CrossPlatformInputManager.GetAxis("Scale3") != 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    m_Capsule.radius = defaultRadius;
                    m_RigidBody.advancedSettings.groundCheckDistance = defaultGroundCheck;
                    m_Capsule.height = defaultHeight;
                }

                if (CrossPlatformInputManager.GetAxis("Fire3") == 0)
                {
                    inAction = false;
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "PowerUp")
            {
                Destroy(collision.gameObject);
                maxScale = maxScale + .2F;
                minScale = minScale - .2F;
            }
        }
        public bool SanityChecker(Vector3 newScale)
        {
            if (newScale.x >= maxScale)
                return false;
            else if (newScale.x < minScale)
                return false;
            else if (newScale.y > maxScale)
                return false;
            else if (newScale.y < minScale)
                return false;
            else if (newScale.z > maxScale)
                return false;
            else if (newScale.z < minScale)
                return false;
            else
                return true;
        }
        private IEnumerator Action()
        {
            while (pausable)
                yield return null;
        }

        public bool GetShrinkState()
        {
            return shrink;
        }

        IEnumerator SwitchInput()
        {
            yield return new WaitForSeconds(.2f);
            allowInput = true;
        }

    }
}
