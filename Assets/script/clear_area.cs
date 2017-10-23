using UnityEngine;
using System.Collections;

public class clear_area : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("clear");
	}
}
