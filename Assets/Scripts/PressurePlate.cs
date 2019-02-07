using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    /* 
     * This can be improved, but it is a prototype and we have to throw away any code we use soooo
     *    
     * Collision event with either player or an object tagged "object" activates the bridge
     * Should disappear upon removal   
     * 
     * How to use;
     *    
     * Attach script to pressure plate
     * Bridge = bridge object
     */

    public GameObject Bridge;

    private void Start()
    {
        Bridge.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player" || other.tag == "object")
        {
            Bridge.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Bridge.SetActive(false);
    }
}
