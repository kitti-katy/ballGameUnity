using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOptions : MonoBehaviour
{
[SerializeField]
	public Camera mainCam;
	[SerializeField]
	public Camera upperCam;
	public GameObject shot;
	public GameObject shotSpawn;
	private CameraController cc;
	public Rigidbody rb;
	TimerScript timer;
	bool changingGravityUp;
	bool changingGravityForward;

	public Vector3 sky;
	public Vector3 tempSky;

	private Vector3 vectorGoUp;
	private Vector3 vectorGoForward;
	private PlayerControl playerControl1;
	public int health;
	public bool currentPlayer;


	float timeCounterToTransform;
	// Use this for initialization
	void Start ()
	{
		mainCam = Camera.main;
		timer = FindObjectOfType<TimerScript> ();

		rb = GetComponent<Rigidbody> ();
		// GetComponent<ConstantForce> ().force = new Vector3 (0, -9, 0);
		cc = FindObjectOfType<CameraController> ();

		timeCounterToTransform = 0.0f;
		sky = GetComponent<ConstantForce> ().force * (-1.0f);
		playerControl1 = FindObjectOfType<PlayerControl> ();
		Debug.Log (playerControl1);
			
		cc.setSky (sky);
		health = 100;
		tempSky = new Vector3 (0, 5, -4);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftControl)) {

			timer.goTimer = false;
			GameObject newShot = Instantiate (shot, shotSpawn.transform.position, shotSpawn.transform.rotation) as GameObject;
			playerControl1.enabled = false;
			mainCam.GetComponent<PlayerControl> ().enabled = false;
			cc.changeDirection (sky, sky * 2);
			cc.setLookAt (newShot.transform);
		

		}
		if (Input.GetKey (KeyCode.RightControl)) {


			cc.changeDirection (sky, new Vector3(0,0,0));



		}
	

		if (changingGravityUp && timeCounterToTransform < 0.25f) {
			timeCounterToTransform += Time.deltaTime;
			rb.transform.Translate (vectorGoUp * Time.deltaTime*2.5f, Space.World);

			if (timeCounterToTransform >= 0.25f) {
				timeCounterToTransform = 0;
				changingGravityUp = false;
				if (currentPlayer)
					cc.changeDirection (sky, tempSky);
				changingGravityForward = true;
			}

		} else if (changingGravityForward && timeCounterToTransform < 0.5f) {
			
				
			timeCounterToTransform += Time.deltaTime;
			rb.transform.Translate (vectorGoForward * Time.deltaTime*2.5f, Space.World);
			if (timeCounterToTransform >= 0.5f) {
				GetComponent<ConstantForce> ().force = sky * (-1.0f);
			
				changingGravityForward = false;
				timeCounterToTransform = 0;
				playerControl1.frozenControl = false;

				cc.timer = 4;
		
			
			}
			
		}



	}

	void OnTriggerEnter (Collider collectableObject)
	{
		if (collectableObject.gameObject.CompareTag ("pickUp")) {
			collectableObject.gameObject.SetActive (false);
			Vector3 scaleDecseaser = rb.transform.localScale;
			scaleDecseaser.x += 0.1f;
			scaleDecseaser.y += 0.1f;
			scaleDecseaser.z += 0.1f;
			rb.transform.localScale = scaleDecseaser;
			changeHealth (20);

		}
		if (collectableObject.gameObject.CompareTag ("gravityChanger")) {
			cc.timer = 100;
			cc.gravityChange = true;

			playerControl1.frozenControl = true;

			DirectionValues dv = collectableObject.gameObject.GetComponent<DirectionValues> ();

			vectorGoUp = dv.getFirstVectorChange (rb.transform.localScale.x, sky);
			vectorGoForward = dv.getSecondVectorChange (rb.transform.localScale.x, sky);
		

			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		
		
		
			GetComponent<ConstantForce> ().force = new Vector3 (0, 0, 0);


			sky = dv.getNewSky (sky);
		
			tempSky = dv.getCameraDir (sky);
			changingGravityUp = true;
	
		}
		if (collectableObject.gameObject.CompareTag ("fireParticleShot")) {
			
			Debug.Log ("Shot found");
			collectableObject.gameObject.SetActive (false);
			Vector3 scaleDecseaser = rb.transform.localScale;
			scaleDecseaser.x -= 0.1f;
			scaleDecseaser.y -= 0.1f;
			scaleDecseaser.z -= 0.1f;
			rb.transform.localScale = scaleDecseaser;
			changeHealth (-20);

		}
	}

	public Vector3 getSky ()
	{
		return sky;
	}

	public void changeHealth (int n)
	{
		health += n;
		if (health <= 0)
			gameObject.SetActive (false);
	}
}
