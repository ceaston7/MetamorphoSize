// MoveTo.cs
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;
using UnityEngine.SceneManagement;

namespace OurGame
{
		public class SearchAndDestroy : MonoBehaviour
		{

				private Transform goal;

				private GameObject[] targets;

				private float distToGround;

				private NavMeshAgent agent;
				void Start()
				{
						agent = GetComponent<NavMeshAgent>();
						targets = GameObject.FindGameObjectsWithTag("Player");
						// Assumes there is only one Player Object in the game. This can also be a public variable we assign if we prefer.
						goal = targets[0].transform;
						agent.destination = goal.position;
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
						else if (IsGrounded())
						{
								agent.destination = goal.position;
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