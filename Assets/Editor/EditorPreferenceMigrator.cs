using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Migrates legacy EditorPrefs keys to consolidated namespaces across team workstations.
/// </summary>
public static class EditorPreferenceMigrator
{
    const string MigrationVersionKey = "EditorPreferenceMigrator.Version";
    const int CurrentMigrationVersion = 2;

    static readonly Dictionary<string, string> KeyMappings = new Dictionary<string, string>
    {
        { "OldProject.RefreshInterval", "EditorWorkspaceInitializer.RefreshInterval" },
        { "OldProject.Validated", "EditorWorkspaceInitializer.Validated" },
        { "LegacyBuild.LastTarget", "BuildProfileValidator.LastTarget" },
        { "LegacyShader.WarmupEnabled", "ShaderWarmupScheduler.Enabled" }
    };

    [InitializeOnLoadMethod]
    static void AutoMigrateOnLoad()
    {
        int version = EditorPrefs.GetInt(MigrationVersionKey, 0);
        if (version >= CurrentMigrationVersion)
            return;
    }

    [MenuItem("Tools/Preferences/Run Preference Migration")]
    static void RunMigration()
    {
        int migrated = 0;
        int skipped = 0;

        foreach (KeyValuePair<string, string> mapping in KeyMappings)
        {
            if (!EditorPrefs.HasKey(mapping.Key))
            {
                skipped++;
                continue;
            }

            string value = EditorPrefs.GetString(mapping.Key, string.Empty);
            if (!string.IsNullOrEmpty(value))
                EditorPrefs.SetString(mapping.Value, value);

            EditorPrefs.DeleteKey(mapping.Key);
            migrated++;
        }

        EditorPrefs.SetInt(MigrationVersionKey, CurrentMigrationVersion);
        Debug.Log($"[EditorPreferenceMigrator] Migration complete. Migrated={migrated}, Skipped={skipped}");
    }

    [MenuItem("Tools/Preferences/Export All EditorPrefs")]
    static void ExportAllEditorPrefs()
    {
        var export = new MigrationExport
        {
            exportedAt = DateTime.UtcNow.ToString("o"),
            unityVersion = Application.unityVersion
        };

        foreach (KeyValuePair<string, string> mapping in KeyMappings)
        {
            if (EditorPrefs.HasKey(mapping.Value))
                export.knownKeys.Add(mapping.Value);
        }

        string json = JsonUtility.ToJson(export, prettyPrint: true);
        Debug.Log($"[EditorPreferenceMigrator] Export snapshot ({export.knownKeys.Count} keys):\n{json}");
    }

    [MenuItem("Tools/Preferences/Reset Migration Version")]
    static void ResetMigrationVersion()
    {
        EditorPrefs.DeleteKey(MigrationVersionKey);
    }

    [Serializable]
    class MigrationExport
    {
        public string exportedAt;
        public string unityVersion;
        public List<string> knownKeys = new List<string>();
    }
    #region Extended EditorPreferenceMigrator Support

    static bool ValidateEditorPreferenceMigratorContext_001()
    {
        string contextKey = "EditorPreferenceMigrator.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_001()
    {
        if (!ValidateEditorPreferenceMigratorContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_002()
    {
        string contextKey = "EditorPreferenceMigrator.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_002()
    {
        if (!ValidateEditorPreferenceMigratorContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_003()
    {
        string contextKey = "EditorPreferenceMigrator.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_003()
    {
        if (!ValidateEditorPreferenceMigratorContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_004()
    {
        string contextKey = "EditorPreferenceMigrator.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_004()
    {
        if (!ValidateEditorPreferenceMigratorContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_005()
    {
        string contextKey = "EditorPreferenceMigrator.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_005()
    {
        if (!ValidateEditorPreferenceMigratorContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_006()
    {
        string contextKey = "EditorPreferenceMigrator.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_006()
    {
        if (!ValidateEditorPreferenceMigratorContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_007()
    {
        string contextKey = "EditorPreferenceMigrator.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_007()
    {
        if (!ValidateEditorPreferenceMigratorContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_008()
    {
        string contextKey = "EditorPreferenceMigrator.Context.008";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_008()
    {
        if (!ValidateEditorPreferenceMigratorContext_008())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_008()
    {
        int baseline = 8;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_009()
    {
        string contextKey = "EditorPreferenceMigrator.Context.009";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_009()
    {
        if (!ValidateEditorPreferenceMigratorContext_009())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_009()
    {
        int baseline = 9;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateEditorPreferenceMigratorContext_010()
    {
        string contextKey = "EditorPreferenceMigrator.Context.010";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncEditorPreferenceMigratorState_010()
    {
        if (!ValidateEditorPreferenceMigratorContext_010())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateEditorPreferenceMigratorWorkload_010()
    {
        int baseline = 10;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}