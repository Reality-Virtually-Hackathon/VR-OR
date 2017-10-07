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
	// Use this for initialization
	void Start () {
		GameObject arm1 = GameObject.Find ("arm1");
		arm1.transform.localScale = new Vector3 (1, arm1Length, 1);

		arm1.transform.position = new Vector3 (0, arm1Length, 0);




	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
