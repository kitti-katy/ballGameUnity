using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
	public Rigidbody rb;
	public float movementSpeed;
	float rotationAngle;
	public Transform camereTransform;

	public bool moveup;
	public bool movedown;
	public bool moveleft;
	public bool moveright;
	public bool releaseup;
	public bool releasedown;
	public Vector3 pressDown;
	public bool frozenControl;

	public int currentPlayerNumber;
	public bool playerChanged;



	public Vector3 upVector;
	public Vector3 downVector;
	public Vector3 leftVector;
	public Vector3 rightVector;
	private Vector3 v3;

	Vector3 velocityForGravity;

	void Start ()
	{   playerChanged = true;
		defineVectorsForForces ();
		currentPlayerNumber = 0;
		camereTransform = Camera.main.transform;
	}


	public void RefreshBooleansForMoving ()
	{
		moveup = false;
		movedown = false;
		moveleft = false;
		moveright = false; 
		releaseup = false;
		releasedown = false;
	}



	void FixedUpdate ()
	{
		checkPlayerChange ();
		if(!frozenControl)
		MakeMovement ();
	}



	void checkPlayerChange(){
		if (playerChanged) {
			rb = GameObject.FindGameObjectsWithTag ("player") [currentPlayerNumber].GetComponent<Rigidbody> ();

			playerChanged = false;
		}
	}

	public void DisableVelocity ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero; 
	}

		
	//Defining moving directions when a button is pressed and moves the object
	private void MakeMovement ()
	{
		if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.DownArrow) || releaseup || releasedown) {
			DisableVelocity ();
		}

		if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.UpArrow)) {
			
			rb.transform.Rotate (Vector3.right * Time.deltaTime);
			return;
		}

		if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.UpArrow)) {
			
			rb.transform.Rotate (Vector3.left * Time.deltaTime);
			return;
		}

		if (Input.GetKey (KeyCode.UpArrow))
			moveup = true;
		if (Input.GetKey (KeyCode.DownArrow))
			movedown = true;
		if (Input.GetKey (KeyCode.LeftArrow))
			moveleft = true;
		if (Input.GetKey (KeyCode.RightArrow))
			moveright = true;


		if (moveup && (moveleft || moveright) || moveup) {
			if (moveup)
				v3 = upVector;
			else if (movedown)
				DisableVelocity();
			else if (moveup && moveright) {
				DisableVelocity ();
				v3 = rightVector;
			} else if (moveup && moveleft) {
				DisableVelocity ();
				v3 = leftVector;
			}



			if (v3.magnitude > 1)
				v3.Normalize ();

			Vector3 rotatedDir = camereTransform.TransformDirection (v3);
			rotatedDir = new Vector3 (rotatedDir.x, rotatedDir.y, rotatedDir.z);
			rotatedDir = rotatedDir.normalized * v3.magnitude;


			rb.velocity = rotatedDir * movementSpeed;
			RefreshBooleansForMoving ();
		} else if (movedown && (moveleft || moveright) || movedown) {
	
			if (movedown)
				v3 = downVector;
			else if (moveup)
				DisableVelocity();
			else if (movedown && moveright) {
				DisableVelocity ();
				v3 = rightVector;
			} else if (movedown && moveleft) {
				DisableVelocity ();
				v3 = leftVector;
			}
		

			if (v3.magnitude > 1)
				v3.Normalize ();

			Vector3 rotatedDir = camereTransform.TransformDirection (v3);
			rotatedDir = new Vector3 (rotatedDir.x, rotatedDir.y, rotatedDir.z);
			rotatedDir = rotatedDir.normalized * v3.magnitude;
			Debug.Log (pressDown);
			rotatedDir.x *= pressDown.x;
			rotatedDir.y *= pressDown.y;
			rotatedDir.z *= pressDown.z;
	
			
			rb.velocity = rotatedDir * movementSpeed;
			RefreshBooleansForMoving ();


		}
	}

	public void defineVectorsForForces ()
	{
		v3 = new Vector3 (0.0f, 0.0f, 0.0f);
		upVector = new Vector3 (0.0f, 0.0f, 1.0f);
		downVector = new Vector3 (0.0f, 0.0f, -1.0f);
		leftVector = new Vector3 (-1.0f, 0.0f, 1.0f);
		rightVector = new Vector3 (1.0f, 0.0f, 1.0f);

	}
	public void setPressDown(Vector3 pdv){
		pressDown = pdv;}
}


