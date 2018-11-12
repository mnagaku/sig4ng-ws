using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

[InitializeOnLoad]
public static class BallPos
{
    static BallPos()
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

            Scene scene = SceneManager.GetActiveScene();

            // Try to find "firstBall" from root game objects.
            // Move the first ball to the initiali position.
            foreach (var child in scene.GetRootGameObjects())
            {
                //Debug.Log(child.name);
                if (child.name == "firstBall")
                {
                    child.gameObject.transform.position
                        += new Vector3(-10f * startNo, -5f * startNo, -8.3f * startNo);
                    break;
                }
            }

            foreach(var child in scene.GetRootGameObjects())
            {
                if (child.name.StartsWith("stage") && !child.name.StartsWith("stageBase"))
                {
                    int stageNo = Convert.ToInt32(child.name.Substring(5));

                    if (startNo + 1 == stageNo)
                    {
                        ApplyCamera(child.gameObject);
                    }

                }
            }
        }
        else if(state == PlayModeStateChange.EnteredEditMode)
        {
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

    static private void ApplyCamera(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.name == "baseGate")
            {
                var gate = child.gameObject.GetComponent<gate>();
                if (gate)
                {
                    // Debug.Log("Apply camera of " + obj.gameObject.name + ".");
                    gate.ApplyCamera(true);
                }
            }
        }
    }
}
