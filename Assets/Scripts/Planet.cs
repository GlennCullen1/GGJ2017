using UnityEngine;
using System.Collections;


public enum PlanetState {Planet, LargeAsteroid, SmallAsteroid};

public class Planet : MonoBehaviour {
		
	public int health = 30;
	public float scoreZoneSlowdown = 0.5f;
	public PlanetState state = PlanetState.Planet;

	ScoreManager scoreManager; 
	bool beingDestroyed = false;
	Rigidbody rb;
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (beingDestroyed) 
		{
			rb.velocity = rb.velocity * scoreZoneSlowdown;
		}

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "ScoreZone" && beingDestroyed == false)
		{
			//rb.drag = scoreZoneSlowdown;
			//rb.angularDrag = scoreZoneSlowdown;
			if (scoreManager == null) 
			{
				scoreManager = other.gameObject.GetComponent<ScoreZone> ().scoreManager;
			}
			beingDestroyed = true;
			StartCoroutine(StartDestroying (other.gameObject.GetComponent<ScoreZone> ().playerID));

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "ScoreZone" && beingDestroyed == true) 
		{
			beingDestroyed = false;
		}
	}

	IEnumerator StartDestroying(int playerID) 
	{
		float timeElapsed = 0;

		while (beingDestroyed == true) 
		{

			timeElapsed += Time.deltaTime;

			if (timeElapsed >= 1) 
			{
				health -= 1;
				SetSize (health);
				scoreManager.IncreaseScore (playerID, 1);

				if (health <= 0) 
				{
					beingDestroyed = false;
				}
				timeElapsed = 0;
			}

			yield return null;
		}
	}

	void SetSize(int Health)
	{
		if (health < 20 && state != PlanetState.LargeAsteroid) {
			state = PlanetState.LargeAsteroid;
		} else if (health < 10 && state != PlanetState.SmallAsteroid) {
			state = PlanetState.SmallAsteroid;
		}
	}
}

