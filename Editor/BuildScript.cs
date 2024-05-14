// using UnityEditor;
// using UnityEngine;
// using System.Collections.Generic;
// using System.IO;
// using System;
// // https://wonjuri.tistory.com/entry/unity-Build-Script-class
// // https://bitbucket.org/cargoldcompany/cargoldlibraryproject/src/master/Cargold/5_Remocon/ProjectRemocon/BuildSystem.cs
// public class BuildScript : MonoBehaviour
// {
//     /* App Info */
//     const string APP_NAME = "bsk"; // fileName
//     const string KEYSTORE_PASSWORD = "zxcv0987";
//     const string KEYSTORE_PATH = "/Users/sehwanlim/Dropbox/Keystore/Android/alchemists.keystore";
//     const string BUILD_PATH = "/Users/sehwanlim/My Drive (alchemistsgames@gmail.com)/APKs";
//     // const string BUILD_PATH = "/Users/sehwanlim/Desktop";

//     /* IOS 권한 메세지 정보 */
//     const string PHOTO_LIBRARY_USAGE_DESCRIPTION = "앱과 상호 작용하려면 사진 액세스 권한이 필요합니다.";
//     const string PHOTO_LIBRARY_ADDITIONS_USAGE_DESCRIPTION = "이 앱에 미디어를 저장하려면 사진에 액세스할 수 있어야 합니다.";
//     const string MICROPHONE_USAGE_DESCRIPTION = "앱 내 음성 확인 콘텐츠를 활용하려면 마이크 권한이 필요합니다.";
//     const bool DONT_ASK_LIMITED_PHOTOS_PERMISSION_AUTOMATICALLY_ON_IOS14 = true;

//     public static void Build(BuildTarget target, bool isAAB = false)
//     {
//         var fileName = "";
//         if (target == BuildTarget.Android)
//         {
//             PlayerSettings.Android.keystorePass = KEYSTORE_PASSWORD;
//             PlayerSettings.Android.keyaliasPass = KEYSTORE_PASSWORD;
//             PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
//             fileName = $"{APP_NAME}_{PlayerSettings.bundleVersion}.{(isAAB ? "aab" : "apk")}";
//             // fileName = $"/{APP_NAME}.{(isAAB ? "aab" : "apk")}";
//         }
//         var buildOption = new BuildPlayerOptions();
//         buildOption.locationPathName = BUILD_PATH + fileName;
//         buildOption.scenes = GetBuildSceneList();
//         buildOption.target = target;
//         BuildPipeline.BuildPlayer(buildOption);
//     }

//     [MenuItem("Builder/Build APK")]
//     public static void Build_APK() => Build(BuildTarget.Android);
//     [MenuItem("Builder/Build AAB")]
//     public static void Build_AAB() => Build(BuildTarget.Android);
//     [MenuItem("Builder/Build IOS")]
//     public static void BuildForIOS() => Build(BuildTarget.iOS);


//     [MenuItem("Builder/Open Directory")]
//     public static void OpenBuildDirectory()
//     {
//         bool openInsidesOfFolder = false;
//         if (Directory.Exists(BUILD_PATH)) openInsidesOfFolder = true;

//         var arguments = (openInsidesOfFolder ? "" : "-R ") + BUILD_PATH;
//         try
//         {
//             System.Diagnostics.Process.Start("open", arguments);
//         }
//         catch (Exception e)
//         {
//             Debug.Log("Failed to open path : " + e.ToString());
//         }
//     }


//     static string[] GetBuildSceneList()
//     {
//         var scenes = UnityEditor.EditorBuildSettings.scenes;
//         var listScenePath = new List<string>();
//         for (int i = 0; i < scenes.Length; i++)
//         {
//             if (scenes[i].enabled)
//                 listScenePath.Add(scenes[i].path);
//         }
//         return listScenePath.ToArray();
//     }

// #if UNITY_IOS
// #pragma warning disable 0162
//     [PostProcessBuild]
//     public static void OnPostprocessBuild(BuildTarget target, string buildPath)
//     {
//         if (target == BuildTarget.iOS)
//         {
//             string pbxProjectPath = PBXProject.GetPBXProjectPath(buildPath);
//             string plistPath = Path.Combine(buildPath, "Info.plist");
 
//             PBXProject pbxProject = new PBXProject();
//             pbxProject.ReadFromFile(pbxProjectPath);
 
//             string targetGUID = pbxProject.GetUnityFrameworkTargetGuid();

//             //필요한 라이브러리 추가//
//             //pbxProject.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-weak_framework PhotosUI");
//             //pbxProject.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-framework Photos");
//             //pbxProject.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-framework MobileCoreServices");
//             //pbxProject.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-framework ImageIO");
 
//             //pbxProject.RemoveFrameworkFromProject(targetGUID, "Photos.framework");
 
//             //File.WriteAllText(pbxProjectPath, pbxProject.WriteToString());
 
//             PlistDocument plist = new PlistDocument();
//             plist.ReadFromString(File.ReadAllText(plistPath));
 
//             PlistElementDict rootDict = plist.root;
//             //수출 규정 물어보지 않게 하기 위한 옵션.
//             rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);
//             //사진첩 사용권한 설명.
//             rootDict.SetString("NSPhotoLibraryUsageDescription", PHOTO_LIBRARY_USAGE_DESCRIPTION);
//             //사진추가 사용권한 설명.
//             rootDict.SetString("NSPhotoLibraryAddUsageDescription", PHOTO_LIBRARY_ADDITIONS_USAGE_DESCRIPTION);
//             //마이크 사용권한 설명.
//             rootDict.SetString("NSMicrophoneUsageDescription", MICROPHONE_USAGE_DESCRIPTION);
 
//             //if (DONT_ASK_LIMITED_PHOTOS_PERMISSION_AUTOMATICALLY_ON_IOS14)
//             //    rootDict.SetBoolean("PHPhotoLibraryPreventAutomaticLimitedAccessAlert", true);
 
//             File.WriteAllText(plistPath, plist.WriteToString());
//         }
//     }
// #pragma warning restore 0162
// #endif
// }