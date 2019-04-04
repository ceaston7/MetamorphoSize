using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using OurGame;

namespace OurGame
{
		public class TriggerAI : MonoBehaviour
		{
				public NavMeshAgent agent;
				public SearchAndDestroy script1;
				public Resize script2;

				public void OnTriggerEnter()
				{
						agent.enabled = true;
						script2.enabled = true;
				}
		}
}