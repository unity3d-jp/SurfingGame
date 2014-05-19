using UnityEngine;
using System.Collections;

public class BoardController_Ver4 : MonoBehaviour {
	// ここから
	public Vector3 moveSpeed;
	public bool isGround = false;

	// Use this for initialization
	void Start () {
	
	}
	
	private float xForce;
	private bool isJump;
	
	void Update () {
		xForce = Input.GetAxisRaw("Horizontal");
		
		isJump = false;
		if ( Input.GetButton("Jump") && isGround ) isJump = true;
	}
	
	void FixedUpdate()
	{
		Vector3 vel = this.rigidbody.velocity;
		vel.x = moveSpeed.x * xForce;
		vel.z = moveSpeed.z;
		
		if ( isJump ) {
			vel.y += moveSpeed.y;
			isGround = false;
			isJump = false;
		}
		
		this.rigidbody.velocity = vel;
	}

	void OnCollisionEnter( Collision col ) 
	{
		// check ground for a water effect
		if ( col.gameObject.CompareTag("Ground") ) isGround = true;
	}
	// ここまで
}
