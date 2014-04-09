using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{

		//GUI.Box (new Rect (10, 10, 200, 150), "Main Menu");
		if (GUI.Button (new Rect (20,200,Screen.width - 100,40), "Play Asteroid Dodge"))
		{
			Application.LoadLevel("Game");
		}

		if (GUI.Button (new Rect (20,260,Screen.width - 100,40), "Exit"))
		{
			Application.Quit ();
		}
	}
}
