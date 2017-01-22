using UnityEngine;
using System.Collections;


public enum PlanetState {Planet, LargeAsteroid, SmallAsteroid};

public class Planet : MonoBehaviour {
		
	public int health = 30;
	public float scoreZoneSlowdown = 0.5f;
	public PlanetState state = PlanetState.Planet;
	public float spawnTime = 0.2f;
	public Vector3 spawnPosition;
	public GameObject LargeAsteroid;
	public GameObject SmallAsteroid;
	public GameObject RockExplosion;

	GameObject currentAsteroid;
	ScoreManager scoreManager; 
	bool beingDestroyed = false;
	Rigidbody rb;
	Vector3 InitialScale;

	AudioClip audioClip;
	public GameObject AudioOutputPrefab;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		rb.detectCollisions = false;
		InitialScale = gameObject.transform.localScale;
		Color tempColor = gameObject.GetComponent<MeshRenderer> ().material.color;
		tempColor.a = tempColor.a / 2;
		gameObject.GetComponent<MeshRenderer> ().material.color = tempColor;
		transform.position = new Vector3 (0, 0, 0);
		rb.angularVelocity = new Vector3(0,0,Random.Range (0.5f, 2f));
		StartCoroutine (Spawn ());

		//audioSource = GetComponent<AudioSource> ();
		AudioClip[] clips = GameObject.FindGameObjectWithTag ("Audio").GetComponent<AudioBank> ().planetEatingSounds;
		audioClip = clips[Random.Range(0,clips.Length)];
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
		if (health < 20 && health >10 && state != PlanetState.LargeAsteroid) {
			gameObject.GetComponent<MeshRenderer> ().enabled = false;
			state = PlanetState.LargeAsteroid;
			gameObject.transform.localScale = Vector3.one;

			currentAsteroid = (GameObject)Instantiate (LargeAsteroid, transform.position, Quaternion.identity);
			currentAsteroid.transform.localScale = new Vector3 (100, 100, 100);
			currentAsteroid.transform.parent = gameObject.transform;

			GameObject explosion = (GameObject)Instantiate (RockExplosion, transform.position + new Vector3(0,3,0), Quaternion.identity);
			Destroy (explosion, 5.0f);
			//currentAsteroid.transform.localPosition = Vector3.zero;
			GetComponent<SphereCollider> ().radius = 0.32f;

		} else if (health < 10 && state != PlanetState.SmallAsteroid) {
			state = PlanetState.SmallAsteroid;
			Destroy (currentAsteroid);
			currentAsteroid = (GameObject)Instantiate (SmallAsteroid, transform.position, Quaternion.identity);
			currentAsteroid.transform.parent = gameObject.transform;
			currentAsteroid.transform.localScale = new Vector3 (200, 200, 200);
			//currentAsteroid.transform.localPosition = Vector3.zero;
			GameObject explosion = (GameObject)Instantiate (RockExplosion, transform.position + new Vector3(0,3,0), Quaternion.identity);
			explosion.transform.localScale = explosion.transform.localScale * 0.5f;
			Destroy (explosion, 5.0f);
			GetComponent<SphereCollider> ().radius = 0.18f;
		}

		if (health == 0) {
			GameObject.FindGameObjectWithTag ("SpawnArea").GetComponent<PlanetSpawner> ().PlanetDestroyed ();
			GameObject explosion = (GameObject)Instantiate (RockExplosion, transform.position + new Vector3(0,3,0), Quaternion.identity);
			explosion.transform.localScale = explosion.transform.localScale * 0.25f;
			Destroy (explosion, 5.0f);

			GameObject obj = (GameObject)Instantiate(AudioOutputPrefab, transform.position, Quaternion.identity);
			obj.GetComponent<AudioSource> ().PlayOneShot (audioClip);
			Destroy (obj, 5.0F);

			Destroy (gameObject);
		}
	}
}

