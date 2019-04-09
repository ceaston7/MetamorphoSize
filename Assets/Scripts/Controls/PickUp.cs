using UnityEngine;
using System.Collections;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        private bool held;
        private GameObject hand;

        private void Start()
        {
            if(Camera.main.transform.Find("Hand").gameObject)
            {
                hand = Camera.main.transform.Find("Hand").gameObject;
            }
            else
            {
                Vector3 location = Camera.main.transform.position;
                hand = new GameObject("Hand");
                hand.transform.parent = Camera.main.transform;
                GameObject player = Camera.main.transform.parent.gameObject;
                hand.transform.position = new Vector3(location.x, location.y + .5f, location.z + 3);
            }

            if (hand.GetComponent<Rigidbody>() == null)
            {
                // Give hand a rigidbody
                Rigidbody body = hand.AddComponent<Rigidbody>();
                body.isKinematic = true;
                body.useGravity = false;
            }
        }

        private void Update()
        {
            // If the object collides with something and breaks the joint
            // update the status of it being held and destroy hand.
            if(gameObject.GetComponent<FixedJoint>() == null && hand.transform.childCount > 0)
            {
                gameObject.transform.parent = null;
                held = false;
            }
        }

        // Helper method to either pick up or drop object
        public IEnumerator Move()
        {
            if (!held)
            {
                Pick();
                while (!held)
                    yield return null;
            }
            else
            {
                Drop();
                while (held)
                    yield return null;
            }
        }

        private void Pick()
        {
            // Move object into hand
            transform.position = hand.transform.position;
            transform.localRotation = Camera.main.transform.rotation;
            transform.parent = hand.transform;
 
            // Give object a FixedJoint to maintain physics
            // Break force for wall collisions. Sensitivity can be edited (or removed entirely).
            FixedJoint joint = transform.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = hand.GetComponent<Rigidbody>();
            joint.breakForce = 10000f;

            held = true;
        }

        private void Drop()
        {
            //Remove object from hand and destroy the joint/hand
            gameObject.transform.parent = null;
            Destroy(gameObject.GetComponent<FixedJoint>());

            held = false;
        }
    }
}
