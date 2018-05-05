using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xVelocity = 10;
    public float jumpVelocity = 300;

    private float xInput;
    private Rigidbody2D rb;

	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
        xInput = Input.GetAxis("Horizontal");
	}

	private void FixedUpdate()
	{
        rb.AddForce(Vector3.right * xInput * xVelocity * Time.fixedDeltaTime);
	}
}
