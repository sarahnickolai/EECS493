using UnityEngine;
using System.Collections;

public class PlanetRotate : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Vector3 center = transform.position + transform.localScale / 2;
		transform.RotateAround (transform.position, new Vector3 (1, 1, 0), rotationSpeed);
	}
}
