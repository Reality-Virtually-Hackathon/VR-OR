using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	[Header("Robotarm")]
	public float arm1Length;
	public float arm2Length;
	public float scalpelLength;
	public float arm1Arm2Angle;
	public float baseArm1Angle;
	public float arm2ScalpelAngle;
	// Use this for initialization
	void Start () {
		arm1Length = 1;
		arm2Length = 1;
		baseArm1Angle = 0;
		arm1Arm2Angle = 0;
		arm2ScalpelAngle = 45;


		GameObject arm1 = GameObject.Find ("arm1");
		GameObject arm2 = GameObject.Find ("arm2");
		GameObject scalpel = GameObject.Find ("scalpel");
		GameObject Base = GameObject.Find ("base");

		arm1.transform.localScale = new Vector3 (1, arm1Length, 1);

		arm1.transform.position = new Vector3 (0, arm1Length, 0);




		//GameObject lena = GameObject.Find ("lena");
		//lena.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (1, 0, 0), 45);



		//rotations
		Vector3 BaseRotationPoint = new Vector3(Base.transform.position.x, Base.transform.position.y, Base.transform.position.z);
		arm1.transform.RotateAround (BaseRotationPoint,new Vector3 (1, 0, 0), baseArm1Angle);

		Vector3 arm2RotationPoint = new Vector3 (arm1.transform.position.x, arm1.transform.position.y+arm1.transform.localScale.y, arm1.transform.position.z);
		arm2.transform.RotateAround (arm2RotationPoint,new Vector3 (1, 0, 0), arm1Arm2Angle);

		Vector3 scalpelRotationPoint = new Vector3 (arm2.transform.position.x, arm2.transform.position.y+arm2.transform.localScale.y, arm2.transform.position.z);
		scalpel.transform.RotateAround (scalpelRotationPoint,new Vector3 (1, 0, 0), arm2ScalpelAngle);

		Debug.Log (scalpel.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
		//GameObject arm1 = GameObject.Find ("arm1");
		//Debug.Log (arm1.transform.position);
	}
}
