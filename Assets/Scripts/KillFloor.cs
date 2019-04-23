using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class KillFloor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered Kill Floor");
            switch(other.gameObject.tag)
            {
                case "Player":
                    GameObject level = GameObject.Find("LevelState");
                    level.GetComponent<LevelState>().Respawn();
                    break;
                case "Object":
								case "Scaleable":
                    other.transform.position = other.GetComponent<ObjectState>().GetSpawnLocation();
                    other.transform.localScale = other.GetComponent<ObjectState>().GetSpawnSize();
                    break;
                case "Enemy":
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}

