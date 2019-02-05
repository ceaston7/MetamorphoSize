using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class PickUp : MonoBehaviour
    {
        public Transform pickUpObject;
        public GameObject item;
        public GameObject temp;

        private void Start()
        {
            item.GetComponent<Rigidbody>().useGravity = true;
        }

        private void Update()
        {
        }

        void OnMouseDown()
        {
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.position = pickUpObject.position;
            item.transform.rotation = pickUpObject.rotation;
            item.transform.parent = temp.transform;
        }

        void OnMouseUp()
        {
            item.transform.parent = null;
            item.transform.position = pickUpObject.transform.position;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}