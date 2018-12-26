using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class WSPanel : EditorWindow
{
    private const int baseLayer = 8; // base

    private int selected = 10; // 「全ステージ」を初期選択

    public int getSelected() {
        return selected;
    }

    void OnGUI ()
    {
        EditorGUI.BeginChangeCheck ();
        EditorGUILayout.LabelField ("ステージ選択");
        selected = GUILayout.SelectionGrid (selected,
            new string[]{ "ステージ1", "ステージ2", "ステージ3", "ステージ4", "ステージ5",
            "ステージ6", "ステージ7", "ステージ8", "ステージ9", "ステージ10", "全ステージ"}, 1);
        if (EditorGUI.EndChangeCheck ()) {
            Clear();
            if(selected >= 10)
            {
                Debug.Log("edit 全ステージ");
                return;
            }
            Debug.Log("edit stage : " + (selected + 1));
            DisableBase();
            EnableStageOnly(selected + 1);
        }
    }
//----------
    private static readonly string[] scenefiles = {
        "ステージ1",
        "ステージ2",
        "ステージ3",
        "ステージ4",
        "ステージ5",
        "ステージ6",
        "ステージ7",
        "ステージ8",
        "ステージ9",
        "ステージ10",
        "共通"};
//----------
    private static void Clear()
    {
        foreach(var scenefile in scenefiles)
        {
            var scene = SceneManager.GetSceneByName(scenefile);
            foreach(var child in scene.GetRootGameObjects())
            {
                Editable(child);
            }
        }
    }

    private static void Editable(GameObject obj)
    {
        obj.hideFlags = HideFlags.None;
        foreach (Transform child in obj.transform)
        {
            Editable(child.gameObject);
        }
    }
//----------
    private static void DisableBase()
    {
        foreach(var scenefile in scenefiles)
        {
            var scene = SceneManager.GetSceneByName(scenefile);
            foreach(var child in scene.GetRootGameObjects())
            {
                DisableBase(child);
            }
        }
    }

    private static void DisableBase(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            DisableBase(child.gameObject);
        }

        if (obj.layer == baseLayer)
        {
            obj.hideFlags |= HideFlags.NotEditable;
            obj.hideFlags |= HideFlags.HideInHierarchy;
            obj.hideFlags |= HideFlags.HideInInspector;
        }
    }
//----------
    private static void EnableStageOnly(int stage)
    {
        foreach(var scenefile in scenefiles)
        {
            if(scenefile == scenefiles[stage - 1])
            {
                continue;
            }
            var scene = SceneManager.GetSceneByName(scenefile);
            foreach(var child in scene.GetRootGameObjects())
            {
                DisableStage(child);
            }
        }
    }

    private static void DisableStage(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            DisableStage(child.gameObject);
        }

        obj.hideFlags |= HideFlags.NotEditable;
        obj.hideFlags |= HideFlags.HideInHierarchy;
        obj.hideFlags |= HideFlags.HideInInspector;
    }
}
