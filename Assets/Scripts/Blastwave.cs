using UnityEngine;
using System.Collections;

public class Blastwave : MonoBehaviour
{
	SpriteRenderer blastwaveSprite;
	public float Radius;
	public float TimeForExplosion = 0.1f;
	float timeElapsed = 0;
	// Use this for initialization
	void Start ()
	{
		blastwaveSprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeElapsed += Time.deltaTime;
		Vector3 tempScale = transform.parent.gameObject.transform.localScale;
		tempScale = Vector3.Lerp (Vector3.zero,Vector3.one*Radius, timeElapsed / TimeForExplosion);
		transform.parent.gameObject.transform.localScale = tempScale; 

		Color tempColor = gameObject.GetComponent<SpriteRenderer> ().color;
		tempColor.a = Mathf.Lerp(0.5f,0.0f,timeElapsed / TimeForExplosion);
		gameObject.GetComponent<SpriteRenderer> ().material.color = tempColor;

		if (timeElapsed / TimeForExplosion >= 1) {
			Destroy (transform.parent.gameObject);
		}
	}
}

