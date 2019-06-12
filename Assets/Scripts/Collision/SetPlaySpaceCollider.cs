using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SetPlaySpaceCollider : MonoBehaviour
{
    private BoxCollider collider;
    public CapsuleCollider capsule;
    public Transform head;

    void Start()
    {
        try
        {
            collider = gameObject.GetComponent<BoxCollider>();
        }
        catch
        {
            collider = gameObject.AddComponent<BoxCollider>();
        }

        if(capsule == null)
        {
            capsule = GameObject.Find("BodyCollider").GetComponent<CapsuleCollider>();
        }

        collider.size = new Vector3(capsule.radius * 2, head.localPosition.y, capsule.radius * 2);
        collider.center = new Vector3(head.localPosition.x, head.localPosition.y * 0.5f, head.localPosition.z);
    }

    void LateUpdate()
    {
        collider.size = new Vector3(capsule.radius * 2, head.localPosition.y, capsule.radius * 2);
        collider.center = new Vector3(head.localPosition.x, head.localPosition.y * 0.5f, head.localPosition.z);
    }
}
