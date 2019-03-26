// MoveTo.cs
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;
using UnityEngine.SceneManagement;

namespace OurGame
{
		public class SearchAndDestroy : MonoBehaviour
		{
				public float fear_scale;

				public Transform hiding_spot;
				private Transform goal;

				private GameObject[] targets;

				private GameObject[] hiding_spots;

				private float distToGround;

				private NavMeshAgent agent;

				void Start()
				{
						agent = GetComponent<NavMeshAgent>();
						agent.updateUpAxis = false;
						targets = GameObject.FindGameObjectsWithTag("Player");
						hiding_spots = GameObject.FindGameObjectsWithTag("HidingSpot");
						if (!hiding_spot)
						{
							hiding_spot = hiding_spots[0].transform;
						}
						// Assumes there is only one Player Object in the game. This can also be a public variable we assign if we prefer.
						goal = targets[0].transform;
						agent.destination = goal.position;
						// These are subject to change
						fear_scale = 1.5F;
				}

				void FixedUpdate()
				{
						if (!IsGrounded() && agent.enabled)
						{
								agent.enabled = false;
								Rigidbody rigid_body = GetComponent<Rigidbody>();
								rigid_body.useGravity = true;
								rigid_body.isKinematic = false;
								rigid_body.AddForce(0, 0, -1f);
						}
						else if (IsGrounded() && (goal.localScale[0] < fear_scale))
						{
							agent.destination = goal.position;
						}
						else if (IsGrounded() && (goal.localScale[0] > fear_scale))
						{
							agent.destination = hiding_spot.transform.position;
						}
						else
						{
								Rigidbody rigid_body = GetComponent<Rigidbody>();
								rigid_body.AddForce(0, 0, -1f);
						}
				}

				bool IsGrounded()
				{
						return Physics.Raycast(transform.position, -Vector3.up, distToGround + 5f);
				}

				void OnCollisionEnter(Collision collision)
				{
						if (collision.gameObject.tag == "Player")
						{
								SceneManager.LoadScene("VerticalSlice");
						}
				}
		}
}