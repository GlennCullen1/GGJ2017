using UnityEngine;
using System.Collections;

public class TestExplosionOnClick : MonoBehaviour {

public float distance = 20f;
	public float radius = 5.0f;
	public float power = 500.0f;

public GameObject blastWave;

void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("BOOM");
			CauseExplosionAtPoint(CastRayToWorld(Input.mousePosition),radius,power);
        }
    }


 Vector3 CastRayToWorld(Vector3 coords)
     {
        Ray ray = Camera.main.ScreenPointToRay(coords);
        Vector3 point = ray.origin + (ray.direction * distance);
        Debug.Log(point);
        return point;
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
		Instantiate (blastWave, point, Quaternion.identity);
    }
}
