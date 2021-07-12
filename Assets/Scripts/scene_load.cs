using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static void OnAfterSceneLoadRuntimeMethod()
	{
        Debug.Log("OnAfterSceneLoadRuntimeMethod()");
        for(int n = 1; n <= 13; n++) {
            Scene s = SceneManager.GetSceneByName("ステージ"+string.Format($"{n:d2}"));
            if(!s.IsValid()) {
              Debug.Log("ステージ"+string.Format($"{n:d2}")+" load.");
          		SceneManager.LoadScene("ステージ"+string.Format($"{n:d2}"), LoadSceneMode.Additive);
            }
        }
	}
}
