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
            EditorSceneManager.OpenScene("Assets/ステージ1.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ2.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ3.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ4.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ5.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ6.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ7.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ8.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ9.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/ステージ10.unity", OpenSceneMode.Additive);

            var editorAsm = typeof(Editor).Assembly;
            var inspWndType = editorAsm.GetType("UnityEditor.InspectorWindow");
            EditorWindow.GetWindow<WS専用パネル> (inspWndType);
        };
    }
}