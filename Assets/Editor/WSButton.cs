using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class WSButton : MonoBehaviour
{
    [InitializeOnLoadMethod]
    static void Example()
    {
        SceneView.duringSceneGui += OnGUI;
    }

    static void ContactFloor(GameObject tgtObj)
    {
        Rigidbody rb = tgtObj.GetComponent<Rigidbody>();
        RaycastHit hitInfo;
        if (rb)
        {
            // 下向きにフィッティング
            if (rb.SweepTest(Vector3.down, out hitInfo))
            {
                Undo.RecordObject(tgtObj.transform, "Translate " + tgtObj.name);
                tgtObj.transform.Translate(Vector3.down * hitInfo.distance, Space.World);
            }
            else
            {
                Undo.RecordObject(tgtObj.transform, "Translate " + tgtObj.name);

                // 対Terrain的な計算を行う
                Vector3 bottomPos = tgtObj.transform.position + Vector3.down * tgtObj.transform.localScale.y * 0.5f;
                // 地上へ引き上げるためのオフセットを得る ... 1.0fは若干決め打ち
                float offset = 1.0f - bottomPos.y;

                // 移動させる
                tgtObj.transform.position += Vector3.up * offset;

                // 下向きにフィッティング
                if (rb.SweepTest(Vector3.down, out hitInfo))
                {
                    tgtObj.transform.Translate(Vector3.down * hitInfo.distance, Space.World);
                }
            }
        }
    }

    static void OnGUI(SceneView sceneView)
    {
        var rect = new Rect(8, 24, 80, 0);

        GUI.WindowFunction func = id =>
        {
            if (GUILayout.Button("じめんにあわせる"))
            {
                for (int idx = 0; idx < Selection.gameObjects.Length; idx++)
                {
                    var gobj = Selection.gameObjects[idx];

                    if (gobj.GetComponent<object_validator>() == null)
                        continue;

                    if (gobj.isStatic)
                        continue;

                    ContactFloor(gobj);
                }
            }
            else if (GUILayout.Button("ふやす"))
            {
                Regex rgx1 = new Regex(@"(^.+) \(([0-9]+)\)");
                Regex rgx2 = new Regex(@"\(([0-9]+)\)$");

                for (int idx = 0; idx < Selection.gameObjects.Length; idx++)
                {
                    if (Selection.gameObjects[idx].isStatic)
                        continue;

                    var orig_obj = Selection.gameObjects[idx];
                    var gobj = Object.Instantiate(orig_obj, orig_obj.transform.parent);

                    gobj.transform.localPosition = orig_obj.transform.localPosition;
                    gobj.transform.localRotation = orig_obj.transform.localRotation;
                    gobj.transform.localScale = orig_obj.transform.localScale;

                    // 複製したオブジェクトの位置を、オリジナルと重ならないように少しずらす
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
                        ContactFloor(gobj);
                    }

                    // リネーム（あまり賢くない）
                    // 「ドミノ1 (1)」のようなオブジェクトがあれば「ドミノ1 (2)」を作る
                    // 数値の重複は避ける
                    {
                        string bodyname = orig_obj.name;
                        int number = 1;

                        var m1 = rgx1.Match(gobj.name);
                        if (m1.Success) // 「ドミノ1」のパターン
                        {
                            bodyname = m1.Groups[1].Value;
                        }

                        // 兄弟から同じ命名体系の中で括弧内の数字が一番 大きなものをさがす
                        int i = 0, siblingCount = gobj.transform.parent.childCount;

                        while (i < siblingCount)
                        {
                            Transform child = gobj.transform.parent.GetChild(i);

                            var m2 = rgx2.Match(child.gameObject.name);
                            if (child.gameObject.name.StartsWith(bodyname) && m2.Success)
                            {
                                Debug.Log(m2.Groups[1].Value);
                                int t = System.Convert.ToInt32(m2.Groups[1].Value);
                                if (t > number)
                                {
                                    number = t;
                                }
                            }

                            i++;
                        }

                        number++;
                        gobj.name = bodyname + " (" + number + ")";
                    }

                    Undo.RegisterCreatedObjectUndo(gobj, "Duplicate Object");

                    if (Selection.gameObjects.Length == 1)
                        Selection.activeObject = gobj;
                }
            }
            else if (GUILayout.Button("やりなおす"))
            {
                Undo.PerformUndo();
            }
        };

        GUILayout.Window(1, rect, func, string.Empty);
    }
}