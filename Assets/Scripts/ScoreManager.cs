using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int[] playerScores;
	public Text[] playerScoreOutput;

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
			playerScoreOutput [playerID - 1].text = playerScores [playerID - 1].ToString();
			if (playerScores [playerID - 1] >= 1000) {
				GameObject manager = GameObject.FindGameObjectWithTag ("Manager");
				if (manager != null) {
					manager.GetComponent<GameStateManager> ().EndGame (playerID);
				}
			}
		} else {
			Debug.Log ("Error, failed to zero index player ID correctly");
		}
	}
}
