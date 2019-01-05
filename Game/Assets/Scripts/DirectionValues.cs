using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionValues : MonoBehaviour {

	public Vector3 sky1;
	public Vector3 sky2;
	private Vector3 cameraDir1;
	private Vector3 cameraDir2;

	public float x1,y1,z1, x2,y2,z2, cx1,cy1, cz1, cx2, cy2, cz2;
	private Vector3 firstTransform;
	private Vector3 secondTransform;
	void Start(){
		 firstTransform = new Vector3(x1,y1,z1);
		secondTransform = new Vector3(x2,y2,z2);
		cameraDir1 = new Vector3 (cx1, cy1, cz1);

		cameraDir2 = new Vector3 (cx2, cy2, cz2);

	}
	public Vector3 getFirstVectorChange(float ballSize, Vector3 sky){
		if (sky == sky1)
			return firstTransform * ballSize;
		else
			return secondTransform * ballSize * (-1.0f);
	}

	public Vector3 getSecondVectorChange(float ballSize, Vector3 sky){
		if (sky == sky1)
			return secondTransform * ballSize*2;
		else
			return firstTransform * ballSize * (-1.0f)*2;
	}
	public Vector3 getNewSky(Vector3 sky){
		if (sky == sky1)
			return sky2;
		else
			return sky1;
	}

	public Vector3 getCameraDir(Vector3 sky){
		if (sky == sky1) {
			Debug.Log (cameraDir1);
			return cameraDir1;
		} else {
			Debug.Log (cameraDir2);
			return cameraDir2;
		}
	}

}
