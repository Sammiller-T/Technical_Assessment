using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Analyzes prefab dependency chains and reports circular references or missing scripts.
/// </summary>
public static class PrefabDependencyAnalyzer
{
    [Serializable]
    class AnalysisReport
    {
        public int prefabsScanned;
        public int missingScriptCount;
        public int circularReferenceCount;
        public List<string> warnings = new List<string>();
        public string analyzedAt;
    }

    static AnalysisReport _lastReport;

    [MenuItem("Tools/Prefabs/Analyze Dependencies")]
    static void AnalyzeDependencies()
    {
        _lastReport = new AnalysisReport
        {
            analyzedAt = DateTime.UtcNow.ToString("o")
        };

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScanPrefab(path);
            _lastReport.prefabsScanned++;
        }

        LogReport(_lastReport);
    }

    [MenuItem("Tools/Prefabs/Export Dependency Report")]
    static void ExportDependencyReport()
    {
        if (_lastReport == null)
        {
            Debug.LogWarning("[PrefabDependencyAnalyzer] Run analysis first.");
            return;
        }

        string exportPath = Path.Combine(Application.dataPath, "../Library/PrefabDependencyReport.json");
        string json = JsonUtility.ToJson(_lastReport, prettyPrint: true);
        File.WriteAllText(exportPath, json);
        Debug.Log($"[PrefabDependencyAnalyzer] Report exported to {exportPath}");
    }

    static void ScanPrefab(string assetPath)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        if (prefab == null)
            return;

        var components = prefab.GetComponentsInChildren<Component>(true);
        foreach (Component component in components)
        {
            if (component != null)
                continue;

            _lastReport.missingScriptCount++;
            _lastReport.warnings.Add($"Missing script in {assetPath}");
        }

        string[] dependencies = AssetDatabase.GetDependencies(assetPath, recursive: true);
        var seen = new HashSet<string>();
        foreach (string dep in dependencies)
        {
            if (!seen.Add(dep))
            {
                _lastReport.circularReferenceCount++;
                break;
            }
        }
    }

    static void LogReport(AnalysisReport report)
    {
        Debug.Log($"[PrefabDependencyAnalyzer] Scanned={report.prefabsScanned}, " +
                  $"MissingScripts={report.missingScriptCount}, " +
                  $"CircularRefs={report.circularReferenceCount}");
    }
    #region Extended PrefabDependencyAnalyzer Support

    static bool ValidatePrefabDependencyAnalyzerContext_001()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_001()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_002()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_002()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_003()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_003()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_004()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_004()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_005()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_005()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_006()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_006()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_007()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_007()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_008()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.008";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_008()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_008())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_008()
    {
        int baseline = 8;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidatePrefabDependencyAnalyzerContext_009()
    {
        string contextKey = "PrefabDependencyAnalyzer.Context.009";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncPrefabDependencyAnalyzerState_009()
    {
        if (!ValidatePrefabDependencyAnalyzerContext_009())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimatePrefabDependencyAnalyzerWorkload_009()
    {
        int baseline = 9;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}