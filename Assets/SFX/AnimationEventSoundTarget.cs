using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSoundTarget : MonoBehaviour
{
		public AudioSource source;
		public AudioClip clip;

    public void PlaySound(){
				source.PlayOneShot(clip);
		}
}
