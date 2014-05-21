using UnityEngine;
using System.Collections;

public class BoardController_Complete : MonoBehaviour {
	// ここから
	public Vector3 moveSpeed = new Vector3(4,4,8);
	public bool isGround = false;
	public Transform boardObject;
	public Vector3 targetAngle;
	public bool isOculus = true;
	private OculusJump jumpDetector;
	public float angleLimit = 45.0f;

	// Use this for initialization
	void Start () {
		if ( isOculus ) {
			jumpDetector = GetComponent<OculusJump>();
		}
	}
	
	private float xForce;
	private bool isJump;
	
	void Update () {
		if ( isOculus )  {
			Vector3 angles = jumpDetector.riftCam.transform.rotation.eulerAngles;
			float rad = Mathf.Clamp( Mathf.DeltaAngle( angles.z, 0.0f ), -angleLimit, angleLimit ) / angleLimit;
			xForce = rad;
		} else {	
			xForce = Input.GetAxisRaw("Horizontal");
		}

		Vector3 nowAngle = boardObject.localRotation.eulerAngles;
		nowAngle.y = Mathf.LerpAngle( nowAngle.y, targetAngle.y * xForce, 0.1f );
		nowAngle.z = Mathf.LerpAngle( nowAngle.z, targetAngle.z * xForce, 0.1f );
		boardObject.localRotation = Quaternion.Euler(nowAngle);

		isJump = false;
		if ( isOculus )  {
			if ( jumpDetector.isJump && isGround ) isJump = true;
		} else {	
			if ( Input.GetButton("Jump") && isGround ) isJump = true;
		}
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
