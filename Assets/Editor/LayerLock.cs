using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class LayerLock
{
    private const int layer = 8; // base

    static LayerLock ()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI( int instanceID, Rect selectionRect )
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null || go.layer != layer)
        {
            return;
        }
        go.hideFlags |= HideFlags.NotEditable;
//        go.hideFlags |= HideFlags.HideInHierarchy;
    }
}
