using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    public GameObject Player;
    public int BridgeDistance;
    private Vector3 Position;

    void Start()
    {
        Position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Pace());
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < BridgeDistance)
        {
            GetComponent<Rigidbody>().useGravity = true;
            transform.LookAt(Player.transform);
            transform.position += transform.forward * 2 * Time.deltaTime;
        }
        else
        {
            Position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            /*if(WaitTime > 0)
            {
                WaitTime -= Time.deltaTime;
                return;
            }

            GetComponent<Rigidbody>().useGravity = false;

            Vector3 pacer = Position;
            pacer.x += .1f * Mathf.Sin(Time.time) * .5f;
            transform.position = pacer;

            if(StartPosition.x.Equals(pacer.x) || StartPosition.x.Equals(pacer.x - .1f))
            {
                WaitTime = .5f;
            }*/


            StartCoroutine(Pace());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator Pace()
    {
        GetComponent<Rigidbody>().useGravity = false;

        Vector3 pacer = Position;
        pacer.z += .1f * Mathf.Sin(Time.time) * .2f;
        transform.position = pacer;
        yield return new WaitForSeconds(.5f);
    }

}
