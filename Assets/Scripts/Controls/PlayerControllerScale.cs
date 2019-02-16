using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using OurGame;
using UnityEngine;

public class PlayerControllerScale : MonoBehaviour
{
		public Camera cam;
		private bool holding;

		void Start()
		{
				cam = Camera.main;
				holding = false;
		}


		void FixedUpdate()
		{
				if (CrossPlatformInputManager.GetAxis("Fire1") != 0)
				{
						RaycastHit hit;
						Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200);
						Debug.DrawRay(cam.transform.position, cam.transform.forward * 200, Color.red);
						if (hit.collider != null)
						{
								try
								{
										hit.collider.gameObject.GetComponent<Scaler>().scale(-1);
								}
								catch { }
						}
				}
				else if (CrossPlatformInputManager.GetAxis("Fire2") != 0)
				{
						RaycastHit hit;
						Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200);
						if (hit.collider != null)
						{
								try
								{
										hit.collider.gameObject.GetComponent<Scaler>().scale(1);
								}
								catch { }
						}
				}
				else if(CrossPlatformInputManager.GetAxis("Fire3") != 0)
				{
						RaycastHit hit;
						Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3.0f);

						if (!holding && hit.collider != null)
						{
								try
								{
										hit.collider.gameObject.GetComponent<PickUp>().Pick();
										StartCoroutine(WaitAfterPickup(true));
								}
								catch { }
						}
						else if(holding && hit.collider != null)
						{
								try
								{
										hit.collider.gameObject.GetComponent<PickUp>().Drop();
										StartCoroutine(WaitAfterPickup(false));
								}
								catch { }
						}
				}
				else if (CrossPlatformInputManager.GetAxis("Cancel") != 0)
					{
					    Application.Quit();
					}
				}

		IEnumerator WaitAfterPickup(bool update)
		{
				yield return new WaitForSecondsRealtime(0.1f);
				holding = update;
		}
}
