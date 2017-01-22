using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{
	public string m_InputName;
	public float m_Speed;
	public Color m_Color;

	public GameObject blastWave;

	Vector3 startPos;
	// Use this for initialization
	void Start ()
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.Translate (Input.GetAxis (m_InputName + "Horizontal") * m_Speed * Time.deltaTime, Input.GetAxis (m_InputName + "Vertical") * m_Speed * Time.deltaTime, 0);
		if(Input.GetButtonDown(m_InputName + "A"))
		{
			Vector3 posOffset = new Vector3 (transform.position.x, 0, transform.position.z);
			CauseExplosionAtPoint (posOffset, 5.0f, 500.0f);
		}

		var pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);


	}

	public void CauseExplosionAtPoint(Vector3 point, float explosionRadius, float power)
	{
		var planetLayer = 1 << LayerMask.NameToLayer("Planets");
		Collider[] colliders = Physics.OverlapSphere(point, explosionRadius, planetLayer);
		foreach (Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				Debug.Log("Explosion Expected " +rb.gameObject.name);
			rb.AddExplosionForce(power, point, explosionRadius,0.0F);
		}
		point.y = 10;
		GameObject boom = (GameObject)Instantiate (blastWave, point, Quaternion.identity);
		boom.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().color = m_Color;
	}

	public void ResetPosition()
	{
		transform.position = startPos;
	}
}

