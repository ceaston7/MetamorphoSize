using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        public Transform pickUpObject;

        void OnMouseDown()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            this.transform.position = pickUpObject.position;
            this.transform.parent = GameObject.Find("FirstPersonCharacter").transform;
        }

        void OnMouseUp()
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}
