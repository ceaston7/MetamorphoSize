using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurGame;

namespace OurGame
{
		public class ActivateDoor : IActivate
		{
				public override void ActivateMe()
				{
						transform.GetComponent<Animator>().SetBool("isOpen", true);
				}

				public override void DeactivateMe()
				{
						transform.GetComponent<Animator>().SetBool("isOpen", false);
				}
		}
}