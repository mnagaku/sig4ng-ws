using System;
using System.IO;
using System.Linq;
using UnityEditor;

[InitializeOnLoad]
public static class PathUpdater
{
    static PathUpdater()
    {
        // プロジェクトのパスを取得
        string projectPath = Directory.GetCurrentDirectory();

        // 2つのツールへのpath文字列を構築
        string targetBinPath1 = Path.Combine(projectPath, "TortoiseGit", "bin");
        string targetBinPath2 = Path.Combine(projectPath, "PortableGit", "cmd");

        // 現在の環境変数 PATH を取得
        string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        UnityEngine.Debug.Log($"before PATH: '{currentPath}'");

        // targetBinPath1 が PATH に含まれていない場合、先頭に追加
        if (!currentPath.Split(Path.PathSeparator).Contains(targetBinPath1, StringComparer.OrdinalIgnoreCase))
        {
            string newPath = targetBinPath1 + Path.PathSeparator + currentPath;
            Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.Process);
            UnityEngine.Debug.Log($"Added '{targetBinPath1}' to PATH.");
        }
        else
        {
            UnityEngine.Debug.Log($"'{targetBinPath1}' is already in PATH.");
        }

        currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);

        // targetBinPath2 が PATH に含まれていない場合、先頭に追加
        if (!currentPath.Split(Path.PathSeparator).Contains(targetBinPath2, StringComparer.OrdinalIgnoreCase))
        {
            string newPath = targetBinPath2 + Path.PathSeparator + currentPath;
            Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.Process);
            UnityEngine.Debug.Log($"Added '{targetBinPath2}' to PATH.");
        }
        else
        {
            UnityEngine.Debug.Log($"'{targetBinPath2}' is already in PATH.");
        }

        string afterPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        UnityEngine.Debug.Log($"after PATH: '{afterPath}'");
    }
}
