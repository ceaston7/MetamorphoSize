using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OurGame
{
    public class Balance : MonoBehaviour
    {
        public GameObject otherScale;
        private int weight;
        private float height;
        private float minHeight;
        private float maxHeight;
        private float heightMultiplier;

        void Start()
        {
            height = transform.position.y;

            try
            {
                float otherHeight = otherScale.GetComponent<Balance>().GetHeight();
                if (otherHeight != height)
                {
                    float newHeight = (otherHeight + height) / 2;
                    otherScale.GetComponent<Balance>().SetHeight(newHeight);
                    SetHeight(newHeight);
                }
            }
            catch { }

            minHeight = height - 5;
            maxHeight = height + 5;

            if (height < 6) heightMultiplier = height / 5;
            else heightMultiplier = 1;
        }

        public int GetWeight()
        {
            return weight;
        }

        public float GetHeight()
        {
            return height;
        }

        public void SetHeight(float newHeight)
        {
            height = newHeight;
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }

        void OnCollisionEnter(Collision collision)
        {
            weight += collision.gameObject.GetComponent<WeightTracker>().GetMyWeight();
            FindHeights();
        }

        void OnCollisionExit(Collision collision)
        {
            weight -= collision.gameObject.GetComponent<WeightTracker>().GetMyWeight();
            FindHeights();
        }

        void FindHeights()
        {
            int otherWeight = otherScale.GetComponent<Balance>().GetWeight();
            float otherHeight = otherScale.GetComponent<Balance>().GetHeight();

            int diff = weight - otherWeight;

            float greater = 0;
            float lesser = 0;

            if (Math.Abs(diff) > 5)
            {
                greater = maxHeight;
                lesser = minHeight;
            }
            else
            {
                greater = ((otherHeight + height) / 2) - diff * heightMultiplier;
                lesser = ((otherHeight + height) / 2) + diff * heightMultiplier;
            }

            if(diff >= 0)
            {
                SetHeight(greater);
                otherScale.GetComponent<Balance>().SetHeight(lesser);
            }
            else
            {
                SetHeight(lesser);
                otherScale.GetComponent<Balance>().SetHeight(greater);
            }   
        }
    }
}

