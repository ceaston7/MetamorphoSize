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
        //public Camera Cam;

        private void Start()
        {
            //Cam = Camera.main;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

				public void Pick()
				{
						Debug.Log("pickup");
						GetComponent<Rigidbody>().useGravity = false;
						GetComponent<Rigidbody>().isKinematic = true;
						this.transform.position = Hold.position;
						this.transform.parent = Hold;
				}

				public void Drop()
				{
						Debug.Log("drop");
						this.transform.parent = null;
						GetComponent<Rigidbody>().useGravity = true;
						GetComponent<Rigidbody>().isKinematic = false;
				}

				/*
        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = Cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (CrossPlatformInputManager.GetButtonDown("Fire3"))
            {
                //If you're not holding the object, pick it up with Fire3
                if (held)
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = Hold.position;
                    this.transform.parent = GameObject.FindWithTag("player").transform;
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
				*/
		}
}
