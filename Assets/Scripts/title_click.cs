using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title_click : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ボタンをクリックした時の処理
    public void OnClick() {
        Debug.Log("click");
        SceneManager.LoadScene("共通");
   		SceneManager.LoadScene("ステージ1", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ2", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ3", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ4", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ5", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ6", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ7", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ8", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ9", LoadSceneMode.Additive);
   		SceneManager.LoadScene("ステージ10", LoadSceneMode.Additive);
/*
        for(int n = 1; n <= 10; n++) {
            Scene s = SceneManager.GetSceneByName("ステージ"+n);
            if(s == null || !s.IsValid()) {
        		SceneManager.LoadScene("ステージ"+n, LoadSceneMode.Additive);
            }
        }
*/
    }
}
