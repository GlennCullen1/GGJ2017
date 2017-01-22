using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour
{
	public int playerID;
	public ScoreManager scoreManager;
	public SpritePlayer sprite;
	public float spriteIncreasePerPlannet = 0.5f;
	// Use this for initialization
	void Start ()
	{
		scoreManager = transform.parent.gameObject.GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Planet") {
			sprite.framesPerSecond += spriteIncreasePerPlannet;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Planet") {
			sprite.framesPerSecond -= spriteIncreasePerPlannet;
		}
	}

	public void ResetSpeed()
	{
		sprite.framesPerSecond = 3;
	}

}

