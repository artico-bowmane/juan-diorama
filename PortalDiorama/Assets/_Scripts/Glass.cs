using UnityEngine;

public class GlassShatterController : MonoBehaviour
{
    public float impactForce = 10f;

    public float effectRadius = 3f;

    public float falloffExponent = 2f;

    private Rigidbody[] fragments;

    void Start()
    {
        fragments = GetComponentsInChildren<Rigidbody>();

        foreach (var rb in fragments)
        {
            rb.isKinematic = true;
        }
    }

    public void Shatter(Vector3 collisionPoint)
    {
        foreach (var rb in fragments)
        {
            float distance = Vector3.Distance(collisionPoint, rb.worldCenterOfMass);
            if (distance <= effectRadius)
            {
                rb.isKinematic = false;

                float falloff = Mathf.Pow(1f - (distance / effectRadius), falloffExponent);
                Vector3 direction = (rb.worldCenterOfMass - collisionPoint).normalized;

                rb.AddForce(direction * impactForce * falloff, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Shatter(hitPoint);
            Debug.Log("BREAK");
        }
    }
}