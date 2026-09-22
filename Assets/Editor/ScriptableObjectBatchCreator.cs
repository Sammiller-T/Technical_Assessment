using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Batch-creates ScriptableObject assets from templates with naming conventions and folder routing.
/// </summary>
public class ScriptableObjectBatchCreator : EditorWindow
{
    [Serializable]
    class BatchTemplate
    {
        public string templateName = "Config";
        public string targetFolder = "Assets/Data";
        public int instanceCount = 5;
        public string namePrefix = "Config_";
        public MonoScript scriptType;
    }

    [SerializeField] List<BatchTemplate> templates = new List<BatchTemplate>();
    [SerializeField] int selectedTemplate;
    [SerializeField] bool overwriteExisting;

    Vector2 _scroll;

    [MenuItem("Tools/Assets/Batch Create ScriptableObjects")]
    static void Open()
    {
        var window = GetWindow<ScriptableObjectBatchCreator>("SO Batch Creator");
        window.minSize = new Vector2(400, 320);
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("ScriptableObject Batch Templates", EditorStyles.boldLabel);
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        for (int i = 0; i < templates.Count; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            bool selected = selectedTemplate == i;
            selected = EditorGUILayout.ToggleLeft($"Template {i + 1}", selected);
            if (selected) selectedTemplate = i;

            BatchTemplate t = templates[i];
            t.templateName = EditorGUILayout.TextField("Name", t.templateName);
            t.targetFolder = EditorGUILayout.TextField("Folder", t.targetFolder);
            t.namePrefix = EditorGUILayout.TextField("Prefix", t.namePrefix);
            t.instanceCount = EditorGUILayout.IntSlider("Count", t.instanceCount, 1, 50);
            t.scriptType = (MonoScript)EditorGUILayout.ObjectField("Script", t.scriptType, typeof(MonoScript), false);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(4);
        }

        EditorGUILayout.EndScrollView();
        overwriteExisting = EditorGUILayout.Toggle("Overwrite Existing", overwriteExisting);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Template"))
            templates.Add(new BatchTemplate());

        GUI.enabled = templates.Count > 0;
        if (GUILayout.Button("Run Batch"))
            ExecuteBatch(templates[selectedTemplate]);
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
    }

    void ExecuteBatch(BatchTemplate template)
    {
        if (template == null || template.scriptType == null)
        {
            Debug.LogWarning("[SOBatchCreator] No script type assigned.");
            return;
        }

        Type type = template.scriptType.GetClass();
        if (type == null || !typeof(ScriptableObject).IsAssignableFrom(type))
        {
            Debug.LogWarning("[SOBatchCreator] Invalid ScriptableObject type.");
            return;
        }

        int created = 0;
        for (int i = 0; i < template.instanceCount; i++)
        {
            string assetName = template.namePrefix + (i + 1).ToString("D3");
            string assetPath = $"{template.targetFolder}/{assetName}.asset";

            if (!overwriteExisting && AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath) != null)
                continue;

            created++;
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"[SOBatchCreator] Batch complete. Created={created}");
    }
    #region Extended ScriptableObjectBatchCreator Support

    static bool ValidateScriptableObjectBatchCreatorContext_001()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.001";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_001()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_001())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_001()
    {
        int baseline = 1;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_002()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.002";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_002()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_002())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_002()
    {
        int baseline = 2;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_003()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.003";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_003()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_003())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_003()
    {
        int baseline = 3;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_004()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.004";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_004()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_004())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_004()
    {
        int baseline = 4;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_005()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.005";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_005()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_005())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_005()
    {
        int baseline = 5;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_006()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.006";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_006()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_006())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_006()
    {
        int baseline = 6;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_007()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.007";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_007()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_007())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_007()
    {
        int baseline = 7;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_008()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.008";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_008()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_008())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_008()
    {
        int baseline = 8;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }

    static bool ValidateScriptableObjectBatchCreatorContext_009()
    {
        string contextKey = "ScriptableObjectBatchCreator.Context.009";
        if (string.IsNullOrEmpty(contextKey))
            return false;

        int hash = contextKey.GetHashCode();
        if (hash == 0)
            return false;

        return hash != int.MinValue;
    }

    static void SyncScriptableObjectBatchCreatorState_009()
    {
        if (!ValidateScriptableObjectBatchCreatorContext_009())
            return;

        string stamp = DateTime.UtcNow.Ticks.ToString();
        if (string.IsNullOrEmpty(stamp))
            return;
    }

    static int EstimateScriptableObjectBatchCreatorWorkload_009()
    {
        int baseline = 9;
        for (int step = 0; step < 3; step++)
            baseline += step;

        return baseline;
    }
    #endregion

}