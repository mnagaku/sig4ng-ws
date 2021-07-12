using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class billboard : MonoBehaviour
{
    private bool prevSelected = false;
    private Vector3 pivotPos = new Vector3(0.0f, -1.0f, 0.0f);

    void OnRenderObject()
    {
        if (Camera.current == null)
            return;

        bool isPlaying = true;

#if UNITY_EDITOR
        isPlaying = EditorApplication.isPlaying;
#endif

        // カメラ条件の絞り込み.
        if (isPlaying && Camera.current.name == "Main Camera")
            UpdateTransformInGame();
        else if (!isPlaying && Camera.current.name == "SceneCamera")
            UpdateTransformInEditor();
    }

    private Quaternion origPivotRot;

    private void UpdateTransformInEditor()
    {
#if UNITY_EDITOR
        // エディタモードの場合は、自分が選択状態でなければビルボード処理を行わない
        if (Selection.activeGameObject == this.gameObject)
        {
            // 選択中である

            // 以前は選択中ではなかった
            if (!prevSelected)
            {
                // 元の姿勢を退避させておく.
                origPivotRot = transform.rotation;
                prevSelected = true;
            }

            Vector3 pos = transform.TransformPoint(pivotPos);             // ピボット算出
            transform.rotation = Camera.current.transform.rotation;       // 姿勢を合わせる
            transform.position = pos - transform.rotation * pivotPos;     // 新しい位置を出す.
        }
        else // 選択中ではない
        {
            // しかし、以前は選択中であった（＝さきほど解除された）
            if (prevSelected)
            {
                // 現在のピボット位置から、姿勢を戻す
                Vector3 pos = transform.TransformPoint(pivotPos);          // ピボット算出
                transform.rotation = origPivotRot;                         // 姿勢を書き戻す
                transform.position = pos - transform.rotation * pivotPos;  // 新しい位置を出す.

                prevSelected = false;
            }
        }
#endif
    }

    private void UpdateTransformInGame()
    {
        if (Camera.current.transform.rotation != transform.rotation)
        {
            Vector3 pos = transform.TransformPoint(pivotPos);             // ピボット算出
            transform.rotation = Camera.current.transform.rotation;       // 姿勢を合わせる
            transform.position = pos - transform.rotation * pivotPos;     // 新しい位置を出す.
        }
    }
}
