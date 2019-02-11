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

		public IActivate activate;
		public Material onMat;
		public Material offMat;
		private Renderer render;
		private uint count; //Tracks how many objects are on top of plate


    private void Start()
    {
				activate.DeactivateMe();
				render = GetComponent<Renderer>();
				count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
				activate.ActivateMe();
				render.material = onMat;
				count++;
    }
    private void OnTriggerExit(Collider other)
    {
				count--;
				if (count == 0)
				{
						activate.DeactivateMe();
						render.material = offMat;
				}
    }
}
