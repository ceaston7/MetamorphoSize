using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class Checkpoint : MonoBehaviour
    {
        private GameObject levelState;
				private bool firstTime = true;

        private void Start()
        {
            levelState = GameObject.Find("LevelState");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(firstTime && other.CompareTag("Player"))
            {
                levelState.GetComponent<LevelState>().Checkpoint();
								firstTime = false;
            }
        }
    }
}

