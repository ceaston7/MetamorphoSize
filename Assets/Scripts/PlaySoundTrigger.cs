using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTrigger : MonoBehaviour
{
		public float waitTime;
		private bool firstTime = true;

		void OnTriggerEnter(Collider other)
		{
				if (other.tag == "Player" && firstTime)
				{
						firstTime = false;
						gameObject.GetComponent<AudioSource>().Play();
				}
		}
}
