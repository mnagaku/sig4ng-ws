using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class StagePos
{
    static StagePos()
    {
        EditorApplication.playModeStateChanged += OnChangedPlayMode;
    }

    private static void OnChangedPlayMode(PlayModeStateChange state) 
    {
        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            int startNo = 0;
            if(Menu.GetChecked("ステージ選択/ステージ1"))
            {
                    startNo = 0;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ2"))
            {
                    startNo = 1;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ3"))
            {
                    startNo = 2;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ4"))
            {
                    startNo = 3;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ5"))
            {
                    startNo = 4;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ6"))
            {
                    startNo = 5;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ7"))
            {
                    startNo = 6;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ8"))
            {
                    startNo = 7;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ9"))
            {
                    startNo = 8;
            }
            else if(Menu.GetChecked("ステージ選択/ステージ10"))
            {
                    startNo = 9;
            }
            else
            {
                return;
            }

//        Scene scene = SceneManager.GetSceneByName("Main");
            Scene scene = SceneManager.GetActiveScene();
            foreach(var child in scene.GetRootGameObjects())
            {
                for(int i = 0; i < 10; i++)
                {
                    if(child.name == string.Format("stage{0}", i+1))
                    {
                        float shift = (10 + i - startNo) % 10;
                        child.transform.position = new Vector3(-10f * shift, -5f * shift, -8.3f * shift);

                        Debug.LogFormat("moving : stage{0}", i+1);
                    }
                }
            }
        }
        else if(state == PlayModeStateChange.EnteredEditMode)
        {
//        Scene scene = SceneManager.GetSceneByName("Main");
        Scene scene = SceneManager.GetActiveScene();
            foreach(var child in scene.GetRootGameObjects())
            {
                if(child.name == "clearArea")
                {
                    child.hideFlags |= HideFlags.NotEditable;
                    child.hideFlags |= HideFlags.HideInHierarchy;
                    child.hideFlags |= HideFlags.HideInInspector;
                }
            }
        }
    }
}
