using UnityEngine;
using System.Collections;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        private bool held;
        private GameObject hand;

        private void Update()
        {
            // If the object collides with something and breaks the joint
            // update the status of it being held and destroy hand.
            if(gameObject.GetComponent<FixedJoint>() == null && held)
            {
                Destroy(hand);
                transform.gameObject.transform.parent = null;
                StartCoroutine(UpdateHolding(false));
            }
        }

        // Helper method to either pick up or drop object
        public void Move()
        {
            if (!held)
            {
                Pick();
            }
            else
            {
                Drop();
            }
        }

        private void Pick()
        {
            if (hand == null)
            {
                // Create a hand object and place it infront of the player
                hand = new GameObject("Hand");
                hand.transform.parent = Camera.main.transform;
                GameObject player = Camera.main.transform.parent.gameObject;
                hand.transform.position = player.transform.position + player.transform.forward * 2.5f + player.transform.up * 0.2f;

                Rigidbody body = hand.AddComponent<Rigidbody>();
                body.isKinematic = true;
                body.useGravity = false;
            }

            // Move object into hand
            gameObject.transform.position = hand.transform.position;
            gameObject.transform.localRotation = Camera.main.transform.rotation;
            gameObject.transform.SetParent(hand.transform);
 
            // Give object a FixedJoint to maintain physics
            // Break force for wall collisions. Sensitivity can be edited (or removed entirely).
            FixedJoint joint = transform.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = hand.GetComponent<Rigidbody>();
            joint.breakForce = 10000f;

            StartCoroutine(UpdateHolding(true));
        }

        private void Drop()
        {
            //Remove object from hand and destroy the joint/hand
            gameObject.transform.parent = null;
            Destroy(transform.gameObject.GetComponent<FixedJoint>());
            Destroy(hand);

            StartCoroutine(UpdateHolding(false));

        }

        IEnumerator UpdateHolding(bool status)
        {
            yield return new WaitForSeconds(0.5f);
            held = status;
        }
    }
}
