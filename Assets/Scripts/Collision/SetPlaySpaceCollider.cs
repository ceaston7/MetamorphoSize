using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SetPlaySpaceCollider : MonoBehaviour
{
    private CapsuleCollider collider;
    public CapsuleCollider capsule;
    public Transform head;

    void Start()
    {
        try
        {
            collider = gameObject.GetComponent<CapsuleCollider>();
        }
        catch
        {
            collider = gameObject.AddComponent<CapsuleCollider>();
        }

        if(capsule == null)
        {
            capsule = GameObject.Find("BodyCollider").GetComponent<CapsuleCollider>();
        }

        collider.radius = capsule.radius;
        collider.height = capsule.height;
        collider.center = new Vector3(head.localPosition.x, head.localPosition.y * 0.5f, head.localPosition.z);
    }

    void LateUpdate()
    {
        collider.radius = capsule.radius;
        collider.height = capsule.height;
        collider.center = new Vector3(head.localPosition.x, head.localPosition.y * 0.5f, head.localPosition.z);
    }
}
