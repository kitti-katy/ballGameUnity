using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour {
	[SerializeField]
	Camera upperCamera;
	Transform lookAt;
	Vector3 sky;

	// Use this for initialization
	void Start () {
		upperCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		setLookAt (lookAt, sky);

	}

	public void setLookAt(Transform lookAt, Vector3 sky){ 
		this.lookAt = lookAt;
		this.sky = sky;
		upperCamera.transform.position = lookAt.position +  sky*1.2f;
		upperCamera.transform.LookAt (lookAt.position, sky);
	}
}
