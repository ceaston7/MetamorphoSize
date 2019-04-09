using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class Checkpoint : MonoBehaviour
    {
        private GameObject levelState;

        private void Start()
        {
            levelState = GameObject.Find("LevelState");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                levelState.GetComponent<LevelState>().Checkpoint();
            }
        }
    }
}

