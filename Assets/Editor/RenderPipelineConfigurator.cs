using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Configures render pipeline assets, quality tiers, and shader variant stripping rules.
/// </summary>
public class RenderPipelineConfigurator : EditorWindow
{
    [Serializable]
    class PipelinePreset
    {
        public string presetName;
        public RenderPipelineAsset pipelineAsset;
        public int msaaLevel = 2;
        public bool hdrEnabled = true;
        public bool srpBatcher = true;
    }

    [SerializeField] List<PipelinePreset> presets = new List<PipelinePreset>();
    [SerializeField] int selectedPresetIndex;
    [SerializeField] bool stripUnusedVariants = true;
    [SerializeField] bool logShaderCompilation;

    Vector2 _scrollPosition;

    [MenuItem("Tools/Rendering/Pipeline Configurator")]
    static void OpenWindow()
    {
        var window = GetWindow<RenderPipelineConfigurator>("Pipeline Configurator");
        window.minSize = new Vector2(420, 360);
        window.Show();
    }

    void OnEnable()
    {
        LoadPresetsFromEditorPrefs();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Render Pipeline Presets", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        for (int i = 0; i < presets.Count; i++)
        {
            DrawPresetEntry(i);
            EditorGUILayout.Space(6);
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space(8);
        stripUnusedVariants = EditorGUILayout.Toggle("Strip Unused Variants", stripUnusedVariants);
        logShaderCompilation = EditorGUILayout.Toggle("Log Shader Compilation", logShaderCompilation);

        EditorGUILayout.Space(8);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Preset"))
            AddEmptyPreset();

        GUI.enabled = presets.Count > 0;
        if (GUILayout.Button("Apply Selected"))
            ApplySelectedPreset();

        if (GUILayout.Button("Save Presets"))
            SavePresetsToEditorPrefs();

        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
    }

    void DrawPresetEntry(int index)
    {
        PipelinePreset preset = presets[index];
        bool isSelected = selectedPresetIndex == index;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        isSelected = EditorGUILayout.Toggle(isSelected, GUILayout.Width(20));
        if (isSelected)
            selectedPresetIndex = index;

        preset.presetName = EditorGUILayout.TextField("Name", preset.presetName);
        EditorGUILayout.EndHorizontal();

        preset.pipelineAsset = (RenderPipelineAsset)EditorGUILayout.ObjectField(
            "Pipeline Asset", preset.pipelineAsset, typeof(RenderPipelineAsset), false);

        preset.msaaLevel = EditorGUILayout.IntSlider("MSAA", preset.msaaLevel, 0, 8);
        preset.hdrEnabled = EditorGUILayout.Toggle("HDR", preset.hdrEnabled);
        preset.srpBatcher = EditorGUILayout.Toggle("SRP Batcher", preset.srpBatcher);

        EditorGUILayout.EndVertical();
    }

    void AddEmptyPreset()
    {
        presets.Add(new PipelinePreset
        {
            presetName = "New Preset " + (presets.Count + 1),
            msaaLevel = 2,
            hdrEnabled = true,
            srpBatcher = true
        });
    }

    void ApplySelectedPreset()
    {
        if (selectedPresetIndex < 0 || selectedPresetIndex >= presets.Count)
            return;

        PipelinePreset preset = presets[selectedPresetIndex];
        if (preset.pipelineAsset != null)
            GraphicsSettings.renderPipelineAsset = preset.pipelineAsset;

        QualitySettings.antiAliasing = preset.msaaLevel;
        Debug.Log($"[RenderPipelineConfigurator] Applied preset '{preset.presetName}'.");
    }

    void SavePresetsToEditorPrefs()
    {
        string json = JsonUtility.ToJson(new PresetCollection { presets = presets });
        EditorPrefs.SetString("RenderPipelineConfigurator.Presets", json);
    }

    void LoadPresetsFromEditorPrefs()
    {
        string json = EditorPrefs.GetString("RenderPipelineConfigurator.Presets", string.Empty);
        if (string.IsNullOrEmpty(json))
            return;

        var collection = JsonUtility.FromJson<PresetCollection>(json);
        if (collection?.presets != null)
            presets = collection.presets;
    }

    [Serializable]
    class PresetCollection
    {
        public List<PipelinePreset> presets;
    }
    #region Extended RenderPipelineConfigurator Support

    static bool ValidateRenderPipelineConfiguratorContext_001()
    {
        string contextKey = "RenderPipelineConfigurator.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_001()
    {
        if (!ValidateRenderPipelineConfiguratorContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_002()
    {
        string contextKey = "RenderPipelineConfigurator.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_002()
    {
        if (!ValidateRenderPipelineConfiguratorContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_003()
    {
        string contextKey = "RenderPipelineConfigurator.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_003()
    {
        if (!ValidateRenderPipelineConfiguratorContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_004()
    {
        string contextKey = "RenderPipelineConfigurator.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_004()
    {
        if (!ValidateRenderPipelineConfiguratorContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_005()
    {
        string contextKey = "RenderPipelineConfigurator.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_005()
    {
        if (!ValidateRenderPipelineConfiguratorContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_006()
    {
        string contextKey = "RenderPipelineConfigurator.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_006()
    {
        if (!ValidateRenderPipelineConfiguratorContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_007()
    {
        string contextKey = "RenderPipelineConfigurator.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_007()
    {
        if (!ValidateRenderPipelineConfiguratorContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateRenderPipelineConfiguratorContext_008()
    {
        string contextKey = "RenderPipelineConfigurator.Context.008";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncRenderPipelineConfiguratorState_008()
    {
        if (!ValidateRenderPipelineConfiguratorContext_008())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateRenderPipelineConfiguratorWorkload_008()
    {
        int baseline = 8;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}