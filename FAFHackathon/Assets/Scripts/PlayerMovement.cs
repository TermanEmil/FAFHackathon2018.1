using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float xVelocity = 10;
    public float flyingVelocity = 10;
    public float xMaxVelocity = 100;
    public float jumpVelocity = 300;
    public float torqueUpVel = 10;

    private Vector2 _velocity;

    private float xInput;
    private Rigidbody2D rb;
    private NetworkIdentity networkIdentity;
    public List<Transform> grounds = new List<Transform>();

    public bool IsGrounded
    {
        get
        {
            grounds.RemoveAll(x => x == null || x.gameObject == null);
            return grounds.Count != 0;
        }
    }

	private void Start()
	{
        if (!isLocalPlayer)
            this.enabled = false;
        
        rb = GetComponent<Rigidbody2D>();
        networkIdentity = GetComponent<NetworkIdentity>();
	}

	private void Update()
	{
        if (networkIdentity != null && !networkIdentity.isLocalPlayer)
            return;

        xInput = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(xInput, 0))
        {
            _velocity = Vector2.right * xVelocity;
            //rb.AddTorque(-xInput * xVelocity * Time.deltaTime, ForceMode2D.Force);

            //if (!IsGrounded)
            //{
            //    _velocity = Vector3.right * xVelocity;
            //}
            //else
                //rb.AddForce(Vector3.up * torqueUpVel * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            Jump();
            
	}

	private void FixedUpdate()
	{
        if (!Mathf.Approximately(xInput, 0))
        {
            var vel = new Vector2(xInput * xVelocity, rb.velocity.y);
            rb.velocity = vel;
        }
        else
        {
            
        }
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!isLocalPlayer)
            return;
        
        if (collision.gameObject.CompareTag("Ground"))
            grounds.Add(collision.transform);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (!isLocalPlayer)
            return;
        
        if (collision.gameObject.CompareTag("Ground"))
            grounds.Remove(collision.transform);
	}

	private void Jump()
    {
        if (!isLocalPlayer)
            return;
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

}
