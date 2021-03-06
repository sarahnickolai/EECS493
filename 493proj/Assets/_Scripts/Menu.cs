﻿using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HighScore HS = HighScore.getInstance();
		HS.loadFromFile ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.skin.label.fontSize = 78;
		var title1 = "ASTEROID";
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect(Screen.width/2.0f-200, Screen.height*.3f-250, 400, 500), title1);

		GUI.skin.label.fontSize = 78;
		var title2 = "TERROR";
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect(Screen.width/2.0f-175, Screen.height/2.0f-150, 350, 300), title2);

		GUI.skin.label.fontSize = 14;
		var instr = "Use the arrow keys to move your ship and dodge asteroids!";
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect(Screen.width/2.0f-200, Screen.height*.65f-150, 400, 300), instr);

		GUI.skin.button.alignment = TextAnchor.MiddleCenter;
		if (GUI.Button (new Rect (Screen.width/2.0f-100, Screen.height*.7f+20, 200, 40), "Start Game"))
		{
			Application.LoadLevel("Game");
		}
		
		if (GUI.Button (new Rect (Screen.width/2.0f-100, Screen.height*.8f+20, 200, 40), "Exit"))
		{
			Application.Quit ();
		}
	}
}
