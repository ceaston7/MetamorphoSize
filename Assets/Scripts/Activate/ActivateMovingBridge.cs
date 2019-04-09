using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurGame;

namespace OurGame
{
		public class ActivateMovingBridge : IActivate
		{
				public override void ActivateMe(){
            Debug.Log("IN ACTIVATE");
            Debug.Log(gameObject.name);
						gameObject.GetComponent<Animator>().SetBool("IsUp", true);
				}

				public override void DeactivateMe(){
						gameObject.GetComponent<Animator>().SetBool("IsUp", false);
				}
		}
}