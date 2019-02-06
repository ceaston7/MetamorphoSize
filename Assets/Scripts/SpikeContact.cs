using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeContact : MonoBehaviour
{
    /* 
     * Add this script to the "spikes"
     * 
     * This is hardcoded and we can make it "better" later,
     * but set Cube to be the cube that can combine with the spikes
     * (This will be the one on top of the platform)
     * 
     * Cube must be named "Cube"
     *    
     * If the player comes into contact with a spike,
     * the level will be reset
     *    
     * If the cube comes into contact with a spike,
     * it will be moved back to its original position
    */

    public Transform Cube;
    Vector3 CubeStartPosition;

    void Start()
    {
        CubeStartPosition = new Vector3(Cube.transform.position.x, Cube.transform.position.y, Cube.transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FPSController" || other.gameObject.name == "RigidBodyFPSController")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } 
        else if(other.gameObject.name == "Cube")
        {
            Cube.transform.parent = null;
            Cube.GetComponent<Rigidbody>().useGravity = true;
            Cube.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.transform.position = CubeStartPosition;
        }
    }
}
