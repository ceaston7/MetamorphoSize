using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace OurGame
{

    /* 
     * To use:
     * Select playercharacter, create child object "PickUp"
     * Add script to Cube
     * Pick Up = drag child object "PickUp"
     * Cam = drag playercharacter camera
     * 
     * Key code is Fire3, we are setting it to 'e'
     */

    public class PickUp : MonoBehaviour
    {
        public Transform pickUp;
        public Camera cam;
        private bool holding;

        private void Start()
        {
            cam = Camera.main;
            GetComponent<Rigidbody>().useGravity = true;
        }

        private void Update()
        {
            RaycastHit hit;
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (CrossPlatformInputManager.GetButtonDown("Fire3"))
            {
                //If you're not holding the object, pick it up with Fire3
                if (!holding && Physics.Raycast(ray, out hit, 3.0f))
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = pickUp.position;
                    this.transform.parent = GameObject.Find("FirstPersonCharacter").transform;
                    holding = true;
                }
                //If you are holding the object, drop it with Fire3
                else
                {
                    this.transform.parent = null;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().isKinematic = false;
                    holding = false;
                }
            }
        }
    }
}
