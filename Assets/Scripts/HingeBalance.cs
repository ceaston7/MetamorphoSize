using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurGame;

namespace OurGame {
		public class HingeBalance : MonoBehaviour
		{
				public GameObject[] platforms;
				// Update is called once per frame
				void FixedUpdate()
				{
						foreach(Transform t in gameObject.GetComponentsInChildren<Transform>()){
								t.rotation.SetLookRotation(t.forward, Vector3.up);
						}
				}
		}
}