﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
using OurGame;
using UnityEngine;

public class PlayerControllerScale : MonoBehaviour
{
		public Camera cam;
		private float maxScale;
		private float minScale;
		private PlayerState playerState;
		private float defaultRadius;
		private float defaultGroundCheck;
		RigidbodyFirstPersonController m_RigidBody;
		CapsuleCollider m_Capsule;

		void Start()
		{
				cam = Camera.main;

				maxScale = 1.5F;
				minScale = .5F;

				playerState = GetComponent<PlayerState>();
				playerState.haveTool[0] = true;
				playerState.haveTool[1] = true;
				m_RigidBody = GetComponent<RigidbodyFirstPersonController>();
				m_Capsule = GetComponent<CapsuleCollider>();
				defaultRadius = m_Capsule.radius;
				defaultGroundCheck = m_RigidBody.advancedSettings.groundCheckDistance;
		}


		void FixedUpdate()
		{
				if (CrossPlatformInputManager.GetAxis("Fire1") != 0 && playerState.haveTool[(int)Tool.SizeGun] == true)
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
				else if (CrossPlatformInputManager.GetAxis("Fire2") != 0 && playerState.haveTool[(int)Tool.SizeGun] == true)
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

						if (hit.collider != null)
						{
								try
								{
                                        				hit.collider.gameObject.GetComponent<PickUp>().Move();
                                				}
								catch { }
						}
				}
				else if (CrossPlatformInputManager.GetAxis("Scale0") != 0 && playerState.haveTool[(int)Tool.SizeSelf] == true)
				{
					Vector3 newScale = transform.localScale + new Vector3(.1F, .1F, .1F);
					
					if(SanityChecker(newScale))
					{
						transform.localScale = newScale;
						m_RigidBody.advancedSettings.groundCheckDistance += .1F; 
						m_Capsule.radius += .1f;
					}
					else
					{
						Debug.Log(newScale);
					}
				}
				else if (CrossPlatformInputManager.GetAxis("Scale1") != 0 && playerState.haveTool[(int)Tool.SizeSelf] == true)
				{
					Vector3 newScale = transform.localScale - new Vector3(.1F, .1F, .1F);
					if(SanityChecker(newScale))
					{
						transform.localScale = newScale;
						m_RigidBody.advancedSettings.groundCheckDistance += -.1F; 	
						m_Capsule.radius += -.1f;

					}
					else
					{
						Debug.Log(newScale);
					}
				}
				else if(CrossPlatformInputManager.GetAxis("Scale3") != 0){
						transform.localScale = new Vector3(1f, 1f, 1f);
						m_Capsule.radius = defaultRadius;
						m_RigidBody.advancedSettings.groundCheckDistance = defaultGroundCheck;
				}
				else if (CrossPlatformInputManager.GetAxis("Cancel") != 0)
					{
					    Application.Quit();
					}
				}

		 public bool SanityChecker(Vector3 newScale)
         {
            if (newScale.x > maxScale)
                return false;
			else if (newScale.x < minScale)
				return false;
			else if (newScale.y > maxScale)
				return false; 
			else if (newScale.y < minScale)
				return false;
			else if (newScale.z > maxScale)
				return false;
			else if (newScale.z < minScale)
				return false;
			else
				return true;
         }
}
