using UnityEngine;

public class RotationPivot: MonoBehaviour
{
    /**
     * The transform of the object you want to rotate with this pivot 
     */
    public Transform target;

    
    public void SetRotation(Quaternion newRotation)
    {
        //We store current parents of pivot and target
        Transform oldPivotParent = transform.parent;
        Transform oldTargetParent = target.transform.parent;

        RemoveParents();

        //Set the pivot as the parent of target
        target.transform.SetParent(transform);

        //Apply new rotation to pivot. Target follows since it has the pivot as parent now
        transform.rotation = newRotation;

        RemoveParents();

        transform.SetParent(oldPivotParent);
        target.transform.SetParent(oldTargetParent);
    }

    /*
    * Reset parent of target and pivot. We need this to make both switchable
    */
    private void RemoveParents()
    {
        target.transform.SetParent(null);
        transform.SetParent(null);
    }
}