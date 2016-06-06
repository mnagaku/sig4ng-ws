using UnityEngine;
using System.Collections;

public class gate : MonoBehaviour {

	private Camera camera = Camera.main;
	private bool mflag = false;
	private bool fflag = true;
	private float targetX, targetY, targetZ;

	// Use this for initialization
	void Start () {
		targetX = (float)(this.transform.position.x - 3.5);
		targetY = (float)(this.transform.position.x + 6);
		targetZ = (float)(this.transform.position.x - 13);
	}
	
	// Update is called once per frame
	void Update () {
		if(mflag) {
			Vector3 newPosition = camera.transform.position;
	        newPosition.x = targetX;
	        newPosition.y = targetY;
	        newPosition.z = targetZ;
	        camera.transform.position = newPosition;
			mflag = false;
		}
	}

	void OnTriggerEnter(Collision other) {
		if(fflag) {
			mflag = true;
			fflag = false;
		}
	}
}
