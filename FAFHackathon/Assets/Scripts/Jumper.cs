using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Transform directionPoint;
    public float jumpVelocity;
    public float normalMass = 10;
    public float angularDrag = 100;
    public float massVelGradient = 1.8f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        GetComponent<Animator>().SetTrigger("Activate");
        var rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null)
            return;
        var direction = (directionPoint.position - collision.transform.position).normalized;
        //rb.AddTorque(Random.Range(-angularDrag, angularDrag), ForceMode2D.Impulse);

        rb.velocity = Vector3.zero;
        rb.velocity = direction * jumpVelocity * Mathf.Pow(rb.mass, massVelGradient) / normalMass;
	}
}
