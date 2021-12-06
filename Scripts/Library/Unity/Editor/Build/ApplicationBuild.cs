using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;


namespace TakahashiH.Build
{
    /// <summary>
    /// �A�v���P�[�V�����̃r���h���s��
    /// </summary>
    public sealed class ApplicationBuild
    {
        //====================================
        //! �֐��ipublic)
        //====================================

        /// <summary>
        /// Windows �����r���h
        /// </summary>
        [MenuItem("Tools/Build/Application/Windows")]
        public static void BuildWindows()
        {
            BuildImpl(RuntimePlatform.WindowsPlayer);
        }

        /// <summary>
        /// Android �����r���h
        /// </summary>
        [MenuItem("Tools/Build/Application/Android")]
        public static void BuildAndroid()
        {
            BuildImpl(RuntimePlatform.Android);
        }

        /// <summary>
        /// iOS �����r���h
        /// </summary>
        [MenuItem("Tools/Build/Application/iOS")]
        public static void BuildIOS()
        {
            BuildImpl(RuntimePlatform.IPhonePlayer);
        }


        //====================================
        //! �֐��ipublic)
        //====================================

        /// <summary>
        /// �r���h��������
        /// </summary>
        /// <param name="platform"> �v���b�g�t�H�[�� </param>
        private static void BuildImpl(RuntimePlatform platform)
        {
            Debug.Log($"Begin {platform} Application Build.");

            var scenePathList   = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
            var outputPath      = GetOutputPath(platform);
            var buildTarget     = GetBuildTarget(platform);
            var buildOptions    = GetBuildOptions(platform);

            for (int i = 0; i < scenePathList.Length; i++)
            {
                Debug.Log($"scenePathList({i}) : {scenePathList[i]}");
            }

            Debug.Log($"outputPath : {outputPath}");
            Debug.Log($"buildTarget : {buildTarget}");
            Debug.Log($"buildOptions : {buildOptions}");

            var buildReport = BuildPipeline.BuildPlayer
            (
                levels              : scenePathList     ,
                locationPathName    : outputPath        ,
                target              : buildTarget       ,
                options             : buildOptions
            );

            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Success ApplicationBuild!");
            }
            else
            {
                Debug.Log("Failed ApplicationBuild...");
                Debug.Log($"buildReport.summary.result : {buildReport.summary.result}");
            }
        }

        /// <summary>
        /// �o�͐�̃p�X�擾
        /// </summary>
        /// <param name="platform"> �v���b�g�t�H�[�� </param>
        private static string GetOutputPath(RuntimePlatform platform)
        {
            var basePath        = $"{Application.dataPath}/../Output/";
            var assetFolderDI   = new System.IO.DirectoryInfo(System.IO.Path.GetFullPath(Application.dataPath));
            var projectName     = assetFolderDI.Parent.Name;

            return platform switch
            {
                RuntimePlatform.WindowsPlayer   => $"{basePath}/Windows/{projectName}.exe"  ,
                RuntimePlatform.Android         => $"{basePath}/Android/{projectName}.apk"  ,
                RuntimePlatform.IPhonePlayer    => $"{basePath}/iOS/{projectName}.ipa"      ,
                _                               => string.Empty                             ,
            };
        }

        /// <summary>
        /// �r���h�^�[�Q�b�g�擾
        /// </summary>
        /// <param name="platform"> �v���b�g�t�H�[�� </param>
        private static BuildTarget GetBuildTarget(RuntimePlatform platform)
        {
            return platform switch
            {
                RuntimePlatform.WindowsPlayer   => BuildTarget.StandaloneWindows    ,
                RuntimePlatform.Android         => BuildTarget.Android              ,
                RuntimePlatform.IPhonePlayer    => BuildTarget.iOS                  ,
                _                               => BuildTarget.NoTarget             ,
            };
        }

        /// <summary>
        /// �r���h�I�v�V�����擾
        /// </summary>
        /// <param name="platform"> �v���b�g�t�H�[�� </param>
        private static BuildOptions GetBuildOptions(RuntimePlatform platform)
        {
            return BuildOptions.None;
        }
    }
}
