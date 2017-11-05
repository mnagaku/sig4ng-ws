using UnityEngine;
using System.Collections;

public class gate : MonoBehaviour {

	[Range(-10.0f, 10.0f)]
	public float targetX = 0.5f;

	[Range(0.0f, 15.0f)]
	public float targetY = 10.0f;

	[Range(-10.0f, 10.0f)]
	public float targetZ = -15.0f;

	private bool fflag = true;
	private int count = 0, step = 50;
	private Vector3 fromPosition, toPosition;
	private Quaternion fromRotation, toRotation;

	// Use this for initialization
	void Start () {
		toPosition = new Vector3(
			transform.root.position.x + targetX,
			transform.root.position.y + targetY,
			transform.root.position.z + targetZ);
		toRotation =  Quaternion.LookRotation(
			new Vector3(-targetX, -targetY, -targetZ));
	}

	// Update is called once per frame
	void Update () {
		if(!fflag && count < step) {
			Camera.main.transform.position =
				Vector3.Lerp(fromPosition, toPosition,
				(float)count / step);
			Camera.main.transform.rotation =
				Quaternion.Lerp(fromRotation, toRotation,
				(float)count / step);
			count++;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(fflag) {
			fflag = false;
			fromPosition = new Vector3(
				Camera.main.transform.position.x,
				Camera.main.transform.position.y,
				Camera.main.transform.position.z);
			fromRotation = new Quaternion(
				Camera.main.transform.rotation.x,
				Camera.main.transform.rotation.y,
				Camera.main.transform.rotation.z,
				Camera.main.transform.rotation.w);
		}
	}
}
