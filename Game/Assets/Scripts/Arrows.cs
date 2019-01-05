using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour {

	private PlayerControl player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerControl> ();
		Debug.Log ("Found");

	}


	public void UpArrow  () {

		player.movedown = false;
		player.moveup = true;
		player.releaseup = false;

	}


	public void DownArrow  () {

		player.movedown = true;
		player.moveup = false;
		Debug.Log ("downArrowpressed");
		player.releasedown = false;

	}

	public void ReleaseUpArrow(){

		player.moveup = false;
		player.releaseup = true;

	}

	public void ReleaseDownArrow(){

		player.movedown = false;
		player.releasedown = true;

	}

	/*public void StopButtonPressed(){
		player.stopGame = true;
	}*/



}
