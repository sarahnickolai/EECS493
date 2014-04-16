﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public float spawnTop;
	public float spawnLeftBound;
	public float spawnRightBound;
	public float despawnBottom;
	public float timeBetweenAsteroids;
	public float asteroidSpeed;
	public float curveVal;
	public float score;

	private int numOnScreen;
	private List<GameObject> asteroids;
	private GameObject asteroid;
	private float timeToNextAsteroid;

	// Use this for initialization
	void Start () {
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
			timeToNextAsteroid = timeBetweenAsteroids + Random.value;
			Create();
		}

		foreach (GameObject obj in asteroids) 
		{
			Vector3 vel = obj.transform.rigidbody.velocity;
			vel = (1-curveVal)*vel.normalized + curveVal*(new Vector3(0,-1,0));
			obj.transform.rigidbody.velocity = vel * asteroidSpeed;

			if(obj.transform.position.y < despawnBottom)
			{
				Reset(obj);
				score++;
			}
		}
	}

	void OnGUI()
	{
		GUI.skin.label.fontSize = 20;
		var scoreText = "Asteroids Dodged: " + score;
		GUI.Label (new Rect (20, 20, 300, 30), scoreText);
	}

	private void Reset (GameObject obj)
	{
		Vector3 angVel = new Vector3 (Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
		angVel.Normalize ();

		float randPos = Random.Range(spawnLeftBound, spawnRightBound);

		Vector3 pos = new Vector3 (randPos, spawnTop, -20);

		float randVel = (Random.value - 0.5f) * 2.0f;

		Vector3 vel = new Vector3 (randVel, -0.5f, 0);
		vel.Normalize ();

		obj.transform.rigidbody.velocity = vel * asteroidSpeed;
		obj.transform.position = pos;
		obj.transform.rigidbody.angularVelocity = angVel;

		MeshCollider col = obj.GetComponent<MeshCollider>();
		col.isTrigger = false;
		
		MeshRenderer rend = obj.GetComponent<MeshRenderer>();
		rend.enabled = true;
	}

	private void Create()
	{
		Vector3 pos = new Vector3 ((spawnLeftBound + spawnRightBound)/2.0f, spawnTop, -20);
		GameObject rock = Instantiate (asteroid, pos, Quaternion.identity) as GameObject;
		Reset (rock);
		
		asteroids.Add(rock);
	}
}
