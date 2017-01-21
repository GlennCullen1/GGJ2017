using UnityEngine;
using System.Collections;

public class TestExplosionOnClick : MonoBehaviour {

 public float distance = 20f;


void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("BOOM");
            CauseExplosionAtPoint(CastRayToWorld(Input.mousePosition), 5.0f, 500.0f);
        }
    }


 Vector3 CastRayToWorld(Vector3 coords)
     {
        Ray ray = Camera.main.ScreenPointToRay(coords);
        Vector3 point = ray.origin + (ray.direction * distance);
        Debug.Log(point);
        return point;
     }

    void CauseExplosionAtPoint(Vector3 point, float explosionRadius, float power)
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
    }
}
