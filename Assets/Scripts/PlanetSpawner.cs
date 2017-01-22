using UnityEngine;
using System.Collections;

public class PlanetSpawner : MonoBehaviour
{

	public GameObject[] spawnObject;
	public float rateOfSpawn=0.5f; //spawn every 0.5 seconds
	private float timer=0;
	private float mintime=0.3f;
	public int MAXPLANETS =12;
	private int activePlanets = 0;
	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if(activePlanets<MAXPLANETS && timer>mintime){
			Vector3 rndPosWithin;
			rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
			GameObject obj = (GameObject)Instantiate(spawnObject[Random.Range(0,spawnObject.Length)], rndPosWithin, transform.rotation);
			obj.GetComponent<Planet> ().spawnPosition = rndPosWithin;
			activePlanets++;
			timer = 0;
		}
	}

	public void PlanetDestroyed()
	{
		activePlanets--;
	}

	public void resetPlanetTotal()
	{
		activePlanets = 0;
	}
}

