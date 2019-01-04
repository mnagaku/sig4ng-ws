using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WSButton : MonoBehaviour
{
    [InitializeOnLoadMethod]
    static void Example()
    {
        SceneView.onSceneGUIDelegate += OnGUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void OnGUI(SceneView sceneView)
    {
        var rect = new Rect(8, 24, 80, 0);

        GUI.WindowFunction func = id =>
        {
            if (GUILayout.Button("じめんにつける"))
            {
                for (int idx = 0; idx < Selection.gameObjects.Length; idx++)
                {
                    var gobj = Selection.gameObjects[idx];

                    if (gobj.GetComponent<object_validator>() == null)
                        continue;

                    if (gobj.isStatic)
                        continue;

                    Rigidbody rb = gobj.GetComponent<Rigidbody>();
                    RaycastHit hitInfo;
                    if (rb && rb.SweepTest(Vector3.down, out hitInfo))
                    {
                        gobj.transform.Translate(Vector3.down * hitInfo.distance, Space.World);
                        Undo.RecordObject(gobj.transform, "Translate " + gobj.name);
                    }
                }
            }
            else if (GUILayout.Button("ふやす"))
            {
                for (int idx = 0; idx < Selection.gameObjects.Length; idx++)
                {
                    if (Selection.gameObjects[idx].GetComponent<object_validator>() == null)
                        continue;

                    if (Selection.gameObjects[idx].isStatic)
                        continue;

                    var gobj = Object.Instantiate(Selection.gameObjects[idx]);
                    gobj.transform.parent = Selection.gameObjects[idx].transform.parent;

                    // 今のところ親オブジェクトにぶら下がっているもの同士をチェックする
                    if (gobj.transform.parent)
                    {
                        Vector3 newPos = gobj.transform.position;

                        int i = 0, siblingCount = gobj.transform.parent.childCount;

                        while (i < siblingCount)
                        {
                            Transform child = gobj.transform.parent.GetChild(i);

                            //Debug.Log(child.position);

                            if (child.GetInstanceID() != gobj.transform.GetInstanceID())
                            {
                                // [TODO] 本当はコリジョンの大きさなども見るべき
                                if (child.transform.position == newPos
                                    && child.transform.rotation == gobj.transform.rotation
                                    && child.transform.localScale == gobj.transform.localScale)
                                {
                                    newPos += new Vector3(0.2f, 0f, 0.2f);
                                    i = 0;      // Siblingチェックを最初からやり直す.
                                    continue;
                                }
                            }

                            i++;
                        }

                        gobj.transform.position = newPos;
                    }

                    Undo.RegisterCreatedObjectUndo(gobj, "Duplicate Object");

                }
            }
        };

        GUILayout.Window(1, rect, func, string.Empty);
    }
}