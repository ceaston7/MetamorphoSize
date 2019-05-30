using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitRigidBody : MonoBehaviour
{
		public Rigidbody body;
		public Vector3 initVelocity = new Vector3(0f,0f,0f);
    // Start is called before the first frame update
    void Start()
    {
				body.velocity = initVelocity;
    }
}
