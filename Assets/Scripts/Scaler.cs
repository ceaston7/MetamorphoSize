using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
		public class Scaler : MonoBehaviour
		{
				Vector3 scaleVector = new Vector3(1,1,1);
				public float scaleFactor = 0.2f;
				public void scale(float growOrShrink)
				{
						gameObject.transform.localScale += (scaleVector * scaleFactor * gameObject.transform.localScale.x * growOrShrink * Time.deltaTime);
				}
		}
}
