using UnityEngine;
using System.Collections;

public class bgm : MonoBehaviour {

	public string bgmFile = "oikakekko";

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Resources.Load(bgmFile) as AudioClip;
		audioSource.loop = true;
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
