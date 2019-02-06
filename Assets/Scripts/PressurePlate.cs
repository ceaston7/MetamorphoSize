using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    /* 
     * This can be improved, but it is a prototype and we have to throw away any code we use soooo
     *    
     * Collision event with either player or cube triggers bridge growth
     * Should disappear upon removal   
     * 
     * How to use:
     * 
     * Set bridge size = (x,y,0)
     * Position Z = center of pit
     * Change whether we want the bridge extending on x or z axis: 
     * change multipler location on line 38 to (multiplier,0,0)
     *    
     * Attach script to pressure plate
     * Bridge = bridge object
     * Multiplier = length of bridge
     */


    public Transform bridge;
    public float multiplier;
    private Vector3 originalBridge;

    private void Start()
    {
        originalBridge = bridge.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RigidBodyFPSController" || other.gameObject.name == "Cube")
        {
            bridge.transform.localScale += new Vector3(0, 0, multiplier);
            bridge.transform.position = bridge.transform.position + bridge.transform.forward;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        bridge.transform.localScale = originalBridge;
    }
}
