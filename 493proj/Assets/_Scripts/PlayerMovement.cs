using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float leftBound;
	public float rightBound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xpos = transform.position.x;
		xpos += speed*Time.deltaTime;
		if (xpos > rightBound) 
		{
			xpos = rightBound;
			speed = -Mathf.Abs(speed);
		} 
		else if (xpos < leftBound) 
		{
			xpos = leftBound;
			speed = Mathf.Abs(speed);
		}
		//transform.rigidbody.velocity = new Vector3 (speed, 0, 0);
		transform.position = new Vector3 (xpos, transform.position.y, transform.position.z);
	}
}
