using UnityEngine;
using UnityEngine.AI;
using OurGame;

namespace OurGame
{
		public class PressurePlate : MonoBehaviour
		{
				/* 
				 * This can be improved, but it is a prototype and we have to throw away any code we use soooo
				 *    
				 * Collision event with either player or an object tagged "object" activates the bridge
				 * Should disappear upon removal   
				 * 
				 * How to use;
				 *    
				 * Attach script to pressure plate
				 * Bridge = bridge object
				 */

				public IActivate[] activates;
				public Material onMat;
				public Material offMat;

				public int requiredWeight = 0;
				public int currentWeight = 0;

				private Renderer render;
				private uint count; //Tracks how many objects are on top of plate


				private void Start()
				{
						foreach (IActivate activate in activates)
						{
								activate.DeactivateMe();
						}

						render = GetComponent<Renderer>();
						count = 0;
						currentWeight = 0;
				}

				private void OnTriggerEnter(Collider other)
				{
						try
						{
								currentWeight += other.GetComponent<CollisionTracker>().currentMass;
						}
						catch { }

						count++;
						if (currentWeight >= requiredWeight)
						{
								foreach (IActivate activate in activates)
								{
										activate.ActivateMe();
								}
								render.material = onMat;
						}
				}
				private void OnTriggerExit(Collider other)
				{
						try
						{
								currentWeight -= other.GetComponent<CollisionTracker>().currentMass;
						}
						catch { }

						count--;
						if (count == 0 || currentWeight < requiredWeight)
						{
								foreach (IActivate activate in activates)
								{
										activate.DeactivateMe();
								}
								render.material = offMat;
						}

						if (count < 0 || currentWeight < 0)
						{
								Debug.Log("Negative count or weight");
								count = 0;
								currentWeight = 0;
						}
				}
		}
}