using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace OurGame
{

    /* 
     * To use:
     * Select playercharacter, create child object "Hands"
     * Add script to Cube
     * Hold = drag child object "Hands"
     * Cam = drag playercharacter camera
     * Tag Cube as "object"
     * 
     * Key code is Fire3, we are setting it to 'e'
     */

    public class PickUp : MonoBehaviour
    {
        public Transform Hold;
        public Camera Cam;
        private bool holding;

        private void Start()
        {
            Cam = Camera.main;
            GetComponent<Rigidbody>().useGravity = true;
        }

        private void Update()
        {
            RaycastHit hit;
            Ray ray = Cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (CrossPlatformInputManager.GetButtonDown("Fire3"))
            {
                //If you're not holding the object, pick it up with Fire3
                if (!holding && Physics.Raycast(ray, out hit, 3.0f) && hit.collider.tag == "object")
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = Hold.position;
                    this.transform.parent = GameObject.FindWithTag("player").transform;
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
