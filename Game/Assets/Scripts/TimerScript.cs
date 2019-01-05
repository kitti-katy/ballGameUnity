using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TimerScript : MonoBehaviour {
	public float timer;
	private Text t;
	private PlayerControl player;
	private CameraController camControl;
	private int currentPlayerNumber;
	public bool goTimer;
	public int time;

	// Use this for initialization
	void Start ()
	{goTimer = true;
		currentPlayerNumber = 0;
		player = FindObjectOfType<PlayerControl> ();

		timer = time;
		t = GetComponent<Text> ();
		camControl = FindObjectOfType<CameraController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{if (goTimer) {
			timer -= Time.deltaTime; 
			t.text = Math.Floor (timer).ToString ();

			if (timer < 0.0f) {
				player.DisableVelocity ();
				if (player.currentPlayerNumber != 3) {
					currentPlayerNumber++;
				} else {
					currentPlayerNumber = 0;			
				}

				player.currentPlayerNumber = currentPlayerNumber;
				camControl.changePlayer (currentPlayerNumber);
				player.playerChanged = true;

				timer = time;
			}
		}
	
	}
}
