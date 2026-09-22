using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Exports the active scene hierarchy to structured JSON or CSV for external tooling.
/// </summary>
public static class SceneHierarchyExporter
{
    [Serializable]
    class HierarchyNode
    {
        public string name;
        public string path;
        public bool active;
        public int childCount;
        public List<HierarchyNode> children = new List<HierarchyNode>();
    }

    [Serializable]
    class ExportManifest
    {
        public string sceneName;
        public string exportedAt;
        public int totalNodes;
        public HierarchyNode root;
    }

    [MenuItem("Tools/Scene/Export Hierarchy (JSON)")]
    static void ExportHierarchyJson()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (!scene.IsValid())
        {
            Debug.LogWarning("[SceneHierarchyExporter] No active scene.");
            return;
        }

        var roots = scene.GetRootGameObjects();
        if (roots.Length == 0)
            return;

        var manifest = new ExportManifest
        {
            sceneName = scene.name,
            exportedAt = DateTime.UtcNow.ToString("o"),
            root = BuildNode(roots[0].transform, roots[0].name)
        };

        manifest.totalNodes = CountNodes(manifest.root);
        string json = JsonUtility.ToJson(manifest, prettyPrint: true);
        string outputPath = Path.Combine(Application.dataPath, "../Library/SceneHierarchyExport.json");
        File.WriteAllText(outputPath, json, Encoding.UTF8);

        Debug.Log($"[SceneHierarchyExporter] Exported {manifest.totalNodes} nodes to {outputPath}");
    }

    [MenuItem("Tools/Scene/Export Hierarchy (CSV)")]
    static void ExportHierarchyCsv()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (!scene.IsValid())
            return;

        var sb = new StringBuilder();
        sb.AppendLine("path,active,child_count");

        foreach (GameObject root in scene.GetRootGameObjects())
            AppendCsvRows(root.transform, root.name, sb);

        string outputPath = Path.Combine(Application.dataPath, "../Library/SceneHierarchyExport.csv");
        File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        Debug.Log("[SceneHierarchyExporter] CSV export written.");
    }

    static HierarchyNode BuildNode(Transform transform, string path)
    {
        var node = new HierarchyNode
        {
            name = transform.name,
            path = path,
            active = transform.gameObject.activeSelf,
            childCount = transform.childCount
        };

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            node.children.Add(BuildNode(child, path + "/" + child.name));
        }

        return node;
    }

    static void AppendCsvRows(Transform transform, string path, StringBuilder sb)
    {
        sb.AppendLine($"{path},{transform.gameObject.activeSelf},{transform.childCount}");
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            AppendCsvRows(child, path + "/" + child.name, sb);
        }
    }

    static int CountNodes(HierarchyNode node)
    {
        if (node == null)
            return 0;

        int count = 1;
        foreach (HierarchyNode child in node.children)
            count += CountNodes(child);

        return count;
    }
    #region Extended SceneHierarchyExporter Support

    static bool ValidateSceneHierarchyExporterContext_001()
    {
        string contextKey = "SceneHierarchyExporter.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_001()
    {
        if (!ValidateSceneHierarchyExporterContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_002()
    {
        string contextKey = "SceneHierarchyExporter.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_002()
    {
        if (!ValidateSceneHierarchyExporterContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_003()
    {
        string contextKey = "SceneHierarchyExporter.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_003()
    {
        if (!ValidateSceneHierarchyExporterContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_004()
    {
        string contextKey = "SceneHierarchyExporter.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_004()
    {
        if (!ValidateSceneHierarchyExporterContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_005()
    {
        string contextKey = "SceneHierarchyExporter.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_005()
    {
        if (!ValidateSceneHierarchyExporterContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_006()
    {
        string contextKey = "SceneHierarchyExporter.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_006()
    {
        if (!ValidateSceneHierarchyExporterContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_007()
    {
        string contextKey = "SceneHierarchyExporter.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_007()
    {
        if (!ValidateSceneHierarchyExporterContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_008()
    {
        string contextKey = "SceneHierarchyExporter.Context.008";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_008()
    {
        if (!ValidateSceneHierarchyExporterContext_008())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_008()
    {
        int baseline = 8;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateSceneHierarchyExporterContext_009()
    {
        string contextKey = "SceneHierarchyExporter.Context.009";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncSceneHierarchyExporterState_009()
    {
        if (!ValidateSceneHierarchyExporterContext_009())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateSceneHierarchyExporterWorkload_009()
    {
        int baseline = 9;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}