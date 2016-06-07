using UnityEngine;
using System.Collections;

public class gate : MonoBehaviour {

	private bool mflag = false;
	private bool fflag = true;
	public float vx, vy, vz;
	public int count;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(mflag && count < 50) {
	    Camera.main.transform.position = new Vector3(
				Camera.main.transform.position.x + vx,
				Camera.main.transform.position.y + vy,
				Camera.main.transform.position.z + vz);
			count++;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(fflag) {
			mflag = true;
			fflag = false;
			count = 0;
			vx = (float)(transform.position.x - 3.5
				- Camera.main.transform.position.x) / 50;
			vy = (float)(this.transform.position.y + 6
				- Camera.main.transform.position.y) / 50;
			vz = (float)(this.transform.position.z - 11
				- Camera.main.transform.position.z) / 50;
		}
	}
}
