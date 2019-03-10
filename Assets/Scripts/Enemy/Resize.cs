// MoveTo.cs
    using UnityEngine;
    using OurGame;
    using UnityEngine.AI;
    using System.Collections;
    
    public class Resize : MonoBehaviour {
       
       private float distToGround;

       private GameObject target;

       private GameObject[] scaleables;
       
       private NavMeshAgent agent;
       void Start () {
          agent = GetComponent<NavMeshAgent>();
          scaleables = GameObject.FindGameObjectsWithTag("Scaleable");
          target = null; 
       }

       void FixedUpdate() {
           if(!IsGrounded() && agent.enabled)
           {
              agent.enabled = false;
              Rigidbody rigid_body = GetComponent<Rigidbody>();
              rigid_body.useGravity = true;
              rigid_body.isKinematic = false;
              rigid_body.AddForce(0, 0, -1f);
           }
           else if (IsGrounded())
           {
               for (int i = 0; i < scaleables.Length; i++)
               {
                    Debug.Log(scaleables);

                   if (scaleables[i].GetComponent<Scaler>().isScaled && (target == null))
                   {
                       Debug.Log("Setting Target.");
                       agent.destination = scaleables[i].transform.position;
                       target = scaleables[i];
                   }
               }
           }
           else
           {
              Rigidbody rigid_body = GetComponent<Rigidbody>();
              rigid_body.AddForce(0,0,-1f);
           }
       }
      
        bool IsGrounded() {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 5f);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (target != null)
            {
                if (collision.gameObject == target)
                {
                    Debug.Log("Target Located, Setting to Default Scale.");
                    Scaler scaler = collision.gameObject.GetComponent<Scaler>();
                    scaler.transform.localScale = scaler.defaultScale;
                    scaler.isScaled = false;
                    target = null;
                }
            }
        }
    }
