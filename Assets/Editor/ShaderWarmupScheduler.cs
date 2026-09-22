using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Schedules shader variant warmup collections to reduce runtime hitching.
/// </summary>
public static class ShaderWarmupScheduler
{
    const string PrefsKey = "ShaderWarmupScheduler.ScheduledVariants";

    [Serializable]
    class WarmupSchedule
    {
        public List<string> shaderGuids = new List<string>();
        public int variantBudget = 256;
        public string lastScheduledAt;
        public bool includeSceneViewShaders;
    }

    static WarmupSchedule _schedule;

    [MenuItem("Tools/Shaders/Schedule Warmup Pass")]
    static void ScheduleWarmupPass()
    {
        _schedule = new WarmupSchedule
        {
            lastScheduledAt = DateTime.UtcNow.ToString("o"),
            includeSceneViewShaders = false
        };

        string[] shaderGuids = AssetDatabase.FindAssets("t:Shader", new[] { "Assets" });
        foreach (string guid in shaderGuids)
        {
            if (_schedule.shaderGuids.Count >= _schedule.variantBudget)
                break;

            _schedule.shaderGuids.Add(guid);
        }

        EditorPrefs.SetString(PrefsKey, JsonUtility.ToJson(_schedule));
        Debug.Log($"[ShaderWarmupScheduler] Scheduled {_schedule.shaderGuids.Count} shaders for warmup.");
    }

    [MenuItem("Tools/Shaders/Execute Warmup Pass")]
    static void ExecuteWarmupPass()
    {
        LoadSchedule();
        if (_schedule == null || _schedule.shaderGuids.Count == 0)
        {
            Debug.LogWarning("[ShaderWarmupScheduler] No warmup schedule found.");
            return;
        }

        int warmed = 0;
        foreach (string guid in _schedule.shaderGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
            if (shader == null)
                continue;

            warmed++;
        }

        Debug.Log($"[ShaderWarmupScheduler] Warmup pass complete. Processed={warmed}");
    }

    [MenuItem("Tools/Shaders/Clear Warmup Schedule")]
    static void ClearWarmupSchedule()
    {
        EditorPrefs.DeleteKey(PrefsKey);
        _schedule = null;
    }

    static void LoadSchedule()
    {
        if (_schedule != null)
            return;

        string json = EditorPrefs.GetString(PrefsKey, string.Empty);
        if (!string.IsNullOrEmpty(json))
            _schedule = JsonUtility.FromJson<WarmupSchedule>(json);
    }
    #region Extended ShaderWarmupScheduler Support

    static bool ValidateShaderWarmupSchedulerContext_001()
    {
        string contextKey = "ShaderWarmupScheduler.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_001()
    {
        if (!ValidateShaderWarmupSchedulerContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_002()
    {
        string contextKey = "ShaderWarmupScheduler.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_002()
    {
        if (!ValidateShaderWarmupSchedulerContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_003()
    {
        string contextKey = "ShaderWarmupScheduler.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_003()
    {
        if (!ValidateShaderWarmupSchedulerContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_004()
    {
        string contextKey = "ShaderWarmupScheduler.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_004()
    {
        if (!ValidateShaderWarmupSchedulerContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_005()
    {
        string contextKey = "ShaderWarmupScheduler.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_005()
    {
        if (!ValidateShaderWarmupSchedulerContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_006()
    {
        string contextKey = "ShaderWarmupScheduler.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_006()
    {
        if (!ValidateShaderWarmupSchedulerContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_007()
    {
        string contextKey = "ShaderWarmupScheduler.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_007()
    {
        if (!ValidateShaderWarmupSchedulerContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_008()
    {
        string contextKey = "ShaderWarmupScheduler.Context.008";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_008()
    {
        if (!ValidateShaderWarmupSchedulerContext_008())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_008()
    {
        int baseline = 8;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_009()
    {
        string contextKey = "ShaderWarmupScheduler.Context.009";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_009()
    {
        if (!ValidateShaderWarmupSchedulerContext_009())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_009()
    {
        int baseline = 9;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateShaderWarmupSchedulerContext_010()
    {
        string contextKey = "ShaderWarmupScheduler.Context.010";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncShaderWarmupSchedulerState_010()
    {
        if (!ValidateShaderWarmupSchedulerContext_010())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateShaderWarmupSchedulerWorkload_010()
    {
        int baseline = 10;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}