using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sound : MonoBehaviour {

	public AudioClip ac;

	private bool sflag = true;
	private float x, z;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		if(!ac) {
			ac = Resources.Load("bomb") as AudioClip;
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
	}
}
