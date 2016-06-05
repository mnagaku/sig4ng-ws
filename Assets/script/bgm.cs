using UnityEngine;
using System.Collections;

public class bgm : MonoBehaviour {

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Resources.Load("335571__magntron__gamemusic") as AudioClip;
		audioSource.loop = true;
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
