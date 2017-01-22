using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int[] playerScores;
	public Text[] playerScoreOutput;
	public int maxScore = 1000;
	public ScoreZone[] scoreZones;
	bool[] hasPlayed;
	AudioSource source;
	// Use this for initialization
	void Start () {
		hasPlayed = new bool[4];
		playerScores = new int[4];
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseScore(int playerID, int increaseValue)
	{
		if (playerID - 1 > -1) {
			playerScores [playerID - 1] += increaseValue;
			playerScoreOutput [playerID - 1].text = playerScores [playerID - 1].ToString();

			float percent = (float)playerScores [playerID - 1] / (float)maxScore;
			if (percent > 0.8 && hasPlayed[playerID-1] == false) {
				hasPlayed [playerID - 1] = true;
				source.PlayOneShot (GameObject.FindGameObjectWithTag ("Audio").GetComponent<AudioBank> ().blobble);
			}

			if (playerScores [playerID - 1] >= maxScore) {
				GameObject manager = GameObject.FindGameObjectWithTag ("Manager");
				if (manager != null) {
					manager.GetComponent<GameStateManager> ().EndGame (playerID);
				}
					
			}
		} else {
			Debug.Log ("Error, failed to zero index player ID correctly");
		}
			
	}

	public void ResetScores()
	{
		for (int cnt = 0; cnt < playerScores.Length; cnt++) {
			playerScores[cnt] = 0;
		}

		foreach (ScoreZone zone in scoreZones) {
			zone.ResetSpeed ();
		}
	}
}
