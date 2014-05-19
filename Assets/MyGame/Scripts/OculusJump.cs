using UnityEngine;
using System.Collections;

public class OculusJump : MonoBehaviour {

	public GameObject riftCam;
	float x = 0 ;
	float y = 0;
	float z = 0;

	public float limitGravY = -17.0f;
	private float [] lastGravY;
	private int gravCnt = 0;

	void Start() {
		lastGravY = new float[5];
		for( int i=0 ; i<lastGravY.Length ; i++ ) {
			lastGravY[i] = 0.0f;
		}
		if ( riftCam == null ) {
			riftCam = GameObject.Find("CameraRight");
		}
	}

	Vector3 acc;
	Vector3 grav;
	float ave;

	public bool isJump = false;
	
	// Update is called once per frame
	void Update () {
		#if UNITY_STANDALONE || UNITY_EDITOR
//		if ( OVRDevice.IsHMDPresent() == false ) return;
//		OVRDevice.GetAcceleration(0, ref x, ref y, ref z);
		acc = new Vector3(-x, -y, z);
		#elif UNITY_IPHONE || UNITY_ANDROID
		acc = new Vector3( Input.acceleration.x * 9.81f, Input.acceleration.y * 9.81f, -Input.acceleration.z * 9.81f);
		#endif

		Matrix4x4 mtx = riftCam.transform.localToWorldMatrix;
		mtx.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
		grav = mtx.MultiplyPoint(acc);

		lastGravY[gravCnt] = grav.y;
		ave = 0.0f;
		for( int i=0 ; i<lastGravY.Length ; i++ ) {
			ave += lastGravY[i];
		}
		ave /= (lastGravY.Length * 1.0f);
		gravCnt++;
		if ( gravCnt >= lastGravY.Length ) gravCnt = 0;

		//print ( grav.y + "	" + ave );

		isJump = false;
		if ( ave < limitGravY ) {
			isJump = true;
		}
	}


}
