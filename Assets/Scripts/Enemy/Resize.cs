// MoveTo.cs
    using UnityEngine;
    using OurGame;
    using UnityEngine.AI;
    using System.Collections;
    using System;
    using System.Collections.Generic;
    
    public class Resize : MonoBehaviour {

       public float aggro_range;
       
       private float distToGround;

       public GameObject target;
       public Transform station;

       private RaycastHit hit;
       private GameObject proposed_target;

       private GameObject[] scaleables;
       private List<GameObject> possibleTargets;
       
       private NavMeshAgent agent;

       private GameObject[] player;

       private int ticker;

       private float target_weight;

       void Start () {
          Debug.Log("ResizeAI Script Enabled.");
          agent = GetComponent<NavMeshAgent>();
        //   agent.updateUpAxis = false;
          scaleables = GameObject.FindGameObjectsWithTag("Scaleable");
          target = null; 
          possibleTargets = new List<GameObject>();
          station = GetComponent<Transform>();
          ticker = 0;
          // There should only ever be one player object, thus fetching this tag should return 1D array.
          player = GameObject.FindGameObjectsWithTag("Player");
          aggro_range = 5000000000000;
       }

       void FixedUpdate() {
           ticker++;
           if(!IsGrounded() && agent.enabled)
           {
              Debug.Log("NOT GROUNDED! I CANNOT FUNCTION!");
              agent.enabled = false;
              Rigidbody rigid_body = GetComponent<Rigidbody>();
              rigid_body.useGravity = true;
              rigid_body.isKinematic = false;
              rigid_body.AddForce(0, 0, -1f);
           }
           else if (IsGrounded())
           {
                if (player[0].transform.localScale[0] != 1)
                {
                    //Debug.Log("Priority Target: Player Resize Detected!");
                    agent.destination = player[0].transform.position;
                    target = player[0];
                }
                else if (ticker > 100)
                {
                    possibleTargets = ComposeA(scaleables);
                    ticker = 0;
                }

                for (int i = 1; i <= possibleTargets.Count; i++)
                {
                    //Debug.Log(scaleables);
                    // Player is always priority
                    //Debug.Log(player[0]);
                    if (player[0].transform.localScale[0] != 1)
                    {
                        //Debug.Log("Priority Target: Player Resize Detected!");
                        agent.destination = player[0].transform.position;
                        target = player[0];
                    }
                    else if (possibleTargets[possibleTargets.Count - i].GetComponent<Scaler>().isScaled)
                    {
                        Debug.Log("Setting Target.");
                        if(target == null)
                        {
                            target = possibleTargets[possibleTargets.Count-i];
                            agent.destination = target.transform.position;
                        }
                        else if (target.tag == "Scaleable")
                        {
                            if (target.GetComponent<Scaler>().isScaled == false)
                            {
                                target = possibleTargets[possibleTargets.Count-i];
                                agent.destination = target.transform.position;
                            }
                        }
                        
                        if (ticker > 100)
                        {
                            possibleTargets = ComposeA(scaleables);
                            ticker = 0;
                            i = 1;
                        }
                        
                        // agent.destination = scaleables[i].transform.position;
                    }
                    else
                    {
                        if (ticker > 100)
                        {
                            possibleTargets = ComposeA(scaleables);
                            ticker = 0;
                            i = 1;
                        }
                        target = null;
                        agent.destination = station.position;
                    }
                }
           }
           else
           {
              Debug.Log("Adding Downward Force, Not grounded!");
              Rigidbody rigid_body = GetComponent<Rigidbody>();
              rigid_body.AddForce(0,0,-1f);
           }
       }
      
        bool IsGrounded() {
            return true;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (target != null && collision.gameObject != null)
            {
                Debug.Log("COLLIDING!");
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
        List<GameObject> ComposeA(GameObject[] A)
        {
            possibleTargets.Clear();
            for(int i = 0; i < A.Length; i++)
            {
                if(A[i].GetComponent<Scaler>().isScaled)
                {
                    possibleTargets.Add(A[i]);
                }
            }
            if(possibleTargets.Count > 1)
            {
                return quickSort(possibleTargets, 0, possibleTargets.Count-1);
            }
            else
            {
                if(possibleTargets.Count > 0)
                {
                    return possibleTargets;
                }
                else
                {
                    possibleTargets.Add(A[0]);
                    return possibleTargets;
                }
            }
        }
        List<GameObject> quickSort(List<GameObject> A, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = partition(A, lo, hi);
                A = quickSort(A, lo, p-1);
                A = quickSort(A, p+1, hi);
            }
            return A;
        }

        int partition(List<GameObject> A, int lo, int hi)
        {
            int mid = (lo + hi) / 2;
            if(Math.Abs(1 - A[hi].transform.localScale[0]) + Vector3.Distance(A[hi].transform.position, transform.position) < Math.Abs(1 - A[lo].transform.localScale[0]) + Vector3.Distance(A[lo].transform.position, transform.position))
            {
                GameObject ai = A[lo];
                A[lo] = A[hi];
                A[hi] = ai;
            }
            if(Math.Abs(1 - A[mid].transform.localScale[0]) + Vector3.Distance(A[mid].transform.position, transform.position) < Math.Abs(1 - A[lo].transform.localScale[0]) + Vector3.Distance(A[lo].transform.position, transform.position))
            {
                GameObject ai = A[mid];
                A[mid] = A[lo];
                A[lo] = ai;   
            }
            if(Math.Abs(1 - A[hi].transform.localScale[0]) + Vector3.Distance(A[hi].transform.position, transform.position) < Math.Abs(1 - A[mid].transform.localScale[0]) + Vector3.Distance(A[mid].transform.position, transform.position))
            {
                GameObject ai = A[mid];
                A[mid] = A[hi];
                A[hi] = ai;
            }
            GameObject re = A[hi];
            float pivot = Math.Abs(1 - re.transform.localScale[0]) + Vector3.Distance(re.transform.position, transform.position);
            // Debug.Log("Pivot Weight: " + pivot);
            int i = lo;
            int j = hi;
            while(true)
            {
                // Debug.Log("Length of Game Objects Array: " + A.Length);
                // Debug.Log("Value of i = " + i);
                // Debug.Log("Value of j = " + j);
                while((Math.Abs(1 - A[i].transform.localScale[0]) + Vector3.Distance(A[i].transform.position, transform.position)) < pivot)
                {

                    i = i + 1;
                    // Debug.Log("A[i] Test: " + A[i]);
                    if(i < A.Count)
                    {
                        i = i -1;
                        break;
                    }
                }
                while((Math.Abs(1 - A[j].transform.localScale[0]) + Vector3.Distance(A[j].transform.position, transform.position)) > pivot)
                {
                    j = j - 1;
                }
                if (i >= j)
                {
                    return j;
                }
                GameObject ai = A[i];
                GameObject aj = A[j];
                A[i] = aj;
                A[j] = ai;
                return i;
            }
        }
    }
