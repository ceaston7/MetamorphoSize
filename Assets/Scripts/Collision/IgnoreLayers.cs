using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayers : MonoBehaviour
{
    public int layerStart;
    public int layerEnd;

    void Awake()
    {
        Physics.IgnoreLayerCollision(layerStart, layerEnd);
    }
}
