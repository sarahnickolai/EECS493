using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;

public class HighScore {
	public int score;

	private static HighScore instance;

	private HighScore(){}

	public static HighScore getInstance()
	{
		if (instance == null)
		{
			instance = new HighScore();
		}
		return instance;
	}

	public void loadFromFile()
	{
		FileInfo source = new FileInfo ("Assets/Resources/scores.txt");
		StreamReader reader = source.OpenText ();

		string text = reader.ReadLine();
		score = int.Parse(text);

		reader.Close ();
	}

	public void writeToFile()
	{
		StreamWriter writer = new StreamWriter("Assets/Resources/scores.txt");

		writer.WriteLine (score);

		writer.Close ();
	}

	public int getScore()
	{
		return score;
	}

	public void addNewScore(int newScore)
	{
		if (newScore <= score)
			return;

		score = newScore;
	}
}
