using UnityEngine;
using System.Collections;

public class bgm : MonoBehaviour {

	public AudioClip ac;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = ac;
		audioSource.loop = true;
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
