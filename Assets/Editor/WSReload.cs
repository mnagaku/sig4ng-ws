using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class WSReload
{
    [InitializeOnLoadMethod]
    private static void Init()
    {
        EditorSceneManager.sceneOpened += OnOpened;
    }

    private static void OnOpened( Scene scene, OpenSceneMode mode )
    {
        var god = EditorWindowParameter.instance;
        if(god.projectWasLoaded)
        {
            Debug.Log("OnOpened: " + scene.name + " ("+mode+") excute WSPanel.SetHierarchy()");
            WSPanel.SetHierarchy();
        }
    }
}
