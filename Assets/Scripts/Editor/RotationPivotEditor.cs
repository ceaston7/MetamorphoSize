using UnityEditor;
using UnityEngine;

/**
 * Custom editor for RotationPivot 
 */
[CustomEditor(typeof(RotationPivot))]
public class RotationPivotEditor : Editor {

    /**
     * Gets called everytime the scene GUI is redrawn. Only if the gameObject is slected in the inspector
     */
    void OnSceneGUI()
    {
        //The RotationPivot component of the selected game object
        RotationPivot rotationPivot = (RotationPivot)target;

        //Return if rotationPivot has no target
        if(rotationPivot.target == null)
        {
            return;
        }

        //Display a RotationHandle with the position and rotation of rotationPivot and store its value inside newRotation
        Quaternion newRoation = Handles.RotationHandle(
            rotationPivot.transform.rotation, 
            rotationPivot.transform.position
        );

        //Continue only if the new rotation differs from the current rotation
        if (newRoation != rotationPivot.transform.rotation)
        {
            //Register a undo action both on the rotationPivot and its target
            Undo.RecordObject(rotationPivot.transform, "Rotate by pivot");
            Undo.RecordObject(rotationPivot.target, "Rotate by pivot");

            //Apply the new rotation
            rotationPivot.SetRotation(newRoation);
        }
    }
}
 