using UnityEngine;
using System.Collections;

public class WarpSpeed : MonoBehaviour {
	public float WarpDistortion;
	public float Speed;
	ParticleSystem particles;
	ParticleSystemRenderer rend;
	public bool isWarping;

	float topTime = 20.0f;
	float currentTimescale = 0;
	float timer = 0;
	void Awake()
	{
		particles = GetComponent<ParticleSystem>();
		rend = particles.GetComponent<ParticleSystemRenderer>();
		currentTimescale = Random.Range (topTime / 2, topTime);
	}

	void Update()
	{

		timer += Time.deltaTime;
		if (timer > currentTimescale) {
			if (isWarping) {
				Disengage ();
			} else {
				Engage ();
			}
			currentTimescale = Random.Range (topTime / 2, topTime);
			timer = 0;
		}

		if(isWarping && !atWarpSpeed())
		{
			rend.velocityScale += WarpDistortion * (Time.deltaTime * Speed);
		}

		if(!isWarping && !atNormalSpeed())
		{
			rend.velocityScale -= WarpDistortion * (Time.deltaTime * Speed);
		}
	}

	public void Engage()
	{
		isWarping = true;
	}

	public void Disengage()
	{
		isWarping = false;
	}

	bool atWarpSpeed()
	{
		return rend.velocityScale < WarpDistortion;
	}

	bool atNormalSpeed()
	{
		return rend.velocityScale > 0;
	}
}
