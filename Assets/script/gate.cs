using UnityEngine;
using System.Collections;

public class gate : MonoBehaviour {

	private bool mflag = false;
	private bool fflag = true;
	private float targetX, targetY, targetZ;

	// Use this for initialization
	void Start () {
		targetX = (float)(this.transform.position.x - 3.5);
		targetY = (float)(this.transform.position.y + 6);
		targetZ = (float)(this.transform.position.z - 11);
	}
	
	// Update is called once per frame
	void Update () {
		if(mflag) {
	        Camera.main.transform.position = new Vector3(targetX, targetY, targetZ);
			mflag = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(fflag) {
			mflag = true;
			fflag = false;
		}
	}
}
