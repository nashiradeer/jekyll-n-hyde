using UnityEngine;

namespace NashiraDeer.SkyerBuilder
{
    /// <summary>
    /// Contains the values used by <see cref="SimpleSkyerWindow"/> that are synced in the disk.
    /// </summary>
    public class SimpleSkyerSettings : ScriptableObject
    {
        /// <summary>
        /// The directory used to place the builds.
        /// </summary>
        public string BuildPath = "";
        /// <summary>
        /// Create a subdirectory inside the build path with the product name.
        /// </summary>
        public bool PlaceInProductFolder = false;
        /// <summary>
        /// Create a subdirectory inside the build path with the version.
        /// </summary>
        public bool PlaceInVersionFolder = false;

        /// <summary>
        /// Create step to build for Windows 64-bit.
        /// </summary>
        public bool Windows = false;
        /// <summary>
        /// Create step to build for Windows 32-bit.
        /// </summary>
        public bool Windows32 = false;
        /// <summary>
        /// Create step to build for Linux 64-bit.
        /// </summary>
        public bool Linux = false;
        /// <summary>
        /// Create step to build for Mac OS X (Intel 64-bit).
        /// </summary>
        public bool MacOSX = false;
        /// <summary>
        /// Create step to build for Android.
        /// </summary>
        public bool Android = false;
        /// <summary>
        /// Create step to build for iOS.
        /// </summary>
        public bool iOS = false;
        /// <summary>
        /// Create step to build for WebGL.
        /// </summary>
        public bool WebGL = false;
        /// <summary>
        /// Create step to build for WSA.
        /// </summary>
        public bool WSA = false;
        /// <summary>
        /// Create step to build for PlayStation 4.
        /// </summary>
        public bool PS4 = false;
        /// <summary>
        /// Create step to build for PlayStation 5.
        /// </summary>
        public bool PS5 = false;
        /// <summary>
        /// Create step to build for Xbox One.
        /// </summary>
        public bool XboxOne = false;
        /// <summary>
        /// Create step to build for Nintendo Switch.
        /// </summary>
        public bool Switch = false;
        /// <summary>
        /// Create step to build for Google Stadia.
        /// </summary>
        public bool Stadia = false;
        /// <summary>
        /// Create step to build for tvOS.
        /// </summary>
        public bool tvOS = false;
        /// <summary>
        /// Create step to build for Cloud Rendenring.
        /// </summary>
        public bool CloudRendenring = false;

        /// <summary>
        /// Append the Build Options "Development Build" for all steps.
        /// </summary>
        public bool DevelopmentBuild = false;
    }
}
