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
				public SearchAndDestroy script;

				public void OnTriggerEnter()
				{
						agent.enabled = true;
						script.enabled = true;
				}
		}
}