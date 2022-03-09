using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace NashiraDeer.SkyerBuilder
{
    /// <summary>
    /// A step with the build options and target for <see cref="SkyerEngine"/> that gonna be built.
    /// </summary>
    public class SkyerBuildStep
    {
        /// <summary>
        /// Target platform to be built in this step.
        /// </summary>
        public SkyerTarget Target = SkyerTarget.Unknown;

        /// <summary>
        /// Unity <see cref="BuildOptions"/> to be provided for Unity Build Pipeline during this step.
        /// </summary>
        public BuildOptions Options = BuildOptions.None;

        /// <summary>
        /// Scenes to be built in this step.
        /// </summary>
        public string[] Scenes = { };

        /// <summary>
        /// Initialize a Skyer Build Step with a platform target, build options and scenes predefined.
        /// </summary>
        /// <param name="target">Platform target for this step.</param>
        /// <param name="options">Unity Build Options used in this step.</param>
        /// <param name="scenes">Scenes to be built in this step.</param>
        public SkyerBuildStep(SkyerTarget target, BuildOptions options, string[] scenes)
        {
            Target = target;
            Options = options;
            Scenes = scenes;
        }
    }

    /// <summary>
    /// A single build report from <see cref="SkyerEngine"/>.
    /// </summary>
    public class SkyerBuildReport
    {
        /// <summary>
        /// Step executed that has returned this report.
        /// </summary>
        public SkyerBuildStep Step = null;

        /// <summary>
        /// Build Report returned by the Unity Build Pipeline.
        /// </summary>
        public BuildReport Report = null;
    }

    /// <summary>
    /// Platform targets used by the <see cref="SkyerEngine"/>, represents a simplified target from <see cref="BuildTarget"/> and <see cref="BuildTargetGroup"/> used in Unity.
    /// </summary>
    public enum SkyerTarget : byte
    {
        /// <summary>
        /// The default value of <see cref="SkyerTarget"/>, doesn't represent any platform.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Represents the Standalone Player for Windows 64-bits.
        /// </summary>
        Windows = 1,

        /// <summary>
        /// Represents the Standalone Player for Windows 32-bits.
        /// </summary>
        Windows32 = 2,

        /// <summary>
        /// Represents the Standalone Player for Linux 64-bits.
        /// </summary>
        Linux = 3,

        /// <summary>
        /// Represents the Standalone Player for MacOS Intel.
        /// </summary>
        Mac = 4,

        /// <summary>
        /// Represents the WebGL Player.
        /// </summary>
        WebGL = 5,

        /// <summary>
        /// Represents the Universal Windows Platform Player.
        /// </summary>
        WSA = 6,

        /// <summary>
        /// Represents the Android Player.
        /// </summary>
        Android = 7,

        /// <summary>
        /// Represents the iOS Player.
        /// </summary>
        iOS = 8,

        /// <summary>
        /// Represents the PlayStation 4 Player.
        /// </summary>
        PS4 = 9,

        /// <summary>
        /// Represents the PlayStation 5 Player.
        /// </summary>
        PS5 = 10,

        /// <summary>
        /// Represents the Xbox One Player.
        /// </summary>
        XboxOne = 11,

        /// <summary>
        /// Represents the Nintendo Switch Player.
        /// </summary>
        Switch = 12,

        /// <summary>
        /// Represents the Google Stadia Player.
        /// </summary>
        Stadia = 13,

        /// <summary>
        /// Represents the tvOS Player.
        /// </summary>
        tvOS = 14,

        /// <summary>
        /// Represents the CloudRendering Player.
        /// </summary>
        CloudRendering = 15
    }

    /// <summary>
    /// The default build engine used by the Skyer Builder to organize and automatize the builds.
    /// </summary>
    public class SkyerEngine
    {
        /// <summary>
        /// Resolve a target from Skyer to a target from Unity to be used in Unity Build Pipeline.
        /// </summary>
        /// <param name="skyerTarget">Platform target from Skyer Builder.</param>
        /// <returns>Build Target from Unity.</returns>
        public static BuildTarget ResolveBuildTarget(SkyerTarget skyerTarget)
        {
            switch (skyerTarget)
            {
                case SkyerTarget.Windows: return BuildTarget.StandaloneWindows64;
                case SkyerTarget.Windows32: return BuildTarget.StandaloneWindows;
                case SkyerTarget.Linux: return BuildTarget.StandaloneLinux64;
                case SkyerTarget.Mac: return BuildTarget.StandaloneOSX;
                case SkyerTarget.PS4: return BuildTarget.PS4;
                case SkyerTarget.PS5: return BuildTarget.PS5;
                case SkyerTarget.Stadia: return BuildTarget.Stadia;
                case SkyerTarget.XboxOne: return BuildTarget.XboxOne;
                case SkyerTarget.Switch: return BuildTarget.Switch;
                case SkyerTarget.tvOS: return BuildTarget.tvOS;
                case SkyerTarget.WebGL: return BuildTarget.WebGL;
                case SkyerTarget.WSA: return BuildTarget.WSAPlayer;
                case SkyerTarget.CloudRendering: return BuildTarget.CloudRendering;
                case SkyerTarget.Android: return BuildTarget.Android;
                case SkyerTarget.iOS: return BuildTarget.iOS;
                default: throw new NotSupportedException("Can't get Unity Target for a invalid or not supported Skyer Target");
            }
        }

        /// <summary>
        /// Resolve a target from Skyer to a target group from Unity to be used in Unity Build Pipeline.
        /// </summary>
        /// <param name="skyerTarget">Platform target from Skyer Builder.</param>
        /// <returns>Build Target Group from Unity.</returns>
        public static BuildTargetGroup ResolveTargetGroup(SkyerTarget skyerTarget)
        {
            switch (skyerTarget)
            {
                case SkyerTarget.Windows:
                case SkyerTarget.Windows32:
                case SkyerTarget.Linux:
                case SkyerTarget.Mac: return BuildTargetGroup.Standalone;
                case SkyerTarget.PS4: return BuildTargetGroup.PS4;
                case SkyerTarget.PS5: return BuildTargetGroup.PS5;
                case SkyerTarget.Stadia: return BuildTargetGroup.Stadia;
                case SkyerTarget.XboxOne: return BuildTargetGroup.XboxOne;
                case SkyerTarget.Switch: return BuildTargetGroup.Switch;
                case SkyerTarget.tvOS: return BuildTargetGroup.tvOS;
                case SkyerTarget.WebGL: return BuildTargetGroup.WebGL;
                case SkyerTarget.WSA: return BuildTargetGroup.WSA;
                case SkyerTarget.CloudRendering: return BuildTargetGroup.CloudRendering;
                case SkyerTarget.Android: return BuildTargetGroup.Android;
                case SkyerTarget.iOS: return BuildTargetGroup.iOS;
                default: throw new NotSupportedException("Can't get Unity Target Group for a invalid or not supported Skyer Target");
            }
        }

        /// <summary>
        /// Resolve a build path to be compatible with the location path used in Unity Build Pipeline.
        /// </summary>
        /// <param name="buildpath">Build path to be resolved.</param>
        /// <param name="target">Platform target used to determine the changes for the build path.</param>
        /// <returns>A location path ready to be used in Unity Build Pipeline.</returns>
        public static string ResolveLocationPath(string buildpath, SkyerTarget target)
        {
            switch (target)
            {
                case SkyerTarget.Windows:
                case SkyerTarget.Windows32:
                    return Path.Combine(buildpath, PlayerSettings.productName + ".exe");
                case SkyerTarget.Mac:
                    return Path.Combine(buildpath, PlayerSettings.productName);
                case SkyerTarget.Linux:
                    return Path.Combine(buildpath, PlayerSettings.productName.Replace(" ", "") + ".x86_64");
                case SkyerTarget.Android:
                    return Path.Combine(buildpath, PlayerSettings.productName + ".apk");
                default:
                    return buildpath;
            }
        }

        /// <summary>
        /// Callback called every time that a step is built during the <see cref="BatchBuild(SkyerBuildStep[], SkyerBuildProgress)"/>.
        /// </summary>
        /// <param name="report">The report from the last build.</param>
        /// <param name="builded">The steps that already has built.</param>
        /// <param name="total">Total of steps to be built.</param>
        /// <returns>If true, the batch build is cancelled.</returns>
        public delegate bool SkyerBuildProgress(SkyerBuildReport report, int builded, int total);

        /// <summary>
        /// Directory to place the players built by this Skyer Engine.
        /// </summary>
        public string BuildPath { get; set; }

        /// <summary>
        /// Create a new Skyer Engine specifying a path to place the builds.
        /// </summary>
        /// <param name="buildpath">Directory to place the players built by this Skyer Engine.</param>
        public SkyerEngine(string buildpath)
        {
            BuildPath = buildpath;
        }

        /// <summary>
        /// Starts the build engine, building only a player.
        /// </summary>
        /// <param name="step">A single step to be built.</param>
        /// <returns>Build report containing information about this build.</returns>
        public SkyerBuildReport Build(SkyerBuildStep step)
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = step.Scenes;
            options.target = ResolveBuildTarget(step.Target);
            options.targetGroup = ResolveTargetGroup(step.Target);
            options.options = step.Options;

            string buildpath = Path.Combine(BuildPath, step.Target.ToString());
            Directory.CreateDirectory(buildpath);
            options.locationPathName = ResolveLocationPath(buildpath, step.Target);

            SkyerBuildReport buildReport = new SkyerBuildReport();
            buildReport.Step = step;
            buildReport.Report = BuildPipeline.BuildPlayer(options);

            return buildReport;
        }

        /// <summary>
        /// Starts the build engine, building a batch of steps.
        /// </summary>
        /// <param name="steps">Steps to be built, one by one.</param>
        /// <param name="progress">A progress callback executed at the end of every step.</param>
        /// <returns>An array with all the <see cref="SkyerBuildReport"/> returned by the <see cref="Build(SkyerBuildStep)"/>.</returns>
        public SkyerBuildReport[] BatchBuild(SkyerBuildStep[] steps, SkyerBuildProgress progress)
        {
            List<SkyerBuildReport> results = new List<SkyerBuildReport>();

            for (int i = 0; i < steps.Length; i++)
            {
                SkyerBuildReport buildReport = Build(steps[i]);

                results.Add(buildReport);

                if (progress(buildReport, i + 1, steps.Length)) break;
            }

            return results.ToArray();
        }
    }
}
