using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		GetComponent <Rigidbody>().AddForce( transform.forward*50);
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
