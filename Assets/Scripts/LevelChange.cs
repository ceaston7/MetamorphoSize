using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
		public float waitTime;
		private bool firstTime = true;
		public int levelIndex;

    void OnTriggerEnter(Collider other){
				if(other.tag == "Player" && firstTime){
						firstTime = false;
						gameObject.GetComponent<AudioSource>().Play();
						StartCoroutine(Wait());
				}
		}

		IEnumerator Wait()
		{
				yield return new WaitForSeconds(waitTime);
				SceneManager.LoadScene(levelIndex);
		}
}
