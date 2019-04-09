using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class ObjectState : MonoBehaviour
    {
        public bool UpdatePosition;
        private Vector3 SpawnLoc;
        private Vector3 SpawnSize;

        private void Start()
        {
            SpawnLoc = transform.position;
            SpawnSize = GetComponent<Renderer>().bounds.size;
        }

        public Vector3 GetSpawnLocation()
        {
            return SpawnLoc;
        }

        public Vector3 GetSpawnSize()
        {
            return SpawnSize;
        }

        public void SetSpawn()
        {
            if(UpdatePosition)
            {
                SpawnLoc = transform.position;
                SpawnSize = GetComponent<Renderer>().bounds.size;
            }
        }
    }
}
