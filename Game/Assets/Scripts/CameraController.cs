using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
	
	public Transform lookAt;
	public Transform camTransform;

	public float timer;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float currentZ = 0.0f;
	public PlayerControl playerControl;
	public bool gravityChange;

	private Vector3 sky;
	public int camNumber;
	private Text healthIndicator;

	private bool checker;
	public bool stopCameraGravityChange;

	float timeCountWhenGravityChanges;
	private BallOptions ballOptions;

	public Vector3 dir;
	public Vector3 oldDir;
	void Start () {
		lookAt = GameObject.FindGameObjectsWithTag ("player")[camNumber].GetComponent<Transform>();
		healthIndicator = GameObject.FindGameObjectWithTag("HealthIndicator").GetComponent<Text>();
		camTransform = transform;
	
		camNumber = 0;
		dir = new Vector3 (0, 5, -4);
		playerControl = FindObjectOfType<PlayerControl>();
		ballOptions = GameObject.FindGameObjectsWithTag ("player")[camNumber].GetComponent<BallOptions>();
		ballOptions.currentPlayer = true;
		timeCountWhenGravityChanges = 0.0f;
		sky = new Vector3 (0.0f,9.0f,0.0f);
		sendSetPressDown ();
	
	}
	private void Update(){

		lookAt = GameObject.FindGameObjectsWithTag ("player")[camNumber].GetComponent<Transform>();
		sendHealthIndicator ();
		setNewLeftRightRotationControl ();


	}


	public void changePlayer(int n){
		ballOptions.currentPlayer = false;
		camNumber = n;
		playerControl =GameObject.FindGameObjectsWithTag ("player")[camNumber].GetComponent<PlayerControl>();
		ballOptions = GameObject.FindGameObjectsWithTag ("player")[camNumber].GetComponent<BallOptions>();
		sendHealthIndicator ();
		ballOptions.currentPlayer = true;
		changeDirection (ballOptions.sky, ballOptions.tempSky);
	}
	public void sendHealthIndicator(){
		
		healthIndicator.text = ballOptions.health.ToString ();}


	void LateUpdate () {
		if (gravityChange) {
			Quaternion rotation1 = Quaternion.Euler (currentY, currentX, currentZ);
			camTransform.position=	Vector3.Slerp (camTransform.position, lookAt.position + rotation1 * dir, Time.deltaTime*10f);
			camTransform.LookAt (lookAt.position, sky);
			timer -= Time.deltaTime;
			if (timer < 0.0f)
				gravityChange = false;

		} else
		{		Quaternion rotation = Quaternion.Euler (currentY, currentX, currentZ);
			camTransform.position = lookAt.position + rotation * dir;

			camTransform.LookAt (lookAt.position, sky);
	}



	}
	public void changeDirection(Vector3 newSky, Vector3 newDirection){

		gravityChange = true;
		oldDir = dir;
		sky = newSky;

		setNewLeftRightRotationControl ();
		Debug.Log (newDirection);
		dir = newDirection;
		sendSetPressDown ();
	}


	public void setSky(Vector3 newSky){
		sky = newSky;
		sendSetPressDown ();
	}


	public void sendSetPressDown(){
		Debug.Log (sky);
		playerControl = FindObjectOfType<PlayerControl>();
		if (sky.x != 0.0f)
			playerControl.setPressDown (new Vector3(-1.0f,1.0f,1.0f));
		else if (sky.y != 0.0f)
			playerControl.setPressDown (new Vector3(1.0f,-1.0f,1.0f));
		else
			playerControl.setPressDown (new Vector3(1.0f,1.0f,-1.0f));	
	}

	private void setNewLeftRightRotationControl (){
		if (sky.x > 0) {
			currentY += Input.GetAxis ("Horizontal");
			currentX = 0.0f;
			currentZ = 0.0f;
		} else if (sky.x < 0) {
			currentY -= Input.GetAxis ("Horizontal");
			currentX = 0.0f;
			currentZ = 0.0f;
		} else if (sky.z > 0) {

			currentZ += Input.GetAxis ("Horizontal");
			currentY = 0.0f;
			currentX = 0.0f;
		} else if (sky.z < 0) {
			currentZ -= Input.GetAxis ("Horizontal");
			currentY = 0.0f;
			currentX = 0.0f;
		} else if (sky.y > 0) {
			currentX += Input.GetAxis ("Horizontal");
			currentY = 0.0f;
			currentZ = 0.0f;
		} else {
			currentX -= Input.GetAxis ("Horizontal");
			currentY = 0.0f;
			currentZ = 0.0f;}
	
	}


	public void setLookAt(Transform newLookAt){
		lookAt = newLookAt;
	}
}
