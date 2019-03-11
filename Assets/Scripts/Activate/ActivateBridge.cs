using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using OurGame;

namespace OurGame
{
		public class ActivateBridge : IActivate
		{
				override public void ActivateMe()
				{
						GetComponent<MeshRenderer>().enabled = true;
						GetComponent<BoxCollider>().enabled = true;
						GetComponent<NavMeshModifierVolume>().enabled = false;
				}

				public override void DeactivateMe()
				{
						GetComponent<MeshRenderer>().enabled = false;
						GetComponent<BoxCollider>().enabled = false;
						GetComponent<NavMeshModifierVolume>().enabled = true;
				}
		}
}