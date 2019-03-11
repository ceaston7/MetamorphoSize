using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
		public class WeightTracker : MonoBehaviour
		{
				public int myWeight;

				//Keeps track of objects that have collided, as multiple collisions can come from one object
				private Dictionary<GameObject, int> collided = new Dictionary<GameObject,int>();

				void OnCollisionEnter(Collision collision)
				{
						foreach (ContactPoint point in collision.contacts) {
								if (point.point.y > transform.position.y && point.normal.y < -0.5)
								{
										if (collided.ContainsKey(point.otherCollider.gameObject))
										{
												//Debug.Log(point.otherCollider.gameObject.name + " already collided: " + collided[point.otherCollider.gameObject]);
												collided[point.otherCollider.gameObject]++;
										}
										else
										{
												//Debug.Log("Adding gameObject " + point.otherCollider.gameObject.name);
												collided.Add(point.otherCollider.gameObject, 1);
												try
												{
														myWeight += point.otherCollider.GetComponent<WeightTracker>().GetMyWeight();
												}
												catch { }
										}
								}
						}
				}

				void OnCollisionExit(Collision collision)
				{
						if (collided.ContainsKey(collision.gameObject))
						{
								if (collided[collision.gameObject] == 1)
								{
										//Debug.Log("Removing object " + collision.gameObject.name);
										collided.Remove(collision.gameObject);

										try
										{
												myWeight -= collision.transform.GetComponent<WeightTracker>().GetMyWeight();
										}
										catch { }
								}
								else
								{
										collided[collision.gameObject]--;
										//Debug.Log("Subtracted, now " + collided[collision.gameObject]);
								}
						}
				}

				public int GetMyWeight()
				{
						return myWeight;
				}

				public void SetMyWeight(int newWeight)
				{
						myWeight = newWeight;
				}
		}
}