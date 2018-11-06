using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class StageSelect
{
    private const int baseLayer = 8; // base

    private static void Editable(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            Editable(child.gameObject);
        }
        obj.hideFlags = HideFlags.None;
    }

    private static void Clear()
    {
        Menu.SetChecked("ステージ選択/ステージ1", false);
        Menu.SetChecked("ステージ選択/ステージ2", false);
        Menu.SetChecked("ステージ選択/ステージ3", false);
        Menu.SetChecked("ステージ選択/ステージ4", false);
        Menu.SetChecked("ステージ選択/ステージ5", false);
        Menu.SetChecked("ステージ選択/ステージ6", false);
        Menu.SetChecked("ステージ選択/ステージ7", false);
        Menu.SetChecked("ステージ選択/ステージ8", false);
        Menu.SetChecked("ステージ選択/ステージ9", false);
        Menu.SetChecked("ステージ選択/ステージ10", false);
        Menu.SetChecked("ステージ選択/全ステージ", false);

//        Scene scene = SceneManager.GetSceneByName("Main");
        Scene scene = SceneManager.GetActiveScene();

        foreach(var child in scene.GetRootGameObjects())
        {
            Editable(child);
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

    private static void DisableBase()
    {
//        Scene scene = SceneManager.GetSceneByName("Main");
        Scene scene = SceneManager.GetActiveScene();

        foreach(var child in scene.GetRootGameObjects())
        {
            DisableBase(child);
        }
    }

    private static void EnableStage(GameObject obj, string stage)
    {
        foreach (Transform child in obj.transform)
        {
            EnableStage(child.gameObject, stage);
        }

        if (obj.transform.root.gameObject.name != stage)
        {
            obj.hideFlags |= HideFlags.NotEditable;
            obj.hideFlags |= HideFlags.HideInHierarchy;
            obj.hideFlags |= HideFlags.HideInInspector;
        }
    }

    private static void EnableStage(string stage)
    {
//        Scene scene = SceneManager.GetSceneByName("Main");
        Scene scene = SceneManager.GetActiveScene();

        foreach(var child in scene.GetRootGameObjects())
        {
            EnableStage(child, stage);
        }
    }

    [MenuItem("ステージ選択/ステージ1")]
    static void Stage1()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ1", true);
        EnableStage("ステージ1");
    }

    [MenuItem("ステージ選択/ステージ2")]
    static void Stage2()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ2", true);
        EnableStage("ステージ2");
    }

    [MenuItem("ステージ選択/ステージ3")]
    static void Stage3()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ3", true);
        EnableStage("ステージ3");
    }

    [MenuItem("ステージ選択/ステージ4")]
    static void Stage4()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ4", true);
        EnableStage("ステージ4");
    }

    [MenuItem("ステージ選択/ステージ5")]
    static void Stage5()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ5", true);
        EnableStage("ステージ5");
    }

    [MenuItem("ステージ選択/ステージ6")]
    static void Stage6()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ6", true);
        EnableStage("ステージ6");
    }

    [MenuItem("ステージ選択/ステージ7")]
    static void Stage7()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ7", true);
        EnableStage("ステージ7");
    }

    [MenuItem("ステージ選択/ステージ8")]
    static void Stage8()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ8", true);
        EnableStage("ステージ8");
    }

    [MenuItem("ステージ選択/ステージ9")]
    static void Stage9()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ9", true);
        EnableStage("ステージ9");
    }

    [MenuItem("ステージ選択/ステージ10")]
    static void Stage10()
    {
        Clear();
        DisableBase();
        Menu.SetChecked("ステージ選択/ステージ10", true);
        EnableStage("ステージ10");
    }

    [MenuItem("ステージ選択/全ステージ")]
    static void StageAll()
    {
        Clear();
        Menu.SetChecked("ステージ選択/全ステージ", true);
    }
}
