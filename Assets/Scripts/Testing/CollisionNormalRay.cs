using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNormalRay : MonoBehaviour
{
		public Color color = new Color(255,255,255,255);
		public bool printExit = true;

		void OnCollisionEnter(Collision collision)
		{
				print("Contacts: " + collision.GetContacts(collision.contacts));
				foreach (ContactPoint contact in collision.contacts)
				{
						print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
						print(contact.normal);
						Debug.DrawRay(contact.point, contact.normal * 10, color);
				}
		}
		/*
		void OnCollisionStay(Collision collision)
		{
				print("Stay contacts: " + collision.GetContacts(collision.contacts));
				foreach (ContactPoint contact in collision.contacts)
				{
						print(contact.thisCollider.name + " stayed " + contact.otherCollider.name);
						print(contact.normal);
						Debug.DrawRay(contact.point, contact.normal * 10, color);
				}
		}*/

		void OnCollisionExit(Collision collision)
		{
				print(collision.gameObject.name + " exited");
		}
}
