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

       private GameObject[] player;
       void Start () {
          agent = GetComponent<NavMeshAgent>();
          scaleables = GameObject.FindGameObjectsWithTag("Scaleable");
          target = null; 
          // There should only ever be one player object, thus fetching this tag should return 1D array.
          player = GameObject.FindGameObjectsWithTag("Player");
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
                   if (player[0].transform.localScale[0] > 1)
                   {
                       Debug.Log("Priority Target: Player Resize Detected!");
                       agent.destination = player[0].transform.position;
                       target = player[0];
                   }
                   else if (scaleables[i].GetComponent<Scaler>().isScaled && (target == null))
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
                if ((collision.gameObject == target) && (target.tag != "Player"))
                {
                    Debug.Log("Target Located, Setting to Default Scale.");
                    Scaler scaler = collision.gameObject.GetComponent<Scaler>();
                    scaler.transform.localScale = scaler.defaultScale;
                    scaler.isScaled = false;
                    target = null;
                }
                else if ((collision.gameObject == target) && (target.tag == "Player"))
                {
                    GameObject player = collision.gameObject;
                    player.transform.localScale = new Vector3(1, 1, 1);
                    PlayerControllerScale script = player.GetComponent<PlayerControllerScale>();
                    script.m_Capsule.radius = script.defaultRadius;
                    script.m_RigidBody.advancedSettings.groundCheckDistance = script.defaultGroundCheck;
            		script.m_Capsule.height = script.defaultHeight;
                    target = null;
                }
            }
        }
    }
