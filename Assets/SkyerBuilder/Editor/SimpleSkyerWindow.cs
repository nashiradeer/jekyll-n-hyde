using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace NashiraDeer.SkyerBuilder
{
    /// <summary>
    /// A window for Unity Editor having simplified controller for Skyer Builder.
    /// </summary>
    public class SimpleSkyerWindow : EditorWindow
    {
        /// <summary>
        /// The status of the Skyer Engine in this window, used to avoid run <see cref="StartBuild"/> more than one time during GUI updates.
        /// </summary>
        public bool IsBuilding { get; private set; } = false;

        /// <summary>
        /// Settings value database used by this window.
        /// </summary>
        public SerializedObject Settings = null;

        /// <summary>
        /// Get the window and show it to the user.
        /// </summary>
        [MenuItem("Window/Skyer Builder")]
        public static void ShowWindow()
        {
            GetWindow<SimpleSkyerWindow>().Show();
        }

        /// <summary>
        /// Initialize the window and load the database from the disk.
        /// </summary>
        private void Awake()
        {
            titleContent = new GUIContent("Skyer Builder");
            SimpleSkyerSettings settings = AssetDatabase.LoadAssetAtPath<SimpleSkyerSettings>("Assets/Editor/Skyer Builder/Simple Settings.asset");

            if (settings == null)
            {
                settings = CreateInstance<SimpleSkyerSettings>();

                if (!AssetDatabase.IsValidFolder("Assets/Editor")) AssetDatabase.CreateFolder("Assets", "Editor");
                if (!AssetDatabase.IsValidFolder("Assets/Editor/Skyer Builder")) AssetDatabase.CreateFolder("Assets/Editor", "Skyer Builder");

                AssetDatabase.CreateAsset(settings, "Assets/Editor/Skyer Builder/Simple Settings.asset");
            }

            Settings = new SerializedObject(settings);
        }

        /// <summary>
        /// Save the database on the disk, if the window is closed.
        /// </summary>
        private void OnDestroy() => SaveChanges();

        /// <summary>
        /// Save the database on the disk.
        /// </summary>
        public override void SaveChanges()
        {
            Settings.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
            base.SaveChanges();
        }

        /// <summary>
        /// Draw the elements on the window in Unity Editor.
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Label("Engine Settings", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(Settings.FindProperty("BuildPath"), new GUIContent("Build Path"));

            if (GUILayout.Button("...", GUILayout.ExpandWidth(false)))
                Settings.FindProperty("BuildPath").stringValue = EditorUtility.SaveFolderPanel("Choosing build path...", Settings.FindProperty("BuildPath").stringValue, PlayerSettings.productName);

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(Settings.FindProperty("PlaceInProductFolder"), new GUIContent("Place In Product Folder"));
            EditorGUILayout.PropertyField(Settings.FindProperty("PlaceInVersionFolder"), new GUIContent("Place In Version Folder"));

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Platform Targets", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(Settings.FindProperty("Windows"), new GUIContent("Windows 64-bit"));
            EditorGUILayout.PropertyField(Settings.FindProperty("Windows32"), new GUIContent("Windows 32-bit"));
            EditorGUILayout.PropertyField(Settings.FindProperty("Linux"), new GUIContent("Linux 64-bit"));
            EditorGUILayout.PropertyField(Settings.FindProperty("MacOSX"), new GUIContent("Mac OS X (Intel 64-bit)"));
            EditorGUILayout.PropertyField(Settings.FindProperty("Android"), new GUIContent("Android"));
            EditorGUILayout.PropertyField(Settings.FindProperty("iOS"), new GUIContent("iOS"));
            EditorGUILayout.PropertyField(Settings.FindProperty("WebGL"), new GUIContent("WebGL"));
            EditorGUILayout.PropertyField(Settings.FindProperty("WSA"), new GUIContent("WSA"));
            EditorGUILayout.PropertyField(Settings.FindProperty("PS4"), new GUIContent("PlayStation 4"));
            EditorGUILayout.PropertyField(Settings.FindProperty("PS5"), new GUIContent("PlayStation 5"));
            EditorGUILayout.PropertyField(Settings.FindProperty("XboxOne"), new GUIContent("Xbox One"));
            EditorGUILayout.PropertyField(Settings.FindProperty("Switch"), new GUIContent("Nintendo Switch"));
            EditorGUILayout.PropertyField(Settings.FindProperty("Stadia"), new GUIContent("Google Stadia"));
            EditorGUILayout.PropertyField(Settings.FindProperty("tvOS"), new GUIContent("tvOS"));
            EditorGUILayout.PropertyField(Settings.FindProperty("CloudRendenring"), new GUIContent("Cloud Rendenring"));

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Player Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(Settings.FindProperty("DevelopmentBuild"), new GUIContent("Development Build"));

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Skyer Control Panel", EditorStyles.boldLabel);

            if (GUILayout.Button("Build", GUILayout.Width(118))) StartBuild();
        }

        /// <summary>
        /// Starts the building process initializing the Skyer Engine.
        /// </summary>
        private void StartBuild()
        {
            if (IsBuilding) throw new InvalidOperationException("Can't build if a already has a build running...");

            EditorUtility.DisplayProgressBar("Skyer Builder", "Saving...", 0);
            SaveChanges();

            IsBuilding = true;
            string buildpath = Path.GetFullPath(Settings.FindProperty("BuildPath").stringValue);

            if (Settings.FindProperty("PlaceInProductFolder").boolValue) buildpath = Path.Combine(buildpath, Application.productName);
            if (Settings.FindProperty("PlaceInVersionFolder").boolValue) buildpath = Path.Combine(buildpath, Application.version);

            if (EditorUtility.DisplayCancelableProgressBar("Skyer Builder", "Creating options...", 0))
            {
                EditorUtility.ClearProgressBar();
                IsBuilding = false;
                return;
            }
            BuildOptions options = new BuildOptions();

            if (Settings.FindProperty("DevelopmentBuild").boolValue) options |= BuildOptions.Development;

            if (EditorUtility.DisplayCancelableProgressBar("Skyer Builder", "Creating scene list...", 0))
            {
                EditorUtility.ClearProgressBar();
                IsBuilding = false;
                return;
            }
            List<string> scenes = new List<string>();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
                if (scene.enabled) scenes.Add(scene.path);

            if (EditorUtility.DisplayCancelableProgressBar("Skyer Builder", "Creating build steps...", 0))
            {
                EditorUtility.ClearProgressBar();
                IsBuilding = false;
                return;
            }

            List<SkyerBuildStep> steps = new List<SkyerBuildStep>();

            if (Settings.FindProperty("Windows").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Windows, options, scenes.ToArray()));
            if (Settings.FindProperty("Windows32").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Windows32, options, scenes.ToArray()));
            if (Settings.FindProperty("Linux").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Linux, options, scenes.ToArray()));
            if (Settings.FindProperty("MacOSX").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Mac, options, scenes.ToArray()));
            if (Settings.FindProperty("Android").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Android, options, scenes.ToArray()));
            if (Settings.FindProperty("iOS").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.iOS, options, scenes.ToArray()));
            if (Settings.FindProperty("WebGL").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.WebGL, options, scenes.ToArray()));
            if (Settings.FindProperty("WSA").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.WSA, options, scenes.ToArray()));
            if (Settings.FindProperty("PS4").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.PS4, options, scenes.ToArray()));
            if (Settings.FindProperty("PS5").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.PS5, options, scenes.ToArray()));
            if (Settings.FindProperty("XboxOne").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.XboxOne, options, scenes.ToArray()));
            if (Settings.FindProperty("Switch").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Switch, options, scenes.ToArray()));
            if (Settings.FindProperty("Stadia").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.Stadia, options, scenes.ToArray()));
            if (Settings.FindProperty("tvOS").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.tvOS, options, scenes.ToArray()));
            if (Settings.FindProperty("CloudRendenring").boolValue) steps.Add(new SkyerBuildStep(SkyerTarget.CloudRendering, options, scenes.ToArray()));

            if (EditorUtility.DisplayCancelableProgressBar("Skyer Builder", "Initializing engine...", 0))
            {
                EditorUtility.ClearProgressBar();
                IsBuilding = false;
                return;
            }

            SkyerEngine engine = new SkyerEngine(buildpath);

            if (EditorUtility.DisplayCancelableProgressBar("Skyer Builder", "Starting building...", 0))
            {
                EditorUtility.ClearProgressBar();
                IsBuilding = false;
                return;
            }
            Thread.Sleep(3000);

            engine.BatchBuild(steps.ToArray(), (SkyerBuildReport report, int builded, int total) =>
            {
                if (EditorUtility.DisplayCancelableProgressBar("Skyer Builder", "Building '" + report.Step.Target.ToString() + "'...", (float)builded / total))
                {
                    EditorUtility.ClearProgressBar();
                    IsBuilding = false;
                    return true;
                }
                Thread.Sleep(3000);
                return false;
            });

            IsBuilding = false;
            EditorUtility.ClearProgressBar();
        }
    }
}
