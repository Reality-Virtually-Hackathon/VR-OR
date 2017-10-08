using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

	[Header("Robotarm")]
	public float arm1Length;
	//public float arm2Length;
	public float scalpelLength;
	public float arm2Angle;
	public float arm1Angle;
	public float scalpelAngle;


	// Use this for initialization
	void Start () {
		GameObject arm1 = GameObject.Find ("arm1");
		GameObject arm2 = GameObject.Find ("arm2");
		GameObject scalpel = GameObject.Find ("scalpel");
		GameObject Base = GameObject.Find ("base");
		GameObject ghost = GameObject.Find ("arm1_ghost");



		Debug.Log ("THIS line is running");


		//Rotations
		//Vector3 BaseRotationPoint = new Vector3(Base.transform.position.x, Base.transform.position.y, Base.transform.position.z);
		//arm1.transform.RotateAround (BaseRotationPoint,new Vector3 (1, 0, 0), baseArm1Angle);


		//Vector3 arm2RotationPoint = new Vector3 (arm1.transform.position.x, arm1.transform.position.y+arm1.transform.localScale.y, arm1.transform.position.z);
		//arm2.transform.RotateAround (arm2RotationPoint,new Vector3 (1, 0, 0), arm1Arm2Angle);

		//Vector3 scalpelRotationPoint = new Vector3 (arm2.transform.position.x, arm2.transform.position.y+arm2.transform.localScale.y, arm2.transform.position.z);
		//scalpel.transform.RotateAround (scalpelRotationPoint,new Vector3 (1, 0, 0), arm2ScalpelAngle);

		Debug.Log (scalpel.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject arm1 = GameObject.Find ("arm1");
		GameObject arm1R = GameObject.Find ("arm1_ghost");
		GameObject arm2 = GameObject.Find ("arm2");
		GameObject arm2R = GameObject.Find ("arm2_rotation");
		GameObject scalpel = GameObject.Find ("scalpel");
		GameObject scalpelR = GameObject.Find ("scalpel_rotation");
		GameObject Base = GameObject.Find ("base");



		//Allow for the change of the arm length
		arm1.transform.localRotation = Quaternion.Euler (0,arm1Angle, 0);
		arm1.transform.position = new Vector3 (0, arm1Length, 0);

		//Allow for changing the angle in manager mode
		scalpelR.transform.localRotation = Quaternion.Euler (scalpelAngle, 0,0);
		arm1R.transform.localRotation = Quaternion.Euler (0,arm1Angle, 0);
		arm2R.transform.localRotation = Quaternion.Euler (0,arm2Angle, 0);

		//Restore all to original sizes
		//Debug.Log(scalpelR.transform.localScale);
		//arm2.transform.localScale = new Vector3 (0.1f, arm2Length, 0.1f);
		//arm2.transform.position = new Vector3 (0, arm2Length, 0);
		//Vector3 BaseRotationPoint = new Vector3(RotationPointArm1.transform.position.x, RotationPointArm1.transform.position.y, RotationPointArm1.transform.position.z);

		//GameObject arm1 = GameObject.Find ("arm1");

		//arm1.transform.localRotation = (baseArm1Angle,0,0);
//		GameObject arm1 = GameObject.Find ("arm1");
//		GameObject arm2 = GameObject.Find ("arm2");
//		GameObject scalpel = GameObject.Find ("scalpel");
	
//
//		//Rotations
//		
//		Vector3 arm2RotationPoint = new Vector3 (arm1.transform.position.x, arm1.transform.position.y+arm1.transform.localScale.y, arm1.transform.position.z);
//		Vector3 scalpelRotationPoint = new Vector3 (arm2.transform.position.x, arm2.transform.position.y+arm2.transform.localScale.y, arm2.transform.position.z);
//
//		arm1.transform.RotateAround (BaseRotationPoint,new Vector3 (1, 0, 0), baseArm1Angle);
//		arm2.transform.RotateAround (arm2RotationPoint,new Vector3 (1, 0, 0), arm1Arm2Angle);
//		scalpel.transform.RotateAround (scalpelRotationPoint,new Vector3 (1, 0, 0), arm2ScalpelAngle);
//
		//

	}



}
