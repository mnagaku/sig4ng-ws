using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDisplay : MonoBehaviour
{
    public Text message = null;

    private void Awake()
    {
        Application.logMessageReceived  += HandleLog;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived  += HandleLog;
    }

    private void HandleLog( string logText, string stackTrace, LogType type )
    {
        if(type == LogType.Log) {
            message.text += logText + "\n";
        }
    }
}
