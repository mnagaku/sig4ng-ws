﻿using UnityEngine;
using System.Collections;

public class clear_click : MonoBehaviour {

	float timer = 0.0f;
	int count = 0;

	UnityEngine.UI.Text uiText;

	// Use this for initialization
	void Start () {
		uiText = this.gameObject.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
//		if (timer >= 1.0f)
		{
			timer = 0.0f;

			var col = uiText.color;
			count++;
			col = ((count & 0x04) == 0) ? Color.white : Color.black;
			uiText.color = col;
		}
	}

	public void ClickNext(string next)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("共通");
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ2", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ3", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ4", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ5", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ6", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ7", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ8", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ9", UnityEngine.SceneManagement.LoadSceneMode.Additive);
		UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ10", UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}
}
