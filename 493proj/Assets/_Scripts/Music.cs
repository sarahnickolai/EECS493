using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioClip backgroundMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		audio.clip = backgroundMusic;
		audio.Play ();
	}
}
