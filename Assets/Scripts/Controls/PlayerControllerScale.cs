using System.Collections;
using System;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
using OurGame;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScale : MonoBehaviour
{
		public Camera cam;
		private float maxScale;
		private float minScale;
		private PlayerState playerState;
		public float defaultRadius;
		public float defaultGroundCheck;
		public RigidbodyFirstPersonController m_RigidBody;
		public CapsuleCollider m_Capsule;
		private bool inAction = false;
		private bool pausable = false;
		private bool scaling = false;
		public Canvas canvas;
		private bool paused = false;
		
		private Transform gunTip;
		public GameObject RayBlue;
		public GameObject RayOrange;
		private GameObject beam;
		public LayerMask layerMask;

		public float defaultHeight;

		private float maxRadius;
		private float maxHeight;
		void Start()
		{
				cam = Camera.main;

				maxScale =  1.31F;
				minScale = .69F;

				playerState = GetComponent<PlayerState>();
				playerState.haveTool[0] = true;
				playerState.haveTool[1] = true;
				m_RigidBody = GetComponent<RigidbodyFirstPersonController>();
				m_Capsule = GetComponent<CapsuleCollider>();
				defaultRadius = m_Capsule.radius;
				defaultHeight = m_Capsule.height;
				defaultGroundCheck = m_RigidBody.advancedSettings.groundCheckDistance;
				maxRadius = defaultRadius + (maxScale-1);
				maxHeight = defaultHeight + (maxScale-1);
				gunTip = GameObject.Find("GunTip").transform;
				canvas = Canvas.FindObjectOfType<Canvas>();
		}

		void Update(){
				if (Input.GetButton("Pause"))
				{
						if(!pausable){
								paused = !paused;
						}

						pausable = true;
						StartCoroutine(Action());
						if (paused)
						{
								Time.timeScale = 0;
								canvas.GetComponentInChildren<Text>().enabled = true;
						}
						else
						{
								Time.timeScale = 1;
								canvas.GetComponentInChildren<Text>().enabled = false;
						}
				}
				else if (CrossPlatformInputManager.GetButton("Cancel"))
				{
						Application.Quit();
				}
				else if (!Input.GetButton("Pause"))
				{
						pausable = false;
				}
		}

		void FixedUpdate()
		{
				if (!paused)
				{
						if (!(CrossPlatformInputManager.GetAxis("Fire1") != 0 || CrossPlatformInputManager.GetAxis("Fire2") != 0) && scaling)
						{
								scaling = false;
								DestroyGunEffect();
						}

						if (CrossPlatformInputManager.GetAxis("Fire1") != 0 && playerState.haveTool[(int)Tool.SizeGun] == true)
						{
								RaycastHit hit;
								Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10000f, layerMask.value);
								Debug.DrawRay(cam.transform.position, cam.transform.forward * 200, Color.red);

								if (!scaling)
								{
										CreateGunEffect(RayBlue, hit);
										scaling = true;
								}
								else
								{
										UpdateBeam(hit);
								}

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
								Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10000f, layerMask.value);

								if (!scaling)
								{
										CreateGunEffect(RayOrange, hit);
										scaling = true;
								}
								else
								{
										UpdateBeam(hit);
								}

								if (hit.collider != null)
								{
										try
										{
												hit.collider.gameObject.GetComponent<Scaler>().scale(1);
										}
										catch { }
								}
						}
						else if (CrossPlatformInputManager.GetAxis("Fire3") != 0 && !inAction)
						{
								RaycastHit hit;
								Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3.0f);

								if (cam.transform.Find("Hand").gameObject.transform.childCount > 0)
								{
										inAction = true;
										GameObject obj = cam.transform.Find("Hand").GetChild(0).gameObject;
										obj.GetComponent<PickUp>().Move(new GameObject());
										//StartCoroutine(Action());
								}
								else if (hit.collider != null)
								{
										try
										{
												inAction = true;
												hit.collider.GetComponent<PickUp>().Move(new GameObject());
												//StartCoroutine(Action());
										}
										catch { }
								}
						}
						else if (CrossPlatformInputManager.GetAxis("Scale0") != 0 && playerState.haveTool[(int)Tool.SizeSelf] == true)
						{
								Vector3 newScale = transform.localScale + new Vector3(.1F, .1F, .1F);

								if (SanityChecker(newScale))
								{
										transform.localScale = newScale;
										// Handle Ground Check
										m_RigidBody.advancedSettings.groundCheckDistance += .1F;
										// Handle Radius
										if (m_Capsule.radius + .1 > maxRadius)
										{
												m_Capsule.radius = maxRadius;
										}
										else
										{
												m_Capsule.radius += .1F;
										}
										// Handle Height breaks groundCheck if it doesn't grow
										if (m_Capsule.height + .1F > maxHeight)
										{
												m_Capsule.height = maxHeight;
										}
										else
										{
												m_Capsule.height += .1F;
										}
								}
								else
								{
										Debug.Log(transform.localScale);
								}
						}
						else if (CrossPlatformInputManager.GetAxis("Scale1") != 0 && playerState.haveTool[(int)Tool.SizeSelf] == true)
						{
								Vector3 newScale = transform.localScale - new Vector3(.1F, .1F, .1F);
								if (SanityChecker(newScale))
								{
										transform.localScale = newScale;
										m_RigidBody.advancedSettings.groundCheckDistance += -.1F;
										if ((m_Capsule.radius - .1F) <= .1f)
										{
												m_Capsule.radius = .1f;
										}
										else
										{
												m_Capsule.radius += -.1f;
										}
										// Height shouldn't change if we shrink (Breaks movement physics)
										if (m_Capsule.height != defaultHeight)
										{
												m_Capsule.height = defaultHeight;
										}
								}
								else
								{
										Debug.Log(newScale);
								}
						}
						else if (CrossPlatformInputManager.GetAxis("Scale3") != 0)
						{
								transform.localScale = new Vector3(1f, 1f, 1f);
								m_Capsule.radius = defaultRadius;
								m_RigidBody.advancedSettings.groundCheckDistance = defaultGroundCheck;
								m_Capsule.height = defaultHeight;
						}

						if (CrossPlatformInputManager.GetAxis("Fire3") == 0)
						{
								inAction = false;
						}
				}
			}

		void OnCollisionEnter(Collision collision)
		{
				if (collision.gameObject.tag == "PowerUp")
				{
					Destroy(collision.gameObject);
					maxScale = maxScale + .2F;
					minScale = minScale - .2F;
				}
		}
		 public bool SanityChecker(Vector3 newScale)
         {
            if (newScale.x >= maxScale)
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
    private IEnumerator Action()
    {
        while (pausable)
            yield return null;
    }

		private void CreateGunEffect(GameObject beamType, RaycastHit ray){
				beam = Instantiate(beamType, gunTip.transform.position, Quaternion.LookRotation(cam.transform.forward));
				beam.transform.localScale.Set(beam.transform.localScale.x, beam.transform.localScale.y, (ray.point - gunTip.transform.position).magnitude);
		}

		private void DestroyGunEffect(){
				Debug.Log("DESTROYING");
				Destroy(beam);
		}

		private void UpdateBeam(RaycastHit ray){
				Debug.Log("Updating beam");
				Debug.Log(ray.transform.localToWorldMatrix.MultiplyVector(ray.transform.forward));
				beam.transform.position = gunTip.transform.position;
				beam.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
				Debug.Log("magnitude: " + (ray.point - gunTip.transform.position).magnitude);
				beam.transform.localScale.Set(beam.transform.localScale.x, beam.transform.localScale.y, (ray.point - gunTip.transform.position).magnitude);
		}
}
