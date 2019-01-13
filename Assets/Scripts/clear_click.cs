using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
		SceneManager.LoadScene("marge");
/*
		SceneManager.LoadScene("共通");
        for(int n = 1; n <= 10; n++) {
            Scene s = SceneManager.GetSceneByName("ステージ"+n);
            if(s == null || !s.IsValid()) {
        		SceneManager.LoadScene("ステージ"+n, LoadSceneMode.Additive);
            }
        }
*/
	}
}
