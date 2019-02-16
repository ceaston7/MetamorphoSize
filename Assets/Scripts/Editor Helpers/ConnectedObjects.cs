using UnityEngine;

/* Used to draw lines between objects in the editor for easy visualization of connections
 * Ex. Place this script on a pressure plate, and put the objects it activates in the array
 * of GameObjects
 */
public class ConnectedObjects : MonoBehaviour
{
		public GameObject[] objs = null;
		public Color color = Color.white;
}
