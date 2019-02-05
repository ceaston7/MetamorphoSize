using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        public Transform pickUpObject;
        public bool holding = false;

        private void Start()
        {
            GetComponent<Rigidbody>().useGravity = true;
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire3"))
            {
                if(!holding)
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = pickUpObject.position;
                    this.transform.parent = GameObject.Find("FirstPersonCharacter").transform;
                    holding = true;
                }
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
