using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpikeContact : MonoBehaviour
{
    /* 
     * Add this script to the "spikes"
     * 
     * To use this script:
     * 
     * Add it to the "spike" object
     * Cube = cube
     * 
     * Tag cubes/things that will fall and you want to respawn as "object"
     * Tag the First Person Controller as "player"
     * Tag enemies as "enemy"
    */

    public GameObject Cube;
    private Vector3 StartPosition;

    private void Start()
    {
        StartPosition = Cube.transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Object":
                other.transform.parent = null;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.transform.position = StartPosition;
                break;
            case "Enemy":
                Destroy(other.gameObject);
                break;
        }
    }
}
