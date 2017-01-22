using UnityEngine;
using System.Collections;

public class ZoneScale : MonoBehaviour
{
	public int playerId = 0;
	ScoreManager scoreManager;
	public GameObject circle;
	// Use this for initialization
	void Start ()
	{
		circle = gameObject;
		scoreManager = gameObject.transform.parent.parent.gameObject.GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float t = (((float)scoreManager.playerScores [playerId - 1] / (scoreManager.maxScore)));
		Vector3 newScale =  Vector3.Lerp(Vector3.zero,Vector3.one,t);
		circle.transform.localScale = newScale;
	}
}

