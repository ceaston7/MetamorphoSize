using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BridgeBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
