using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using OurGame;
using UnityEngine;

public class PlayerControllerScale : MonoBehaviour
{
		public Camera cam;

		void Start()
		{
				cam = Camera.main;
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
		}
}
