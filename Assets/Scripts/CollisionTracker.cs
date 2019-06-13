using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame {

		public class CollisionTracker : MonoBehaviour
		{
				private Dictionary<GameObject, ContactPoint[]> collisions;
				private List<GameObject> gameObjects;
				private List<GameObject> weights;
				public int baseMass;
				public int currentMass;
				public bool isScalable;

				void Start()
				{
						collisions = new Dictionary<GameObject, ContactPoint[]>();
						isScalable = (GetComponent("Scaler") != null);
						currentMass = baseMass;
						gameObjects = new List<GameObject>();
						weights = new List<GameObject>();
				}

				void OnCollisionEnter(Collision collision)
				{
						//Debug.Log("NEW ENTER -------------------------");

						/*foreach (ContactPoint point in collision.contacts)
						{
								Debug.Log(point.normal);
						}*/

						if (collisions.ContainsKey(collision.gameObject))
						{
								collisions[collision.gameObject] = collision.contacts;
						}
						else
						{
								collisions.Add(collision.gameObject, collision.contacts);
						}
						gameObjects.Add(collision.gameObject);

						/*foreach (KeyValuePair<GameObject, ContactPoint[]> a in collisions)
						{
								Debug.Log(a.Key.ToString().Substring(0, a.Key.ToString().Length - 24) + "normals: ");
								foreach(ContactPoint point in a.Value)
								{
										Debug.Log(point.normal);
								}
						}*/
				}

				void OnCollisionStay(Collision collision)
				{
						//Debug.Log("ON STAY --------------------------");

						float angle = 0;
						if (collisions.ContainsKey(collision.gameObject))
						{
								/*Debug.Log("Already in: " + collision.gameObject.ToString().Substring(0, collision.gameObject.ToString().Length - 24) + 
														"\ncollision normal 0: " + collision.GetContact(0).normal);*/
								collisions[collision.gameObject] = collision.contacts;
						}
						else
						{
								/*Debug.Log("Adding " + collision.gameObject.ToString().Substring(0, collision.gameObject.ToString().Length - 24) +
														"\ncollision normal 0: " + collision.GetContact(0).normal);*/
								collisions.Add(collision.gameObject, collision.contacts);
						}

						if (!gameObjects.Contains(collision.gameObject))
						{
								gameObjects.Add(collision.gameObject);
						}

						try
						{
								if (!weights.Contains(collision.gameObject))
								{
										collision.gameObject.GetComponent<CollisionTracker>();
										foreach (ContactPoint point in collisions[collision.gameObject])
										{
												if (point.point.y > transform.position.y && point.normal.y < -0.5)
												{
														weights.Add(collision.gameObject);
														currentMass += collision.gameObject.GetComponent<CollisionTracker>().currentMass;
														break;
												}
										}
								}
						}
						catch { }

						if (isScalable && gameObject.tag != "Player")
						{
								ContactPoint[] contactsA, contactsB;
								for (int i = 0; i < gameObjects.Count; i++)
								{
										for (int j = i + 1; j < gameObjects.Count; j++)
										{
												contactsA = collisions[gameObjects[i]];
												contactsB = collisions[gameObjects[j]];
												for(int k = 0; k < contactsA.Length; k++){
														for(int l = 0; l < contactsB.Length; l++){
																angle = Vector3.Angle(contactsA[k].normal, contactsB[l].normal);

																if (angle >= 170)
																{
																		if (CanConstrain(gameObjects[i], gameObjects[j]))
																		{
																				gameObject.GetComponent<Scaler>().canGrow = false;
																				return;
																		}
																}
														}
												}
										}
								}

								gameObject.GetComponent<Scaler>().canGrow = true;
						}
				}

				void OnCollisionExit(Collision collision)
				{
						if (collisions.ContainsKey(collision.gameObject))
						{
								collisions.Remove(collision.gameObject);
						}
						gameObjects.Remove(collision.gameObject);

						if (weights.Contains(collision.gameObject))
						{
								currentMass -= collision.gameObject.GetComponent<CollisionTracker>().currentMass;
								weights.Remove(collision.gameObject);
						}

				}

				private bool CanConstrain(GameObject a, GameObject b)
				{
						bool aHasRigid = a.GetComponent("Rigidbody") != null;
						bool bHasRigid = b.GetComponent("Rigidbody") != null;

						if (!(aHasRigid || bHasRigid))
						{
								return true;
						}

						if (aHasRigid && bHasRigid) {
								if (	a.GetComponent<CollisionTracker>().currentMass > baseMass 
										&& b.GetComponent<CollisionTracker>().currentMass > baseMass){
										
										return true;
								}
						}
						else {
								if (bHasRigid){
										GameObject temp;
										temp = a;
										a = b;
										b = temp;
								}

								if (a.GetComponent<CollisionTracker>().currentMass > baseMass){
										return true;
								}
						}

						return false;
				}
		}
}