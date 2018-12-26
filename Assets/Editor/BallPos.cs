using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;


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
            var w = EditorWindow.GetWindow<WSPanel>(typeof(WSPanel));
            var startNo = w.getSelected();
            startNo = startNo >= 10 ? 0 : startNo;

            var scene = SceneManager.GetSceneByName("共通");

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
            var scene = SceneManager.GetSceneByName("共通");

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
