using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
		public class Scaler : MonoBehaviour
		{
				private float scaleFactor = 0.5f;
				private Vector3 scaleWeight = new Vector3(1,1,1); //Ensures that objects scale uniformly
				private int greatestDim; //Stores which of x, y, or z scale is largest

				private Collision collisionData;

				void Start()
				{
						float scaleSum = transform.localScale.x + transform.localScale.y + transform.localScale.z;

						for (int i = 0; i < 3; i++)
						{
								scaleWeight[i] = transform.localScale[i] / scaleSum;
						}

						greatestDim = transform.localScale.x > transform.localScale.y ? 0 : 1;
						greatestDim = transform.localScale[greatestDim] > transform.localScale.z ? greatestDim : 2;

				}

				public void scale(float growOrShrink)
				{
						transform.localScale += (scaleWeight * scaleFactor * transform.localScale[greatestDim] * growOrShrink * Time.deltaTime);
				}
		}
}
