using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sound : MonoBehaviour {

	public AudioClip ac;
	private float timeElapsed = 0.0f;

	private bool sflag = true;
	private float x, z;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody>().Sleep();

		if(!ac) {
			ac = Resources.Load("SE/bomb") as AudioClip;
		}
		x = transform.localPosition.x;
		z = transform.localPosition.z;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = ac;
		audioSource.time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(sflag && 
			(System.Math.Abs(transform.localPosition.x - x) > 0.1 ||
			System.Math.Abs(transform.localPosition.z - z) > 0.1)) {
			audioSource.Play();
			sflag = false;
		}
		if(!sflag)
		{
			timeElapsed += Time.deltaTime;
			if(timeElapsed > 5000.0f)
			{
				gameObject.GetComponent<Rigidbody>().Sleep();
			}
		}
	}
}
