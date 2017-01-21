using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int[] playerScores;
	// Use this for initialization
	void Start () {
		playerScores = new int[4];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseScore(int playerID, int increaseValue)
	{
		if (playerID - 1 > -1) {
			playerScores [playerID - 1] += increaseValue;
		} else {
			Debug.Log ("Error, failed to zero index player ID correctly");
		}
	}
}
