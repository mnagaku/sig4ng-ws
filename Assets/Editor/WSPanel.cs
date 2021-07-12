using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EditorWindowParameter : ScriptableSingleton<EditorWindowParameter>
{
	public int selectedStageNo = 13;
	public bool projectWasLoaded = false;
}

public class WSPanel : EditorWindow
{
    private const int baseLayer = 8; // base

    void OnGUI ()
    {
        var god = EditorWindowParameter.instance;
        EditorGUI.BeginChangeCheck ();
        EditorGUILayout.LabelField ("ステージ選択");
        god.selectedStageNo = GUILayout.SelectionGrid (god.selectedStageNo,
            new string[]{ "ステージ01", "ステージ02", "ステージ03", "ステージ04", "ステージ05",
            "ステージ06", "ステージ07", "ステージ08", "ステージ09", "ステージ10", "ステージ11",
            "ステージ12", "ステージ13", "全ステージ"}, 1);
        if (EditorGUI.EndChangeCheck ()) {
            SetHierarchy();
            if(god.selectedStageNo >= 13)
            {
                Debug.Log("edit 全ステージ");
                return;
            }
            Debug.Log("edit stage : " + (god.selectedStageNo + 1));
        }
    }

    public static void SetHierarchy() {
        var god = EditorWindowParameter.instance;
        Clear();
        if(god.selectedStageNo >= 13) return;
        DisableBase();
        EnableStageOnly(god.selectedStageNo + 1);
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
