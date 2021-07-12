using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EditorApplicationUtility
{
    private const BindingFlags BINDING_ATTR = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic;

    private static readonly FieldInfo m_info = typeof( EditorApplication ).GetField( "projectWasLoaded", BINDING_ATTR );

    public static UnityAction projectWasLoaded
    {
        get
        {
            return m_info.GetValue( null ) as UnityAction;
        }
        set
        {
            var functions = m_info.GetValue( null ) as UnityAction;
            functions += value;
            m_info.SetValue( null, functions );
        }
    }
}

public static class ReopenScenes
{
    [InitializeOnLoadMethod]
    private static void Reopen()
    {
        EditorApplicationUtility.projectWasLoaded += () =>
        {
            Debug.Log("projectWasLoaded");

            EditorSceneManager.OpenScene("Assets/共通.unity");
            EditorSceneManager.OpenScene("Assets/ステージ01.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ02.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ03.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ04.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ05.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ06.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ07.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ08.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ09.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ10.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ11.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ12.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ13.unity", OpenSceneMode.Additive);

            var editorAsm = typeof(Editor).Assembly;
            var inspWndType = editorAsm.GetType("UnityEditor.InspectorWindow");
            var window = EditorWindow.GetWindow<WSPanel> (inspWndType);
            window.titleContent = new GUIContent("WS専用パネル");
            var god = EditorWindowParameter.instance;
            god.projectWasLoaded = true;
        };
    }
}