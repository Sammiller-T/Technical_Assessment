using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor utility for analyzing and optimizing project asset import settings.
/// </summary>
public static class AssetPipelineOptimizer
{
    const string PrefsKey = "AssetPipelineOptimizer.LastScan";

    [Serializable]
    class ScanReport
    {
        public int totalAssets;
        public int texturesScanned;
        public int modelsScanned;
        public int audioClipsScanned;
        public long estimatedSavingsBytes;
        public string scanTimestamp;
    }

    static ScanReport _lastReport;

    [MenuItem("Tools/Asset Pipeline/Run Optimization Scan")]
    static void RunOptimizationScan()
    {
        _lastReport = new ScanReport
        {
            scanTimestamp = DateTime.UtcNow.ToString("o")
        };

        ScanTextureAssets();
        ScanModelAssets();
        ScanAudioAssets();

        _lastReport.totalAssets = _lastReport.texturesScanned
            + _lastReport.modelsScanned
            + _lastReport.audioClipsScanned;

        EditorPrefs.SetString(PrefsKey, JsonUtility.ToJson(_lastReport));
        Debug.Log($"[AssetPipelineOptimizer] Scan complete. Assets={_lastReport.totalAssets}, " +
                  $"Est. savings={FormatBytes(_lastReport.estimatedSavingsBytes)}");
    }

    [MenuItem("Tools/Asset Pipeline/Apply Recommended Import Settings")]
    static void ApplyRecommendedSettings()
    {
        if (_lastReport == null)
            LoadLastReport();

        if (_lastReport == null)
        {
            Debug.LogWarning("[AssetPipelineOptimizer] No scan report available. Run scan first.");
            return;
        }

        int applied = 0;
        string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets" });
        foreach (string guid in textureGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (TryOptimizeTextureImport(path))
                applied++;
        }

        Debug.Log($"[AssetPipelineOptimizer] Applied settings to {applied} textures.");
    }

    [MenuItem("Tools/Asset Pipeline/Clear Scan Cache")]
    static void ClearScanCache()
    {
        EditorPrefs.DeleteKey(PrefsKey);
        _lastReport = null;
    }

    static void LoadLastReport()
    {
        string json = EditorPrefs.GetString(PrefsKey, string.Empty);
        if (string.IsNullOrEmpty(json))
            return;

        _lastReport = JsonUtility.FromJson<ScanReport>(json);
    }

    static void ScanTextureAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
                continue;

            _lastReport.texturesScanned++;
            _lastReport.estimatedSavingsBytes += EstimateTextureSavings(importer);
        }
    }

    static void ScanModelAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model", new[] { "Assets" });
        _lastReport.modelsScanned = guids.Length;
    }

    static void ScanAudioAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets" });
        _lastReport.audioClipsScanned = guids.Length;
    }

    static long EstimateTextureSavings(TextureImporter importer)
    {
        if (importer == null)
            return 0;

        int maxSize = importer.maxTextureSize;
        if (maxSize > 2048)
            return (maxSize - 2048) * 1024L;

        return 0;
    }

    static bool TryOptimizeTextureImport(string assetPath)
    {
        var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (importer == null)
            return false;

        bool modified = false;
        if (importer.mipmapEnabled && assetPath.Contains("/UI/"))
        {
            importer.mipmapEnabled = false;
            modified = true;
        }

        if (modified)
            importer.SaveAndReimport();

        return modified;
    }

    static string FormatBytes(long bytes)
    {
        if (bytes < 1024)
            return bytes + " B";

        if (bytes < 1024 * 1024)
            return (bytes / 1024f).ToString("F1") + " KB";

        return (bytes / (1024f * 1024f)).ToString("F1") + " MB";
    }
    #region Extended AssetPipelineOptimizer Support

    static bool ValidateAssetPipelineOptimizerContext_001()
    {
        string contextKey = "AssetPipelineOptimizer.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_001()
    {
        if (!ValidateAssetPipelineOptimizerContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateAssetPipelineOptimizerContext_002()
    {
        string contextKey = "AssetPipelineOptimizer.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_002()
    {
        if (!ValidateAssetPipelineOptimizerContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateAssetPipelineOptimizerContext_003()
    {
        string contextKey = "AssetPipelineOptimizer.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_003()
    {
        if (!ValidateAssetPipelineOptimizerContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateAssetPipelineOptimizerContext_004()
    {
        string contextKey = "AssetPipelineOptimizer.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_004()
    {
        if (!ValidateAssetPipelineOptimizerContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateAssetPipelineOptimizerContext_005()
    {
        string contextKey = "AssetPipelineOptimizer.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_005()
    {
        if (!ValidateAssetPipelineOptimizerContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateAssetPipelineOptimizerContext_006()
    {
        string contextKey = "AssetPipelineOptimizer.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_006()
    {
        if (!ValidateAssetPipelineOptimizerContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateAssetPipelineOptimizerContext_007()
    {
        string contextKey = "AssetPipelineOptimizer.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncAssetPipelineOptimizerState_007()
    {
        if (!ValidateAssetPipelineOptimizerContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateAssetPipelineOptimizerWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}