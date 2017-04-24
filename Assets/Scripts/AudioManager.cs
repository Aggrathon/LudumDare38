using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance { get; protected set; }

	AudioSource source;

	void Awake () {
		instance = this;
		source = GetComponent<AudioSource>();
	}
	

	public static void PlayAt(AudioClip clip, Vector3 pos)
	{
		instance.transform.position = pos;
		instance.source.PlayOneShot(clip);
	}
}
