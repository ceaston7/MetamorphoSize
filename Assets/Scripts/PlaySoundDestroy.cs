using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundDestroy : MonoBehaviour
{
		public AudioSource source;

		void OnDestroy(){
				source.Play();
		}
}
