using UnityEngine;
using System.Collections;


public enum PlanetState {Planet, LargeAsteroid, SmallAsteroid};

public class Planet : MonoBehaviour {
		
	public int health = 30;
	public float scoreZoneSlowdown = 0.5f;
	public PlanetState state = PlanetState.Planet;
	public float spawnTime = 0.2f;
	public Vector3 spawnPosition;

	ScoreManager scoreManager; 
	bool beingDestroyed = false;
	Rigidbody rb;
	Vector3 InitialScale;
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		rb.detectCollisions = false;
		InitialScale = gameObject.transform.localScale;
		Color tempColor = gameObject.GetComponent<MeshRenderer> ().material.color;
		tempColor.a = tempColor.a / 2;
		gameObject.GetComponent<MeshRenderer> ().material.color = tempColor;
		transform.position = new Vector3 (0, 0, 0);
		StartCoroutine (Spawn ());
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

			if (timeElapsed >= 0.1) 
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

	IEnumerator Spawn()
	{
		float timeElapsed = 0;
		gameObject.transform.localScale = gameObject.transform.localScale * 0.0f;

		while (timeElapsed / spawnTime < 1) 
		{
			timeElapsed += Time.deltaTime;
			Vector3 tempScale = gameObject.transform.localScale;
			tempScale = Vector3.Lerp (Vector3.zero, InitialScale, timeElapsed / spawnTime);

			Vector3 tempLocation = gameObject.transform.position;
			tempLocation = Vector3.Lerp (Vector3.zero, spawnPosition, timeElapsed / spawnTime);
			gameObject.transform.position = tempLocation;

			Color tempColor = gameObject.GetComponent<MeshRenderer> ().material.color;
			tempColor.a = Mathf.Lerp(0.0f,1.0f,timeElapsed / spawnTime);
			gameObject.GetComponent<MeshRenderer> ().material.color = tempColor;

			gameObject.transform.localScale = tempScale;
			yield return null;
		}

		rb.detectCollisions = true;
	}

	void SetSize(int Health)
	{
		if (health < 20 && state != PlanetState.LargeAsteroid) {
			state = PlanetState.LargeAsteroid;
		} else if (health < 10 && state != PlanetState.SmallAsteroid) {
			state = PlanetState.SmallAsteroid;
		}

		if (health == 0) {
			GameObject.FindGameObjectWithTag ("SpawnArea").GetComponent<PlanetSpawner> ().PlanetDestroyed ();
			Destroy (gameObject);
		}
	}
}

