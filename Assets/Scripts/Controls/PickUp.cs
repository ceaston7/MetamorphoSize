using UnityEngine;
using System.Collections;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        private bool held;
        private Transform cam;

        private void Start()
        {

        }

        public void Move(GameObject hand)
        {
            if (!held)
            {
                Pick(hand);
            }
            else if (held)
            {
                Drop();
            }
        }

        private void Pick(GameObject hand)
        {
            // Move object into hand
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

        private void OnJointBreak()
        {
            gameObject.transform.parent = null;
            held = false;
        }
    }
}
