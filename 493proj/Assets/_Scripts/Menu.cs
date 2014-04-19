using UnityEngine;
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

		if (GUI.Button (new Rect (Screen.width/2.0f-100, Screen.height/2.0f+120, 200, 40), "Start Game"))
		{
			Application.LoadLevel("Game");
		}
		
		if (GUI.Button (new Rect (Screen.width/2.0f-100, Screen.height/2.0f+165, 200, 40), "Exit"))
		{
			Application.Quit ();
		}
	}
}
