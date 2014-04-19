using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public float spawnTopExtra;
	public float spawnSideExtra;
	public float despawnBottomExtra;

	public float timeBetweenAsteroids;
	public float asteroidSpeed;
	public float asteroidSize;
	public float asteroidSizeOffset;
	public float curveVal;
	public int score;

	public AudioClip thud;

	private int numOnScreen;
	private List<GameObject> asteroids;
	private GameObject asteroid;
	private float timeToNextAsteroid;
	private bool gameOver;

	static public GameLogic that;

	// Use this for initialization
	void Start () {
		that = this;

		gameOver = false;

		numOnScreen = 1;
		timeToNextAsteroid = timeBetweenAsteroids;

		asteroid = Resources.Load<GameObject>("Asteroid");

		asteroids = new List<GameObject> ();
		for (int i = 0; i < numOnScreen; ++i)
		{
			Create ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeToNextAsteroid -= Time.deltaTime;

		if (timeToNextAsteroid < 0) 
		{
			timeToNextAsteroid = timeBetweenAsteroids + 3*Random.value;
			Create();
		}

		float bottomDespawn = BottomBound () - despawnBottomExtra;

		foreach (GameObject obj in asteroids) 
		{
			Vector3 vel = obj.transform.rigidbody.velocity;
			vel = (1-curveVal)*vel.normalized + curveVal*(new Vector3(0,-1,0));
			obj.transform.rigidbody.velocity = vel * asteroidSpeed;

			if(obj.transform.position.y < bottomDespawn)
			{
				Reset(obj);
				score++;
			}
		}
	}

	void OnGUI()
	{
		if (!gameOver) 
		{
			GUI.skin.label.fontSize = 20;
			var scoreText = "Asteroids Dodged: " + score;
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label (new Rect (20, 20, 300, 30), scoreText);

			HighScore.getInstance().loadFromFile();

			GUI.skin.label.fontSize = 20;
			var highScoreText = "High Score: " + HighScore.getInstance().getScore();
			GUI.skin.label.alignment = TextAnchor.UpperRight;
			GUI.Label (new Rect (Screen.width - 220, 20, 200, 30), highScoreText);
		}

		if (gameOver) 
		{
			int oldHighScore = HighScore.getInstance().getScore();

			GUI.skin.label.fontSize = 35;
			var gameOverText = "Game Over!";
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect(Screen.width*0.5f-200, Screen.height*0.25f-200, 400, 400), gameOverText);

			GUI.skin.label.fontSize = 35;
			var highScoreText = "Old High Score: ";
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect(Screen.width*0.5f-200, Screen.height*0.4f-200, 400, 400), highScoreText+""+oldHighScore);

			GUI.skin.label.fontSize = 35;
			var yourScoreText = "Your Score: ";
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect(Screen.width*0.5f-200, Screen.height*0.5f-200, 400, 400), yourScoreText+""+score);

			if(score > oldHighScore)
			{
				GUI.skin.label.fontSize = 35;
				var newHighScore = "New High Score Of: ";
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label (new Rect(Screen.width/2.0f-200, Screen.height*.53f+50, 400, 100), newHighScore+""+score);
			}

			if (GUI.Button (new Rect (Screen.width/2.0f-100, Screen.height/2.0f+150, 200, 40), "Return to main menu"))
			{
				HighScore.getInstance().addNewScore(score);
				HighScore.getInstance().writeToFile();

				Time.timeScale = 1.0f;
				gameOver = false;
				Application.LoadLevel("Title");
			}
		}
	}

	public void PlayCollide ()
	{
		audio.PlayOneShot (thud);
	}

	private void Reset (GameObject obj)
	{
		Vector3 angVel = new Vector3 (Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
		angVel *= 2.0f;

		float randPos = Random.Range(LeftBound() - spawnSideExtra, RightBound() - spawnSideExtra);
		Vector3 pos = new Vector3 (randPos, TopBound() + spawnTopExtra + Random.value*2, -20);

		float randVel = (Random.value - 0.5f) * 2.0f;
		Vector3 vel = new Vector3 (randVel, -0.5f, 0);
		vel.Normalize ();

		Vector3 size = new Vector3 (Random.value*2.0f - 1.0f, Random.value*2.0f - 1.0f, Random.value*2.0f - 1.0f);
		size *= asteroidSizeOffset;
		size += new Vector3 (asteroidSize, asteroidSize, asteroidSize);

		obj.transform.rigidbody.velocity = vel * asteroidSpeed;
		obj.transform.position = pos;
		obj.transform.rigidbody.angularVelocity = angVel;
		obj.transform.localScale = size;

		MeshCollider col = obj.GetComponent<MeshCollider>();
		col.isTrigger = false;
		
		MeshRenderer rend = obj.GetComponent<MeshRenderer>();
		rend.enabled = true;
	}

	private void Create()
	{
		Vector3 pos = new Vector3 ((LeftBound() + RightBound())/2.0f, TopBound(), -20);
		GameObject rock = Instantiate (asteroid, pos, Quaternion.identity) as GameObject;
		Reset (rock);
		
		asteroids.Add(rock);
	}

	public void GameOver()
	{
		Time.timeScale = 0.0f;
		gameOver = true;
	}



	public static float LeftBound()
	{
		float camToObjZ = Mathf.Abs (Camera.main.transform.position.z - (-20));
		return Camera.main.ScreenToWorldPoint (new Vector3 (0,0,camToObjZ)).x;
	}

	public static float RightBound()
	{
		float camToObjZ = Mathf.Abs (Camera.main.transform.position.z - (-20));
		return Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width,0,camToObjZ)).x;
	}

	public static float TopBound()
	{
		float camToObjZ = Mathf.Abs (Camera.main.transform.position.z - (-20));
		return Camera.main.ScreenToWorldPoint (new Vector3 (0,Screen.height,camToObjZ)).y;
	}
	
	public static float BottomBound()
	{
		float camToObjZ = Mathf.Abs (Camera.main.transform.position.z - (-20));
		return Camera.main.ScreenToWorldPoint (new Vector3 (0,0,camToObjZ)).y;
	}

}
