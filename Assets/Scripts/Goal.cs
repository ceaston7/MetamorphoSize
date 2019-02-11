using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
		public Canvas winScreen;

		void Start()
		{
				winScreen.enabled = false;
		}

		void OnCollisionEnter(Collision other)
		{
				if(other.gameObject.tag == "Player")
				{
						winScreen.enabled = true;
				}
		}
}
