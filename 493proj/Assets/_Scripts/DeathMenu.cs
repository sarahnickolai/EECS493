using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		
		//GUI.Box (new Rect (10, 10, 200, 150), "Main Menu");
		if (GUI.Button (new Rect (20,200,Screen.width - 100,40), "You died! Click here to return to main menu!"))
		{
			Application.LoadLevel("Title");
		}
		
		if (GUI.Button (new Rect (20,260,Screen.width - 100,40), "Exit"))
		{
			Application.Quit ();
		}
	}
}
