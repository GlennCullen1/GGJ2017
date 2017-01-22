using UnityEngine;
using System.Collections;

public class SpritePlayer : MonoBehaviour
{
	SpriteRenderer displaySprite; 
	public Sprite[] spriteArray;
	bool reverse = false;
	public float framesPerSecond = 6;
	int currentFrame = 0;
	public float timer = 0;
	// Use this for initialization
	void Start ()
	{
		currentFrame = Random.Range (0, spriteArray.Length);
		displaySprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= 1.0f / framesPerSecond) {
			timer = 0;

			if (currentFrame == spriteArray.Length - 1) {
				reverse = true;
			}

			if (currentFrame == 0) {
				reverse = false;
			}

			if (!reverse) {
				currentFrame++;
				displaySprite.sprite = spriteArray [currentFrame];
			} else {
				currentFrame--;
				displaySprite.sprite = spriteArray [currentFrame];
			}
		}

	}
}

