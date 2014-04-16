using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioClip backgroundMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Awake () {
		if (!audio.isPlaying)
		{
			audio.clip = backgroundMusic;
			audio.Play ();
		}
	}
}
