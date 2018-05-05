using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    public float xVelocity = 10;
    public float xMaxVelocity = 100;
    public float jumpVelocity = 300;

    private float xInput;
    private Rigidbody2D rb;
    private NetworkIdentity networkIdentity;
    private List<Transform> grounds = new List<Transform>();

    public bool IsGrounded
    {
        get
        {
            grounds.RemoveAll(x => x.gameObject == null);
            return grounds.Count != 0;
        }
    }

	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
        networkIdentity = GetComponent<NetworkIdentity>();
	}

	private void Update()
	{
        if (!networkIdentity.isLocalPlayer)
            return;
        
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            Jump();
            
	}

	private void FixedUpdate()
	{
        if (!networkIdentity.isLocalPlayer)
            return;

        if (IsGrounded)
        {
            rb.AddForce(Vector3.right * xInput * xVelocity * Time.fixedDeltaTime);
            var xVel = Mathf.Clamp(rb.velocity.x, -xMaxVelocity, xMaxVelocity);
            rb.velocity = new Vector2(xVel, rb.velocity.y);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("Ground"))
            grounds.Add(collision.transform);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("Ground"))
            grounds.Remove(collision.transform);
	}

	private void Jump()
    {
        rb.AddForce(Vector2.up * jumpVelocity);
    }

}
