using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour {
	bool turnOnFPSCam;
	CameraController cc;
	// Use this for initialization
	void Start () {
		cc = FindObjectOfType<CameraController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (turnOnFPSCam) {
		
//			transform = cc.lookAt.transform;


		
		}
		
	}
}
