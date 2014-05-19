using UnityEngine;
using System.Collections;

public class BoardController_Ver1 : MonoBehaviour {
	// ここから
	public Vector3 moveSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xForce = 0.0f;
		xForce = Input.GetAxisRaw("Horizontal");
		
		// velocity setting
		Vector3 vel = this.rigidbody.velocity;
		vel.x = moveSpeed.x * xForce;
		vel.z = moveSpeed.z;
		
		this.rigidbody.velocity = vel;
	}
	// ここまで
}
