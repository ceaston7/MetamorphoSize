using UnityEditor;
using UnityEngine;

//Draws lines between connected objects when object is selected in editor
[CustomEditor(typeof(ConnectedObjects))]
class DrawConnections : Editor
{
		void OnSceneGUI()
		{
				ConnectedObjects connectedObjects = target as ConnectedObjects;
				if (connectedObjects.objs == null)
						return;

				Handles.color = connectedObjects.color;

				Vector3 center = connectedObjects.transform.position;
				for (int i = 0; i < connectedObjects.objs.Length; i++)
				{
						GameObject connectedObject = connectedObjects.objs[i];
						if (connectedObject)
						{
								Handles.DrawLine(center, connectedObject.transform.position);
						}
						else
						{
								Handles.DrawLine(center, Vector3.zero);
						}
				}
		}
}