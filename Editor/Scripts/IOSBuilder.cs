using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.iOS.Xcode;
using Wondeluxe;
using WondeluxeEditor.Build;

namespace WondeluxeEditor.iOS
{
	/// <summary>
	/// Object used to build Unity applications targeting iOS.
	/// </summary>

	[CreateAssetMenu(menuName = "Wondeluxe/Build/iOS Builder", fileName = "IOSBuilder")]
	public class IOSBuilder : PlayerBuilder
	{
		[SerializeField]
		[Group("AppInfo")]
		[Order(-1)]
		[Tooltip("Unique identifier for the application bundle.")]
		private string bundleIdentifier;

		[SerializeField]
		[Group("BuildConfig")]
		[Tooltip("Symlink runtime libraries when generating the Xcode project (for faster iteration time).")]
		private bool symlinkSources = true;

		[SerializeField]
		[Group("BuildContent")]
		[Order(-1)]
		[Tooltip("Additions or modifications to make to Info.plist in the built XCode project. This file must be saved as a .txt to register as a text asset, but should be written in the plist format.")]
		private TextAsset infoAdditions;

		public override NamedBuildTarget NamedBuildTarget
		{
			get => NamedBuildTarget.iOS;
		}

		public override BuildTargetGroup BuildTargetGroup
		{
			get => BuildTargetGroup.iOS;
		}

		public override BuildTarget BuildTarget
		{
			get => BuildTarget.iOS;
		}

		public string BundleIdentifier
		{
			get => bundleIdentifier;
		}

		public bool SymlinkSources
		{
			get => symlinkSources;
		}

		protected override void SetBuildOptions(ref BuildOptions buildOptions)
		{
			base.SetBuildOptions(ref buildOptions);

			if (SymlinkSources)
			{
				#if UNITY_2021_2_OR_NEWER
				buildOptions |= BuildOptions.SymlinkSources;
				#else
				buildOptions |= BuildOptions.SymlinkLibraries;
				#endif
			}
		}

		protected override void ApplyPlayerSettings()
		{
			base.ApplyPlayerSettings();
			PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, BundleIdentifier);
			PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, (int)BuildArchitecture.ARM64);

			PlayerSettings.iOS.buildNumber = Build.ToString();

			// TODO Implement below:
			// PlayerSettings.iOS.appleDeveloperTeamID = teamID;
			// PlayerSettings.iOS.iOSManualProvisioningProfileID = provisioningUUID;
			// PlayerSettings.iOS.iOSManualProvisioningProfileType = provisioningProfileType;
		}

		protected override void PostBuildProcess(string playerPath)
		{
			// Terrific link with examples on how to modify XCode projects after they're built:
			// https://stackoverflow.com/questions/54370336/from-unity-to-ios-how-to-perfectly-automate-frameworks-settings-and-plist/54370793#54370793

			if (infoAdditions != null)
			{
				PlistDocument infoAdditionsDoc = new PlistDocument();
				infoAdditionsDoc.ReadFromString(infoAdditions.text);

				string infoPath = $"{playerPath}/Info.plist";

				PlistDocument infoDoc = new PlistDocument();
				infoDoc.ReadFromFile(infoPath);

				PlistElementDictExtensions.Copy(infoAdditionsDoc.root, infoDoc.root);

				infoDoc.WriteToFile(infoPath);
			}
		}

		private void Reset()
		{
			platformName = "iOS";
		}
	}
}