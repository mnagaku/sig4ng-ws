using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_validator : MonoBehaviour
{
    // 一度だけOnValidate()内でオブジェクトの重複チェックを行う
    private bool hasBeenCheckedPos = false;

    void OnValidate()
    {
        if (hasBeenCheckedPos)
            return;

        // 今のところ親オブジェクトにぶら下がっているもの同士をチェックする
        if (transform.parent)
        {
            Vector3 newPos = transform.position;

            int i = 0, siblingCount = transform.parent.childCount;

            while (i < siblingCount)
            {
                Transform child = transform.parent.GetChild(i);

                //Debug.Log(child.position);

                if (child.GetInstanceID() != transform.GetInstanceID())
                {
                    // [TODO] 本当はコリジョンの大きさなども見るべき
                    if (child.transform.position == newPos
                        && child.transform.rotation == transform.rotation
                        && child.transform.localScale == transform.localScale)
                    {
                        newPos += new Vector3(0.2f, 0f, 0.2f);
                        i = 0;      // Siblingチェックを最初からやり直す.
                        continue;
                    }
                }

                i++;
            }

            transform.position = newPos;
        }

        hasBeenCheckedPos = true;
    }
}
