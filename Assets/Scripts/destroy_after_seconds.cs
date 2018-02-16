using UnityEngine;
using System.Collections;

public class destroy_after_seconds : MonoBehaviour {

    public int sec = 3;

	// Use this for initialization
	IEnumerator Start()
	{
		yield return new WaitForSeconds(sec);
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
