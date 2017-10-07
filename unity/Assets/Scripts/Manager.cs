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
	public float arm2Scalpel;
	// Use this for initialization
	void Start () {
		arm1Length = 1;
		arm2Length = 1;
		arm1Arm2Angle = -45;
		arm2Scalpel = 45;

		GameObject arm1 = GameObject.Find ("arm1");
		arm1.transform.localScale = new Vector3 (1, arm1Length, 1);

		arm1.transform.position = new Vector3 (0, arm1Length, 0);

		GameObject arm2 = GameObject.Find ("arm2");
		//arm2.transform.localScale = new Vector3 (1, arm2Length, 1);
		//arm2.transform.position = new Vector3 (arm1Length, arm1Length+arm2Length, 0);

		GameObject scalpel = GameObject.Find ("scalpel");
		//GameObject lena = GameObject.Find ("lena");
		//lena.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (1, 0, 0), 45);


		//rotations
		Vector3 art2RotationPoint = new Vector3 (arm1.transform.position.x, arm1.transform.position.y*arm1.transform.localScale.y, arm1.transform.position.z);
		arm2.transform.RotateAround (art2RotationPoint,new Vector3 (1, 0, 0), arm1Arm2Angle);

		Vector3 scalpelRotationPoint = new Vector3 (arm2.transform.position.x, arm2.transform.position.y*arm2.transform.localScale.y, arm2.transform.position.z);
		scalpel.transform.RotateAround (scalpelRotationPoint,new Vector3 (1, 0, 0), arm2Scalpel);

		Debug.Log (scalpel.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
		//GameObject arm1 = GameObject.Find ("arm1");
		//Debug.Log (arm1.transform.position);
	}
}
