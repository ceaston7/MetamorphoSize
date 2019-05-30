using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurGame;

namespace OurGame {
		public class HingeBalance : MonoBehaviour
		{
				void Start(){
						HingeJoint hinge = GetComponents<HingeJoint>()[0];
						JointLimits limits = new JointLimits();
						Debug.Log(transform.rotation.eulerAngles.x);
						limits.min = -90.0f - transform.rotation.eulerAngles.x;
						limits.max = 90.0f - transform.rotation.eulerAngles.x;
						hinge.limits = limits;
						hinge.useLimits = true;
				}
		}
}