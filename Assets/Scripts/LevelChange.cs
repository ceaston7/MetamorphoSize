﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
				if(other.tag == "Player"){
						SceneManager.LoadScene("Level2");
				}
		}
}
