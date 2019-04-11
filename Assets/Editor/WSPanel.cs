using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class WSPanel : EditorWindow
{
    private const int baseLayer = 8; // base

    private int selected = 13; // 「全ステージ」を初期選択

    public int getSelected() {
        return selected;
    }

    void OnGUI ()
    {
        EditorGUI.BeginChangeCheck ();
        EditorGUILayout.LabelField ("ステージ選択");
        selected = GUILayout.SelectionGrid (selected,
            new string[]{ "ステージ01", "ステージ02", "ステージ03", "ステージ04", "ステージ05",
            "ステージ06", "ステージ07", "ステージ08", "ステージ09", "ステージ10", "ステージ11",
            "ステージ12", "ステージ13", "全ステージ"}, 1);
        if (EditorGUI.EndChangeCheck ()) {
            Clear();
            if(selected >= 13)
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
        "ステージ01",
        "ステージ02",
        "ステージ03",
        "ステージ04",
        "ステージ05",
        "ステージ06",
        "ステージ07",
        "ステージ08",
        "ステージ09",
        "ステージ10",
        "ステージ11",
        "ステージ12",
        "ステージ13",
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
