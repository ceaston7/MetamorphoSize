using UnityEngine;
using System.Collections;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        private GameObject obj;
        private bool held;

        private void Start()
        {
            obj = transform.gameObject;
        }

        private void Update()
        {
            if(obj.GetComponent<FixedJoint>() == null && held)
            {
                held = false;
            }
        }

        public void Move(PlayerControllerScale control)
        {
            if (!held)
            {
                Pick(control.hand);
            }
            else
            {
                Drop();
            }

            StartCoroutine(UpdateHolding());
        }

        private void Pick(GameObject hand)
        {
            obj.transform.position = hand.transform.position;
            obj.transform.localRotation = Camera.main.transform.rotation;
 
            FixedJoint joint = obj.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = hand.GetComponent<Rigidbody>();
            joint.breakForce = 10000f;
        }

        private void Drop()
        {
            Destroy(obj.GetComponent<FixedJoint>());
            obj.transform.parent = null;
            obj.transform.localRotation = transform.rotation;

        }

        IEnumerator UpdateHolding()
        {
            held = !held;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
