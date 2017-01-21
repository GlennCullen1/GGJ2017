using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour
{
	public int playerID;
	public ScoreManager scoreManager;
	// Use this for initialization
	void Start ()
	{
		scoreManager = transform.parent.gameObject.GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

