using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class LevelState : MonoBehaviour
    {
        private GameObject player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        public void Respawn()
        {
            Debug.Log("Begin Respawn");
            player.transform.position = player.GetComponent<PlayerState>().spawn.position;

            var objects = GameObject.FindGameObjectsWithTag("Object");
            foreach(var item in objects)
            {
                item.transform.position = item.GetComponent<ObjectState>().GetSpawnLocation();
                item.transform.localScale = item.GetComponent<ObjectState>().GetSpawnSize();
            }
        }

        public void Checkpoint()
        {
            Debug.Log("Checkpoint");
            player.GetComponent<PlayerState>().spawn.position = player.transform.position;

            var objects = GameObject.FindGameObjectsWithTag("Object");
            foreach (var item in objects)
            {
                item.GetComponent<ObjectState>().SetSpawn();
            }
        }
    }
}

