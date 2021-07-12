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

    private static float[,] firstBallPosTable = new float[13, 2] {
        {39.3f, 39.3f},
        {39.3f, 29.3f},
        {39.3f, 19.3f},
        {39.3f, 9.3f},
        {29.3f, 0.7f},
        {19.3f, 0.7f},
        {9.3f, 0.7f},
        {0.7f, 10.7f},
        {0.7f, 20.7f},
        {0.7f, 30.7f},
        {10.7f, 39.3f},
        {20.7f, 39.3f},
        {29.3f, 29.3f}
    };

    private static void OnChangedPlayMode(PlayModeStateChange state) 
    {
        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            var god = EditorWindowParameter.instance;
            var startNo = god.selectedStageNo;
            startNo = startNo >= 13 ? 0 : startNo;

            var scene = SceneManager.GetSceneByName("共通");

            // Try to find "firstBall" from root game objects.
            // Move the first ball to the initiali position.
            foreach (var child in scene.GetRootGameObjects())
            {
                //Debug.Log(child.name);
                if (child.name == "firstBall")
                {
                    child.gameObject.transform.position
                        = new Vector3(firstBallPosTable[startNo,0], 3.0f, firstBallPosTable[startNo,1]);
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
