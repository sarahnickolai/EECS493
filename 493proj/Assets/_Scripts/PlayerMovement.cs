using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed;
	public float moveAngle;
	public float angleSensitivity;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float leftBound = GameLogic.LeftBound ();
		float rightBound = GameLogic.RightBound ();
		float topBound = GameLogic.TopBound ();
		float bottomBound = GameLogic.BottomBound ();


		float xpos = transform.position.x;
		float ypos = transform.position.y;
		
		Vector3 desiredOrientation = new Vector3 ();
		
		int xInput = (ToInt (Input.GetKey (KeyCode.UpArrow)) - ToInt (Input.GetKey (KeyCode.DownArrow)));
		int yInput = (ToInt (Input.GetKey (KeyCode.RightArrow)) - ToInt (Input.GetKey (KeyCode.LeftArrow)));
		
		ypos += xInput * speed*Time.deltaTime;
		xpos += yInput * speed*Time.deltaTime;
		
		desiredOrientation.x = - xInput * moveAngle;
		desiredOrientation.z = - yInput * moveAngle;

		//Camera camera = Camera.main;
		//Vector3 cameraPos = earth.transform.position;
		
		// Check Boundary
		if(ypos > topBound)
		{
			ypos = topBound;
		}
		if(ypos < bottomBound)
		{
			ypos = bottomBound;
		}
		if(xpos < leftBound)
		{
			//camera.transform.RotateAround(cameraPos, new Vector3(0,0,-1), -1.0f);
			xpos = leftBound;
		}
		if(xpos > rightBound)
		{
			//camera.transform.RotateAround(cameraPos, new Vector3(0,0,-1), 1.0f);
			xpos = rightBound;
		}
		
		transform.position = new Vector3 (xpos, ypos, transform.position.z);
		Vector3 curRot = transform.eulerAngles;
		if (curRot.x > 180) curRot.x -= 360;
		if (curRot.y > 180) curRot.y -= 360;
		if (curRot.z > 180) curRot.z -= 360;
		
		float percent = angleSensitivity * Time.deltaTime;
		
		Vector3 rot = (1-percent) * curRot + percent * desiredOrientation;
		if (rot.x < 0) rot.x += 360;
		if (rot.y < 0) rot.y += 360;
		if (rot.z < 0) rot.z += 360;
		transform.eulerAngles = rot;
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
		
		GameLogic.that.GameOver ();
	}
	
	int ToInt(bool val)
	{
		if (val) return 1;
		return 0;
	}
}
