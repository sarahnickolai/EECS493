using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float leftBound;
	public float rightBound;
	public float upperBound;
	public float lowerBound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//float xpos = transform.position.x;
		//xpos += speed*Time.deltaTime;
		//if (xpos > rightBound) 
		//{
		//	xpos = rightBound;
		//	speed = -Mathf.Abs(speed);
		//} 
		//else if (xpos < leftBound) 
		//{
		//	xpos = leftBound;
		//	speed = Mathf.Abs(speed);
		//}
		//transform.rigidbody.velocity = new Vector3 (speed, 0, 0);
		//transform.position = new Vector3 (xpos, transform.position.y, transform.position.z);
	
		float xpos = transform.position.x;
		float ypos = transform.position.y;

		if (Input.GetKey (KeyCode.UpArrow))
		{
			ypos += speed*Time.deltaTime;
			if(ypos > upperBound)
			{
				ypos = upperBound;
			}
		} 

		if (Input.GetKey (KeyCode.DownArrow)) 
		{
			ypos -= speed*Time.deltaTime;
			if(ypos < lowerBound)
			{
				ypos = lowerBound;
			}
		}


		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			xpos += -speed*Time.deltaTime;
			if(xpos < leftBound)
			{
				xpos = leftBound;
			}
		}
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			xpos += speed*Time.deltaTime;
			if(xpos > rightBound)
			{
				xpos = rightBound;
			}
		}

		transform.position = new Vector3 (xpos, ypos, transform.position.z);
	}

	void OnTriggerEnter (Collider col)
	{

		if(col.gameObject.tag == "asteroid")
		{
			MeshCollider mesh = col.gameObject.GetComponent<MeshCollider>();
			mesh.isTrigger = true;

			MeshRenderer rend = col.gameObject.GetComponent<MeshRenderer>();
			rend.enabled = false;
			//col.gameObject.SetActive(false);
		}

		GameOver ();
	}

	private void GameOver()
	{
		Application.LoadLevel("Death");
	}
}
